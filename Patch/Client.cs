using rso.core;
using rso.http;
using rso.net;
using System;
using System.Collections.Generic;
using System.IO;

namespace rso
{
    namespace patch
    {
        using TPeerCnt = UInt32;
        public delegate void TDownloadChangedFunc(string ObjectName_, Int64 Received_, Int64 Total_);
        public delegate void TDownloadCompletedFunc(EHttpRet Ret_, string ObjectName_);
        public class CClient : CClientCore
        {
            class _SPatchDataCount
            {
                public SPatchData PatchData = null;
                public Int32 LeftCount = 0;

                public _SPatchDataCount(SPatchData PatchData_, Int32 LeftCount_)
                {
                    PatchData = PatchData_;
                    LeftCount = LeftCount_;
                }
            }
            CHttp _Http;
            string _ServerName = "";
            bool _VersionPath;
            Queue<_SPatchDataCount> _PatchDatas = new Queue<_SPatchDataCount>();
            TLinkFunc _LinkFunc;
            TDownloadChangedFunc _DownloadChangedFunc;
            TDownloadCompletedFunc _DownloadCompletedFunc;
            balance.CClient _NetB;

            void _LinkS(CKey Key_)
            {
                _NetB.Send(Key_.PeerNum, new SHeader(EProto.CsLogin), new SCsLogin(_Data.Data.Version));
                _LinkFunc(Key_);
            }
            void _RecvS(CKey Key_, CStream Stream_)
            {
                var Header = new SHeader();
                Stream_.Pop(ref Header);

                try
                {
                    switch (Header.Proto)
                    {
                        case EProto.ScPatchData:
                            _RecvScPatchData(Key_, Stream_);
                            return;
                        default:
                            throw new Exception();
                    }
                }
                catch
                {
                    _NetB.Close(Key_.PeerNum);
                }
            }
            void _RecvScPatchData(CKey Key_, CStream Stream_)
            {
                var Proto = new SPatchData();
                Stream_.Pop(ref Proto);

                _Patch(Proto);
                _NetB.CloseAll();
            }
            void _DownloadChanged(Int32 SessionIndex_, string ObjectName_, Int64 Received_, Int64 Total_)
            {
                _DownloadChangedFunc(ObjectName_, Received_, Total_);
            }
            void _DownloadCompleted(Int32 SessionIndex_, EHttpRet Ret_, string ObjectName_, Byte[] Buffer_)
            {
                if (Ret_ != EHttpRet.Ok)
                {
                    _DownloadCompletedFunc(Ret_, ObjectName_);
                    return;
                }

                var FullPath = Path.GetFullPath(_DataPathFull + ObjectName_);
                Directory.CreateDirectory(Path.GetDirectoryName(FullPath));
                File.WriteAllBytes(FullPath, Buffer_);

                _DownloadCompletedFunc(Ret_, ObjectName_);

                --_PatchDatas.Peek().LeftCount;

                if (_PatchDatas.Peek().LeftCount == 0)
                {
                    _PatchCore(_PatchDatas.Peek().PatchData);
                    _PatchDatas.Dequeue();
                }
            }
            protected void _Patch(SPatchData Data_)
            {
                if (_Data.Data.Version.Main != Data_.Version.Main)
                {
                    var di = new DirectoryInfo(_DataPathFull);

                    foreach (var file in di.GetFiles())
                        file.Delete();

                    foreach (var dir in di.GetDirectories())
                        dir.Delete(true);
                }

                Int32 LeftCount = 0;

                foreach (var i in Data_.Files)
                {
                    if (i.Value.IsAdded)
                    {
                        string ObjectName = (_VersionPath ? (i.Value.SubVersion.ToString() + "/") : "");
                        ObjectName += i.Key;
                        _Http.Push(new SInObj(_ServerName, ObjectName.Replace('\\', '/')));
                        ++LeftCount;
                    }
                    else
                    {
                        File.Delete(_DataPathFull.Combine(i.Key));
                    }
                }

                if (LeftCount > 0)
                    _PatchDatas.Enqueue(new _SPatchDataCount(Data_, LeftCount));
            }

            public CClient(
                string FileName_, string ServerName_, bool VersionPath_, string DataPath_,
                TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_,
                TDownloadChangedFunc DownloadChangedFunc_, TDownloadCompletedFunc DownloadCompletedFunc_) :
                base(FileName_, DataPath_)
            {
                _Http = new CHttp(1, _DownloadChanged, _DownloadCompleted);
                _ServerName = ServerName_;
                _VersionPath = VersionPath_;
                _LinkFunc = LinkFunc_;
                _DownloadChangedFunc = DownloadChangedFunc_;
                _DownloadCompletedFunc = DownloadCompletedFunc_;
                _NetB = new balance.CClient(_LinkS, LinkFailFunc_, UnLinkFunc_, _RecvS);
            }
            public CKey Connect(TPeerCnt PeerNum_, string DataPath_, CNamePort MasterNamePort_)
            {
                return _NetB.Connect(PeerNum_, DataPath_, MasterNamePort_);
            }
            public CKey Connect(string DataPath_, CNamePort MasterNamePort_)
            {
                return _NetB.Connect(DataPath_, MasterNamePort_);
            }
            public void Proc()
            {
                _Http.Proc();
                _NetB.Proc();
            }
            public void Dispose()
            {
                _Http.Dispose();
            }
        }
    }
}
