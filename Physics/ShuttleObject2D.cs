using System;
using System.Collections.Generic;

namespace rso.physics
{
    public class CShuttleObject2D : CMovingObject2D
    {
        SPoint _BeginPos;
        SPoint _EndPos;
        Single _Velocity;
        Single _Delay;
        SStructMove _StructMove;

        public CShuttleObject2D(STransform Transform_, SPoint BeginPos_, SPoint EndPos_, Single Velocity_, Single Delay_, SStructMove StructMove_) :
            base(Transform_, new SPoint())
        {
            fFixedUpdate = _FixedUpdate;
            _BeginPos = BeginPos_;
            _EndPos = EndPos_;
            _Velocity = Velocity_;
            _Delay = Delay_;
            _StructMove = StructMove_;
        }
        public void _FixedUpdate(Int64 Tick_)
        {
            if (_StructMove.IsMoving)
            {
                if (_StructMove.Direction == 1)
                {
                    if (CPhysics.MoveTowards(LocalPosition, _EndPos, _Velocity * CEngine.DeltaTime))
                    {
                        _StructMove.Direction = -1;
                        _StructMove.IsMoving = false;
                        Velocity.Clear();
                    }
                }
                else
                {
                    if (CPhysics.MoveTowards(LocalPosition, _BeginPos, _Velocity * CEngine.DeltaTime))
                    {
                        _StructMove.Direction = 1;
                        _StructMove.IsMoving = false;
                        Velocity.Clear();
                    }
                }
            }
            else
            {
                _StructMove.StoppedDuration += CEngine.DeltaTime;

                if (_StructMove.StoppedDuration >= _Delay)
                {
                    _StructMove.StoppedDuration = 0.0f;
                    _StructMove.IsMoving = true;

                    var Vector = _EndPos.GetSub(_BeginPos).GetMulti(_StructMove.Direction);
                    Velocity = Vector.Multi(_Velocity / Vector.GetScalar());
                }
            }
        }
    }
}