using rso.unity;
using UnityEngine;

namespace rso.unity
{
    public class CPadSimulator
    {
#if UNITY_EDITOR
        readonly CInputTouch.FPad _fTouched;
        readonly CInputTouch.FButton _fPushed;
        readonly CInputKey _InputKey;
        readonly Vector2 _PadPosCenter;
        void _Callback(KeyCode KeyCode_, bool Down_)
        {
            switch (KeyCode_)
            {
                case KeyCode.A:
                    {
                        if (Down_)
                        {
                            if (_InputKey.Get(KeyCode.D))
                            {
                                _fTouched(CInputTouch.EState.Up, _PadPosCenter, -1);
                            }
                            else
                            {
                                _fTouched(CInputTouch.EState.Down, _PadPosCenter, -1);
                                _fTouched(CInputTouch.EState.Move, _PadPosCenter, 0);
                            }
                        }
                        else
                        {
                            if (_InputKey.Get(KeyCode.D))
                            {
                                _fTouched(CInputTouch.EState.Down, _PadPosCenter, -1);
                                _fTouched(CInputTouch.EState.Move, _PadPosCenter, 1);
                            }
                            else
                            {
                                _fTouched(CInputTouch.EState.Up, _PadPosCenter, -1);
                            }
                        }
                    }
                    break;
                case KeyCode.D:
                    {
                        if (Down_)
                        {
                            if (_InputKey.Get(KeyCode.A))
                            {
                                _fTouched(CInputTouch.EState.Up, _PadPosCenter, -1);
                            }
                            else
                            {
                                _fTouched(CInputTouch.EState.Down, _PadPosCenter, -1);
                                _fTouched(CInputTouch.EState.Move, _PadPosCenter, 1);
                            }
                        }
                        else
                        {
                            if (_InputKey.Get(KeyCode.A))
                            {
                                _fTouched(CInputTouch.EState.Down, _PadPosCenter, -1);
                                _fTouched(CInputTouch.EState.Move, _PadPosCenter, 0);
                            }
                            else
                            {
                                _fTouched(CInputTouch.EState.Up, _PadPosCenter, -1);
                            }
                        }
                    }
                    break;
                case KeyCode.Space:
                case KeyCode.Return:
                    {
                        if (Down_)
                            _fPushed(CInputTouch.EState.Down);
                    }
                    break;
            }
        }
        public CPadSimulator(CInputTouch.FPad fTouched_, CInputTouch.FButton fPushed_, float StandbyRange_, float ActiveRange_, bool Tracking_)
        {
            _fTouched = fTouched_;
            _fPushed = fPushed_;
            _InputKey = new CInputKey(_Callback);
            _InputKey.Add(KeyCode.A);
            _InputKey.Add(KeyCode.D);
            _InputKey.Add(KeyCode.Space);
            _InputKey.Add(KeyCode.Return);
            _PadPosCenter = new Vector2(Screen.width * 0.25f, Screen.height * 0.5f);
        }
        public void Update()
        {
            _InputKey.Update();
        }
#else
        CInputTouch _InputTouch = new CInputTouch();
        public CPadSimulator(CInputTouch.FPad fTouched_, CInputTouch.FButton fPushed_, float StandbyRange_, float ActiveRange_, bool Tracking_)
        {
            _InputTouch.Add(new CInputTouch.CObjectPad((Vector2 Pos_) => { return Pos_.x < Screen.width * 0.5f; }, fTouched_, StandbyRange_, ActiveRange_, Tracking_, 1));
            _InputTouch.Add(new CInputTouch.CObjectButton((Vector2 Pos_) => { return Pos_.x >= Screen.width * 0.5f; }, fPushed_));
        }
        public void Update()
        {
            _InputTouch.Update();
        }
#endif
    }
}
