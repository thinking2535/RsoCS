using System;
using System.Threading;

namespace rso
{
    namespace util
    {
        public class CGesture
        {
            //D : Down
            //T : Tap
            //DD : DoubleDown
            //DT : DoubleTap
            //LDD : LongDoubleDown
            //LDT : LongDoubleTap
            //LD : LongDown
            //LT : LongTap
            //Dg  : Drag
            //DDg : DoubleDrag

            //D	+ T	- DD	+ DT
            //	|			+		- LDD	- LDT
            //	+			|		- LD	- LT
            //	|			+ DDg
            //	+ Dg




            //            public enum EAction
            //            {
            //                Touch,
            //                Tap,
            //                DoubleTouch,
            //                DoubleTap,
            //                LongDoubleTouch,
            //                LongDoubleTap,
            //                LongTouch,
            //                LongTap,
            //                DragBegin,
            //                DragEnd,
            //                Max,
            //                Null
            //            }
            //            public delegate void TCallback(Int32 X_, Int32 Y_, EAction Action_);
            //            Int32 _ShortDelay = 300;
            //            Int32 _LongDelay = 500;
            //            Int32 _PosError = 10;
            //            TCallback _Callback;
            //            Int32 _LastX = 0;
            //            Int32 _LastY = 0;
            //            EAction _LastAction = EAction.Null;
            //            Int32 _LastActionTick = Environment.TickCount;

            //            public CGesture(TCallback Callback_)
            //            {
            //                _Callback = Callback_;
            //            }
            //            public CGesture(Int32 ShortDelay_, Int32 LongDelay_, Int32 PosError_, TCallback Callback_) // DoubleDelay + LongDelay == LongDoubleDelay
            //            {
            //                _ShortDelay = ShortDelay_;
            //                _LongDelay = LongDelay_;
            //                _PosError = PosError_;
            //                _Callback = Callback_;
            //            }
            //            public EAction Touch(Int32 X_, Int32 Y_)
            //            {
            //                // DD  및 DT 의 오차는 Touch,Up간, 다시 Up과 Touch간의 오차가 모두 PosError_ 이내

            //                _Callback 호출할것.

            //                    DragMode 현재 들어온 X_, Y_ 가 아닌 _LastX, _LastY 를 콜백할것.



            //                var NowTick = Environment.TickCount;

            //                switch (_LastAction)
            //                {
            //                    case EAction.Touch: // 여기 와서는 안됨
            //                    case EAction.Tap:
            //                        if (NowTick - _LastActionTick < _ShortDelay)
            //                            _LastAction = EAction.DoubleTouch;
            //                        else
            //                            _LastAction = EAction.Touch;
            //                        break;


            //                        아래 작업할것.
            //                    case EAction.LongDoubleTap: break;
            //                    case EAction.LongTap: break;
            //                        ///////////////////////




            //                    default:
            //                        _LastAction = EAction.Touch;
            //                        break;
            //                }

            //                _LastActionTick = NowTick;

            //                return _LastAction;
            //            }
            //            public EAction Up(Int32 X_, Int32 Y_)
            //            {
            //                var NowTick = Environment.TickCount;

            //                switch (_LastAction)
            //                {
            //                    작업중
            //                    case EAction.Null: break;
            //                    case EAction.Touch: break;
            //                    case EAction.Tap: break;
            //                    case EAction.DoubleTouch: break;
            //                    case EAction.DoubleTap: break;
            //                    case EAction.LongDoubleTouch: break;
            //                    case EAction.LongDoubleTap: break;
            //                    case EAction.LongTouch: break;
            //                    case EAction.LongTap: break;
            //            }

            //            _LastActionTick = NowTick; 액션이 바뀐경우 _LastActionTick 를 갱신할것.

            //                return _LastAction;
            //            }
            //            public void Move(Int32 X_, Int32 Y_)
            //            {
            //                var NowTick = Environment.TickCount;

            //                switch (_LastAction)
            //                {
            //                    PosError 체크
            //                    case EAction.Null: break;
            //                    case EAction.Touch: break;
            //                    case EAction.Tap: break;
            //                    case EAction.DoubleTouch: break;
            //                    case EAction.DoubleTap: break;
            //                    case EAction.LongDoubleTouch: break;
            //                    case EAction.LongDoubleTap: break;
            //                    case EAction.LongTouch: break;
            //                    case EAction.LongTap: break;
            //        }
            //    }
            //            public void Proc()
            //            {
            //                switch (_LastAction)
            //                {
            //                    LongAction Check
            //                    case EAction.Null: break;
            //                    case EAction.Touch: break;
            //                    case EAction.Tap: break;
            //                    case EAction.DoubleTouch: break;
            //                    case EAction.DoubleTap: break;
            //                    case EAction.LongDoubleTouch: break;
            //                    case EAction.LongDoubleTap: break;
            //                    case EAction.LongTouch: break;
            //                    case EAction.LongTap: break;
            //    }
            //}
        }
    }
}