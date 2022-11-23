using System;

namespace rso.physics
{
    public class CRectCollider2D : CCollider2D
    {
        static SPoint _getRelativeVelocity(CMovingObject2D movingObject)
        {
            return movingObject.Velocity.GetMulti(-1.0f);
        }
        static SPoint _getRelativeVelocity(CMovingObject2D movingObject, CMovingObject2D otherMovingObject)
        {
            return otherMovingObject.Velocity.GetSub(movingObject.Velocity);
        }

        SRectCollider2D _Collider = null;
        public SRect getRect()
        {
            return _Collider.ToRect().Multi(LocalScale).Add(GetPosition());
        }

        public CRectCollider2D(STransform Transform_, Int32 Number_, SRectCollider2D Collider_) :
            base(Transform_, Number_)
        {
            _Collider = Collider_;
        }
        public CRectCollider2D(STransform Transform_, Int32 Number_, SRectCollider2D Collider_, CObject2D Parent_) :
            base(Transform_, Parent_, Number_)
        {
            _Collider = Collider_;
        }
        public void SetSize(SPoint Size_)
        {
            _Collider.Size.Set(Size_);
        }
        public void SetSizeX(float X_)
        {
            _Collider.Size.X = X_;
        }
        public void SetSizeY(float Y_)
        {
            _Collider.Size.Y = Y_;
        }
        public override bool checkOverlapped(Int64 tick, CMovingObject2D movingObject, CCollider2D otherCollider)
        {
            return otherCollider.checkOverlapped(tick, this, movingObject);
        }
        public override bool checkOverlapped(Int64 tick, CMovingObject2D movingObject, CCollider2D otherCollider, CMovingObject2D otherMovingObject)
        {
            return otherCollider.checkOverlapped(tick, otherMovingObject, this, movingObject);
        }
        public override bool checkOverlapped(Int64 tick, CMovingObject2D movingObject, CRectCollider2D otherRectCollider)
        {
            var rect = getRect();
            var otherRect = otherRectCollider.getRect();

            if (!_isOverlapped(rect, otherRectCollider, otherRect))
            {
                movingObject.NotOverlapped(tick, this, otherRectCollider);
                return false;
            }

            if (IsTrigger || otherRectCollider.IsTrigger)
            {
                return movingObject.Triggered(tick, this, otherRectCollider, null);
            }
            else
            {
                if (!movingObject.isKinematic)
                {
                    var relativeVelocity = _getRelativeVelocity(movingObject);
                    var normal = _fixPositionAndGetNormal(relativeVelocity, rect, movingObject, otherRect);

                    return movingObject.Collided(
                        tick,
                        new SCollision2D(
                            relativeVelocity,
                            normal,
                            this,
                            otherRectCollider,
                            null));
                }
            }

            return false;
        }
        public override bool checkOverlapped(Int64 tick, CRectCollider2D otherRectCollider, CMovingObject2D otherMovingObject)
        {
            return otherRectCollider.checkOverlapped(tick, otherMovingObject, this);
        }
        public override bool checkOverlapped(Int64 tick, CMovingObject2D movingObject, CRectCollider2D otherRectCollider, CMovingObject2D otherMovingObject)
        {
            var rect = getRect();
            var otherRect = otherRectCollider.getRect();

            if (!_isOverlapped(rect, otherRectCollider, otherRect))
            {
                movingObject.NotOverlapped(tick, this, otherRectCollider);
                otherMovingObject.NotOverlapped(tick, otherRectCollider, this);
                return false;
            }

            if (IsTrigger || otherRectCollider.IsTrigger)
            {
                otherMovingObject.Triggered(tick, otherRectCollider, this, movingObject);
                return movingObject.Triggered(tick, this, otherRectCollider, otherMovingObject);
            }
            else
            {
                if (!movingObject.isKinematic && !otherMovingObject.isKinematic)
                {
                    var relativeVelocity = _getRelativeVelocity(movingObject, otherMovingObject);
                    var normal = _fixPositionAndGetNormal(relativeVelocity, rect, movingObject, otherRect, otherMovingObject);

                    otherMovingObject.Collided(
                        tick,
                        new SCollision2D(
                            relativeVelocity.GetMulti(-1.0f),
                            normal.GetMulti(-1.0f),
                            otherRectCollider,
                            this,
                            movingObject));

                    return movingObject.Collided(
                        tick,
                        new SCollision2D(
                            relativeVelocity,
                            normal,
                            this,
                            otherRectCollider,
                            otherMovingObject));
                }
                else
                {
                    if (!movingObject.isKinematic)
                    {
                        var relativeVelocity = _getRelativeVelocity(movingObject, otherMovingObject);
                        var normal = _fixPositionAndGetNormal(relativeVelocity, rect, movingObject, otherRect, otherMovingObject);

                        return movingObject.Collided(
                            tick,
                            new SCollision2D(
                                relativeVelocity,
                                normal,
                                this,
                                otherRectCollider,
                                otherMovingObject));
                    }
                    else if (!otherMovingObject.isKinematic)
                    {
                        var relativeVelocity = _getRelativeVelocity(otherMovingObject, movingObject);
                        var normal = _fixPositionAndGetNormal(relativeVelocity, otherRect, otherMovingObject, rect, movingObject);

                        return otherMovingObject.Collided(
                            tick,
                            new SCollision2D(
                                relativeVelocity,
                                normal,
                                otherRectCollider,
                                this,
                                movingObject));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        bool _isOverlapped(SRect rect, CRectCollider2D otherRectCollider, SRect otherRect)
        {
            return GetEnabled() && otherRectCollider.GetEnabled() && CPhysics.IsOverlappedRectRect(rect, otherRect);
        }
        SPoint _fixPositionAndGetNormal(SPoint relativeVelocity, SRect rect, CMovingObject2D movingObject, SRect otherRect)
        {
            var rl = otherRect.Right - rect.Left;
            var lr = rect.Right - otherRect.Left;
            var tb = otherRect.Top - rect.Bottom;
            var bt = rect.Top - otherRect.Bottom;

            if (rl < lr) // Normal.X : +
            {
                if (tb < bt) // Normal.Y : +
                {
                    if (rl < tb) // select Normal.X
                    {
                        if (rl > CEngine.ContactOffset)
                            _moveAwayX(rl - CEngine.ContactOffset, movingObject);

                        return _inelasticCollisionX(new SPoint(1.0f, 0.0f), relativeVelocity, movingObject);
                    }
                    else // select Normal.Y
                    {
                        if (tb > CEngine.ContactOffset)
                            _moveAwayY(tb - CEngine.ContactOffset, movingObject);

                        return _inelasticCollisionY(new SPoint(0.0f, 1.0f), relativeVelocity, movingObject);
                    }
                }
                else // Normal.Y : -
                {
                    if (rl < bt) // select Normal.X
                    {
                        if (rl > CEngine.ContactOffset)
                            _moveAwayX(rl - CEngine.ContactOffset, movingObject);

                        return _inelasticCollisionX(new SPoint(1.0f, 0.0f), relativeVelocity, movingObject);
                    }
                    else // select Normal.Y
                    {
                        if (bt > CEngine.ContactOffset)
                            _moveAwayY(CEngine.ContactOffset - bt, movingObject);

                        return _inelasticCollisionY(new SPoint(0.0f, -1.0f), relativeVelocity, movingObject);
                    }
                }
            }
            else // Normal.X : -
            {
                if (tb < bt) // Normal.Y : +
                {
                    if (lr < tb) // select Normal.X
                    {
                        if (lr > CEngine.ContactOffset)
                            _moveAwayX(CEngine.ContactOffset - lr, movingObject);

                        return _inelasticCollisionX(new SPoint(-1.0f, 0.0f), relativeVelocity, movingObject);
                    }
                    else // select Normal.Y
                    {
                        if (tb > CEngine.ContactOffset)
                            _moveAwayY(tb - CEngine.ContactOffset, movingObject);

                        return _inelasticCollisionY(new SPoint(0.0f, 1.0f), relativeVelocity, movingObject);
                    }
                }
                else // Normal.Y : -
                {
                    if (lr < bt) // select Normal.X
                    {
                        if (lr > CEngine.ContactOffset)
                            _moveAwayX(CEngine.ContactOffset - lr, movingObject);

                        return _inelasticCollisionX(new SPoint(-1.0f, 0.0f), relativeVelocity, movingObject);
                    }
                    else // select Normal.Y
                    {
                        if (bt > CEngine.ContactOffset)
                            _moveAwayY(CEngine.ContactOffset - bt, movingObject);

                        return _inelasticCollisionY(new SPoint(0.0f, -1.0f), relativeVelocity, movingObject);
                    }
                }
            }
        }
        SPoint _fixPositionAndGetNormal(SPoint relativeVelocity, SRect rect, CMovingObject2D movingObject, SRect otherRect, CMovingObject2D otherMovingObject)
        {
            var rl = otherRect.Right - rect.Left;
            var lr = rect.Right - otherRect.Left;
            var tb = otherRect.Top - rect.Bottom;
            var bt = rect.Top - otherRect.Bottom;

            if (rl < lr) // Normal.X : +
            {
                if (tb < bt) // Normal.Y : +
                {
                    if (rl < tb) // select Normal.X
                    {
                        if (rl > CEngine.ContactOffset)
                            _moveAwayX(rl - CEngine.ContactOffset, movingObject, otherMovingObject);

                        return _inelasticCollisionX(new SPoint(1.0f, 0.0f), relativeVelocity, movingObject, otherMovingObject);
                    }
                    else // select Normal.Y
                    {
                        if (tb > CEngine.ContactOffset)
                            _moveAwayY(tb - CEngine.ContactOffset, movingObject, otherMovingObject);

                        return _inelasticCollisionY(new SPoint(0.0f, 1.0f), relativeVelocity, movingObject, otherMovingObject);
                    }
                }
                else // Normal.Y : -
                {
                    if (rl < bt) // select Normal.X
                    {
                        if (rl > CEngine.ContactOffset)
                            _moveAwayX(rl - CEngine.ContactOffset, movingObject, otherMovingObject);

                        return _inelasticCollisionX(new SPoint(1.0f, 0.0f), relativeVelocity, movingObject, otherMovingObject);
                    }
                    else // select Normal.Y
                    {
                        if (bt > CEngine.ContactOffset)
                            _moveAwayY(CEngine.ContactOffset - bt, movingObject, otherMovingObject);

                        return _inelasticCollisionY(new SPoint(0.0f, -1.0f), relativeVelocity, movingObject, otherMovingObject);
                    }
                }
            }
            else // Normal.X : -
            {
                if (tb < bt) // Normal.Y : +
                {
                    if (lr < tb) // select Normal.X
                    {
                        if (lr > CEngine.ContactOffset)
                            _moveAwayX(CEngine.ContactOffset - lr, movingObject, otherMovingObject);

                        return _inelasticCollisionX(new SPoint(-1.0f, 0.0f), relativeVelocity, movingObject, otherMovingObject);
                    }
                    else // select Normal.Y
                    {
                        if (tb > CEngine.ContactOffset)
                            _moveAwayY(tb - CEngine.ContactOffset, movingObject, otherMovingObject);

                        return _inelasticCollisionY(new SPoint(0.0f, 1.0f), relativeVelocity, movingObject, otherMovingObject);
                    }
                }
                else // Normal.Y : -
                {
                    if (lr < bt) // select Normal.X
                    {
                        if (lr > CEngine.ContactOffset)
                            _moveAwayX(CEngine.ContactOffset - lr, movingObject, otherMovingObject);

                        return _inelasticCollisionX(new SPoint(-1.0f, 0.0f), relativeVelocity, movingObject, otherMovingObject);
                    }
                    else // select Normal.Y
                    {
                        if (bt > CEngine.ContactOffset)
                            _moveAwayY(CEngine.ContactOffset - bt, movingObject, otherMovingObject);

                        return _inelasticCollisionY(new SPoint(0.0f, -1.0f), relativeVelocity, movingObject, otherMovingObject);
                    }
                }
            }
        }
        static void _moveAwayX(float overlapped, CMovingObject2D movingObject)
        {
            movingObject.LocalPosition.X += overlapped;
        }
        static void _moveAwayY(float overlapped, CMovingObject2D movingObject)
        {
            movingObject.LocalPosition.Y += overlapped;
        }
        static void _moveAwayX(float overlapped, CMovingObject2D movingObject, CMovingObject2D otherMovingObject)
        {
            if (otherMovingObject.isKinematic)
            {
                _moveAwayX(overlapped, movingObject);
                return;
            }

            movingObject.LocalPosition.X += (overlapped * otherMovingObject.Mass / (movingObject.Mass + otherMovingObject.Mass));
            otherMovingObject.LocalPosition.X -= (overlapped * movingObject.Mass / (movingObject.Mass + otherMovingObject.Mass));
        }
        static void _moveAwayY(float overlapped, CMovingObject2D movingObject, CMovingObject2D otherMovingObject)
        {
            if (otherMovingObject.isKinematic)
            {
                _moveAwayY(overlapped, movingObject);
                return;
            }

            movingObject.LocalPosition.Y += (overlapped * otherMovingObject.Mass / (movingObject.Mass + otherMovingObject.Mass));
            otherMovingObject.LocalPosition.Y -= (overlapped * movingObject.Mass / (movingObject.Mass + otherMovingObject.Mass));
        }
        static SPoint _inelasticCollisionX(SPoint normal, SPoint relativeVelocity, CMovingObject2D movingObject)
        {
            if (normal.X * relativeVelocity.X > 0.0f)
                movingObject.Velocity.X = 0.0f;

            return normal;
        }
        static SPoint _inelasticCollisionY(SPoint normal, SPoint relativeVelocity, CMovingObject2D movingObject)
        {
            if (normal.Y * relativeVelocity.Y > 0.0f)
                movingObject.Velocity.Y = 0.0f;

            return normal;
        }
        static SPoint _inelasticCollisionX(SPoint normal, SPoint relativeVelocity, CMovingObject2D movingObject, CMovingObject2D otherMovingObject)
        {
            if (normal.X * relativeVelocity.X > 0.0f)
            {
                if (otherMovingObject.isKinematic)
                    movingObject.Velocity.X = otherMovingObject.Velocity.X;
                else
                    movingObject.Velocity.X = otherMovingObject.Velocity.X = (movingObject.Velocity.X * movingObject.Mass + otherMovingObject.Velocity.X * otherMovingObject.Mass) / (movingObject.Mass + otherMovingObject.Mass);
            }

            return normal;
        }
        static SPoint _inelasticCollisionY(SPoint normal, SPoint relativeVelocity, CMovingObject2D movingObject, CMovingObject2D otherMovingObject)
        {
            if (normal.Y * relativeVelocity.Y > 0.0f)
            {
                if (otherMovingObject.isKinematic)
                    movingObject.Velocity.Y = otherMovingObject.Velocity.Y;
                else
                    movingObject.Velocity.Y = otherMovingObject.Velocity.Y = (movingObject.Velocity.Y * movingObject.Mass + otherMovingObject.Velocity.Y * otherMovingObject.Mass) / (movingObject.Mass + otherMovingObject.Mass);
            }

            return normal;
        }
    }
}
