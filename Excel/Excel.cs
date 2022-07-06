using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using rso.core;

namespace rso.excel
{
    // public 멤버 변수만 순서대로 정렬
    // IEnumerable<FieldInfo> orderedFields = typeof(T).GetFields().OrderBy(field => field.MetadataToken);
    public class CExcel : IDisposable
    {
        delegate void TSetFunc(CStream Stream_, string Data_);

        CEnumValue _EnumValue = null;
        string _TargetExtension;
        Int32 _RowOffset;
        string _FileName = "";
        Application _Application = null;
        Workbook _Workbook = null;
        Worksheet _Worksheet = null;

        public CExcel(List<Enum> EnumTypes_, string TargetExtension_, Int32 RowOffset_)
        {
            _EnumValue = new CEnumValue(EnumTypes_);
            _TargetExtension = TargetExtension_;
            _RowOffset = RowOffset_;
        }
        ~CExcel()
        {
            Dispose();
        }
        void DisposeWorksheet()
        {
            if (_Worksheet != null)
            {
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(_Worksheet);
                _Worksheet = null;
            }
        }
        public void Dispose()
        {
            Close();
        }
        public void Open(string FileName_)
        {
            if (FileName_.Length == 0)
                throw new Exception("File Name Is Empty");

            Close();

            var FullPath = Path.GetFullPath(FileName_);

            if (!File.Exists(FullPath))
                throw new Exception(string.Format("File[{0}] not found", FullPath)); // 없는 파일을 바로 _Application.Workbooks.Open 여기서 체크가 되면 Excel 프로세스가 더 오래 남음.

            try
            {
                _Application = new Application();
                _Workbook = _Application.Workbooks.Open(FullPath);
            }
            catch
            {
                Dispose();
                throw;
            }

            _FileName = FileName_;
        }
        public void Close()
        {
            if (_FileName.Length == 0)
                return;

            DisposeWorksheet();

            if (_Workbook != null)
            {
                _Workbook.Close(false);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(_Workbook);
                _Workbook = null;
            }
            if (_Application != null)
            {
                _Application.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(_Application);
                _Application = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            _FileName = "";
        }
        public void ExportEnum(string SheetName_, string ColumnName_, string TargetDir_, string TargetFile_, string EnumFrameFormat_)
        {
            try
            {
                try
                {
                    _Worksheet = (Worksheet)_Workbook.Worksheets.get_Item(SheetName_);
                }
                catch
                {
                    throw new Exception(string.Format("FileName[{0}] SheetName[{1}] does not exist at FileName[{2}]", _FileName, SheetName_, _FileName));
                }

                object[,] _UsedRangeData = _Worksheet.UsedRange.Value;
                if (_UsedRangeData == null)
                    throw new Exception(string.Format("FileName[{0}] SheetName[{1}] is empty", _FileName, SheetName_));

                var RowCnt = _UsedRangeData.GetLength(0);
                var ColCnt = _UsedRangeData.GetLength(1);

                // Read Column Names ////////////////////////////////////////
                // 엑셀 컬럼을 순서대로 읽고 Value로 Index를 부여
                Int32 ColumnIndex = -1;
                for (Int32 c = 0; c < ColCnt; ++c)
                {
                    if (_UsedRangeData[1, c + 1] != null)
                    {
                        if (_UsedRangeData[1, c + 1].ToString() == ColumnName_)
                        {
                            ColumnIndex = c;
                            break;
                        }
                    }
                }

                if (ColumnIndex == -1)
                    throw new Exception(string.Format("Column is not found FileName[{0}] SheetName[{1}] ColumnName[{2}]", _FileName, SheetName_, ColumnName_));

                string Content = "";

                for (Int32 r = 1; r < RowCnt; ++r)
                {
                    string CellData = "";

                    if (_UsedRangeData[r + 1, ColumnIndex + 1] != null)
                        CellData = _UsedRangeData[r + 1, ColumnIndex + 1].ToString();

                    try
                    {
                        if (r > 1)
                            Content += ",\n";

                        Content += CellData;
                    }
                    catch (Exception Exception_)
                    {
                        throw new Exception(string.Format("Parse Error FileName[{0}] SheetName[{1}] Row[{2}] Column[{3}] Msg[{4}]", _FileName, SheetName_, r.ToString(), ColumnIndex.ToString(), Exception_.ToString()));
                    }
                }

                Directory.CreateDirectory(TargetDir_);
                File.WriteAllText(Path.GetFullPath(Path.Combine(TargetDir_, TargetFile_)), string.Format(EnumFrameFormat_, Content));
                DisposeWorksheet();
            }
            catch
            {
                Dispose();
                throw;
            }
        }
        public CStream GetStream<TProto>(string SheetName_, string[] ColumnNames_) where TProto : SProto, new()
        {
            try
            {
                try
                {
                    _Worksheet = (Worksheet)_Workbook.Worksheets.get_Item(SheetName_);
                }
                catch
                {
                    throw new Exception(string.Format("FileName[{0}] SheetName[{1}] does not exist at FileName[{2}]", _FileName, SheetName_, _FileName));
                }

                object[,] _UsedRangeData = _Worksheet.UsedRange.Value;
                if (_UsedRangeData == null)
                    throw new Exception(string.Format("FileName[{0}] SheetName[{1}] is empty", _FileName, SheetName_));

                var RowCnt = _UsedRangeData.GetLength(0);

                if (RowCnt <= _RowOffset)
                    throw new Exception(string.Format("RowCnt[{0}] <= RowOffset[{0}]", RowCnt.ToString(), _RowOffset.ToString()));

                var ColCnt = _UsedRangeData.GetLength(1);

                // Read Column Names ////////////////////////////////////////
                // 엑셀 컬럼을 순서대로 읽고 Value로 Index를 부여
                var ColumnIndices = new Dictionary<string, Int32>();
                for (Int32 c = 0; c < ColCnt; ++c)
                {
                    string ColumnName = "";

                    if (_UsedRangeData[1 + _RowOffset, c + 1] != null)
                        ColumnName = _UsedRangeData[1 + _RowOffset, c + 1].ToString();

                    if (ColumnName.Length == 0)
                        continue;

                    if (ColumnIndices.ContainsKey(ColumnName))
                        throw new Exception(string.Format("Column is duplicated FileName[{0}] SheetName[{1}] ColumnNum[{2}] ColumnName[{3}]", _FileName, SheetName_, c.ToString(), ColumnName));

                    ColumnIndices.Add(ColumnName, c);
                }


                var ExportExcelIndices = new List<Int32>(); // Export 할 컬럼의 엑셀에서의 인덱스 모음.
                foreach (var i in ColumnNames_)
                {
                    if (!ColumnIndices.ContainsKey(i))
                        throw new Exception(string.Format("Column can not be found FileName[{0}] SheetName[{1}] ColumnName[{2}]", _FileName, SheetName_, i));

                    ExportExcelIndices.Add(ColumnIndices[i]);
                }

                var Proto = new TProto();
                var SetFuncs = new List<TSetFunc>();

                foreach (var i in Proto.StdName().Split(new char[] { ',' }))
                {
                    switch (i)
                    {
                        case "bool":
                            SetFuncs.Add((CStream Stream_, string Data_) =>
                            {
                                try
                                {
                                    Stream_.Push(bool.Parse(Data_) ? true : false);
                                }
                                catch
                                {
                                    Stream_.Push(Int32.Parse(Data_) != 0 ? true : false);
                                }
                            });
                            break;
                        case "int8":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(SByte.Parse(Data_)); });
                            break;
                        case "uint8":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(Byte.Parse(Data_)); });
                            break;
                        case "int16":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(Int16.Parse(Data_)); });
                            break;
                        case "uint16":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(UInt16.Parse(Data_)); });
                            break;
                        case "int32":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(Int32.Parse(Data_)); });
                            break;
                        case "uint32":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(UInt32.Parse(Data_)); });
                            break;
                        case "int64":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(Int64.Parse(Data_)); });
                            break;
                        case "uint64":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(UInt64.Parse(Data_)); });
                            break;
                        case "float":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(float.Parse(Data_)); });
                            break;
                        case "double":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(double.Parse(Data_)); });
                            break;
                        case "string":
                        case "wstring":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(Data_); });
                            break;
                        case "time_point":
                            SetFuncs.Add((CStream Stream_, string Data_) => { Stream_.Push(new TimePoint(Int64.Parse(Data_))); });
                            break;
                        default: // enum 으로 간주
                            SetFuncs.Add((CStream Stream_, string Data_) => { _EnumValue.PushToStream(i, Data_, Stream_); });
                            break;
                    }
                }

                if (SetFuncs.Count != ExportExcelIndices.Count)
                    throw new Exception(string.Format("FileName[{0}] SheetName[{1}] Proto MemberCount[{2}] does not match export ColumnCount[{3}]", _FileName, SheetName_, SetFuncs.Count, ExportExcelIndices.Count));

                var DataStream = new CStream();
                DataStream.Push(RowCnt - 1 - _RowOffset);

                for (Int32 r = 1 + _RowOffset; r < RowCnt; ++r)
                {
                    for (Int32 i = 0; i < ExportExcelIndices.Count; ++i)
                    {
                        string CellData = "";

                        if (_UsedRangeData[r + 1, ExportExcelIndices[i] + 1] != null)
                            CellData = _UsedRangeData[r + 1, ExportExcelIndices[i] + 1].ToString();

                        try
                        {
                            SetFuncs[i](DataStream, CellData);
                        }
                        catch (Exception Exception_)
                        {
                            throw new Exception(string.Format("Parse Error FileName[{0}] SheetName[{1}] Row[{2}] Column[{3}] Msg[{4}]", _FileName, SheetName_, r.ToString(), ExportExcelIndices[i].ToString(), Exception_.ToString()));
                        }
                    }
                }

                DisposeWorksheet();
                return DataStream;
            }
            catch (Exception Exception_)
            {
                Dispose();
                Console.WriteLine(Exception_.ToString());
                throw;
            }
        }
        public CStream GetStream<TProto>(string SheetName_) where TProto : SProto, new()
        {
            return GetStream<TProto>(SheetName_, new TProto().MemberName().Split(new char[] { ',' }));
        }
        public UInt64 Export<TProto>(string SheetName_, string TargetDir_, string[] ColumnNames_) where TProto : SProto, new()
        {
            var Stream = GetStream<TProto>(SheetName_, ColumnNames_);
            Stream.SaveFile(Path.Combine(TargetDir_, SheetName_ + "." + _TargetExtension));
            return Stream.CheckSum();
        }
        public UInt64 Export<TProto>(string SheetName_, string TargetDir_) where TProto : SProto, new()
        {
            return Export<TProto>(SheetName_, TargetDir_, new TProto().MemberName().Split(new char[] { ',' }));
        }
    }
}
