using rso.Base;
using rso.core;
using System;
using System.IO;

namespace rso
{
    namespace patch
    {
        public class CClientCore
        {
            protected COptionStream<SPatchData> _Data;
            protected string _DataPathFull;

            protected void _PatchCore(SPatchData Data_)
            {
                if (_Data.Data.Version.Main != Data_.Version.Main ||
                    _Data.Data.Version.Sub > Data_.Version.Sub)
                {
                    _Data.Data = Data_;
                }
                else
                {
                    _Data.Data.Version.Sub = Data_.Version.Sub;

                    foreach (var i in Data_.Files)
                    {
                        if (i.Value.IsAdded)
                            _Data.Data.Files.Add(i.Key, i.Value);
                        else
                            _Data.Data.Files.Remove(i.Key);
                    }
                }

                _Data.Save();
            }
            public CClientCore(string FileName_, string DataPath_)
            {
                _Data = new COptionStream<SPatchData>(FileName_, true);
                _DataPathFull = Environment.CurrentDirectory.Combine(DataPath_);
                Directory.CreateDirectory(Path.GetFullPath(_DataPathFull));
            }
            public SPatchData GetData()
            {
                return _Data.Data;
            }
            public string GetDataPathFull()
            {
                return _DataPathFull;
            }
        }
    }
}
