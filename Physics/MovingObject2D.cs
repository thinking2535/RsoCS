using System;
using System.Collections.Generic;

namespace rso.physics
{
    public class CMovingObject2D : CObject2D
    {
        public List<CCollider2D> Colliders { get; private set; } = new List<CCollider2D>();
        public SPoint Velocity;
        public float Mass = 1.0f;
        public bool isKinematic = false;

        public CMovingObject2D(STransform Transform_, List<CCollider2D> Colliders_, SPoint Velocity_) :
            base(Transform_)
        {
            Colliders = Colliders_;
            Velocity = Velocity_;

            foreach (var c in Colliders)
                c.SetParent(this);
        }
        public virtual bool isPlayerObject()
        {
            return false;
        }
        public virtual bool Collided(Int64 tick, SCollision2D Collision_)
        {
            return false;
        }
        public virtual bool Triggered(Int64 tick, CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
            return false;
        }
        public virtual void NotOverlapped(Int64 tick, CCollider2D Collider_, CCollider2D OtherCollider_)
        {
        }
        public virtual void NotOverlapped(Int64 tick, CCollider2D OtherCollider_)
        {
        }

        public Action<Int64> fFixedUpdate;
    }
}
