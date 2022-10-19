using System;
using System.Collections.Generic;
using System.Linq;

namespace rso.physics
{
    using TContactPoint2Ds = Dictionary<SContactPoint2D, CMovingObject2D>;
    public struct SContactPoint2D
    {
        public CCollider2D Collider;
        public CCollider2D OtherCollider;

        public SContactPoint2D(CCollider2D Collider_, CCollider2D OtherCollider_)
        {
            Collider = Collider_;
            OtherCollider = OtherCollider_;
        }
    }
    public class CPlayerObject2D : CMovingObject2D
    {
        public delegate bool FCollision(Int64 tick, SCollision2D Collision_);
        public delegate bool FTrigger(CCollider2D Collider_);

        public FCollision fCollisionEnter;
        public FCollision fCollisionStay;
        public FCollision fCollisionExit;
        public FTrigger fTriggerEnter;
        public FTrigger fTriggerStay;
        public FTrigger fTriggerExit;

        // key�� SContactPoint2D���� ���� ������ �������� Collider�� ������ ��ü�� OnCollisionStay ����
        // ���� ó������ ��� _ContactPoint2Ds �� ���Ͽ� ���Ǿ�� �ϰ�,
        // FixedUpdate�� �ѹ� ���Ǿ�� �ϱ� ������ _ContactPoint2Ds �� CMovingObject2D �� �����ؾ��ϰ�
        // �ټ��� Collider�� ������ CMovingObject2D �� �ٴ�ٷ� Contact�� �Ͼ�� ������
        TContactPoint2Ds _ContactPoint2Ds = new TContactPoint2Ds();
        public CPlayerObject2D(STransform Transform_, List<CCollider2D> Colliders_, SPoint Velocity_) :
            base(Transform_, Colliders_, Velocity_)
        {
        }
        public override CPlayerObject2D GetPlayerObject2D()
        {
            return this;
        }
        public bool CheckOverlapped(Int64 tick, CCollider2D OtherCollider_)
        {
            foreach (var c in Colliders)
            {
                if (c.CheckOverlapped(tick, this, OtherCollider_, null))
                    return true;
            }

            return false;
        }
        public bool CheckOverlapped(Int64 tick, CMovingObject2D OtherMovingObject_)
        {
            foreach (var c in Colliders)
            {
                foreach (var o in OtherMovingObject_.Colliders)
                {
                    if (c.CheckOverlapped(tick, this, o, OtherMovingObject_))
                        return true;
                }
            }

            return false;
        }
        public bool Collided(Int64 tick, SCollision2D Collision_)
        {
            var Key = new SContactPoint2D(Collision_.Collider, Collision_.OtherCollider);
            if (!_ContactPoint2Ds.ContainsKey(Key))
            {
                _ContactPoint2Ds.Add(Key, Collision_.OtherMovingObject);

                if (fCollisionEnter != null)
                    return fCollisionEnter.Invoke(tick, Collision_);
                else
                    return false;
            }

            // �� ���� �� ��ü�� ���� �ٴ� ���������� �ӵ��� ������ ���ϵ���
            var OtherVelocity = Collision_.OtherMovingObject == null ? new SPoint() : Collision_.OtherMovingObject.Velocity;
            if (Collision_.Normal.X > 0.0f)
            {
                if (Velocity.X < OtherVelocity.X)
                    Velocity.X = OtherVelocity.X;
            }
            else if (Collision_.Normal.X < 0.0f)
            {
                if (Velocity.X > OtherVelocity.X)
                    Velocity.X = OtherVelocity.X;
            }
            else if (Collision_.Normal.Y > 0.0f)
            {
                if (Velocity.Y < OtherVelocity.Y)
                    Velocity.Y = OtherVelocity.Y;
            }
            else if (Collision_.Normal.Y < 0.0f)
            {
                if (Velocity.Y > OtherVelocity.Y)
                    Velocity.Y = OtherVelocity.Y;
            }

            if (fCollisionStay != null)
                return fCollisionStay.Invoke(tick, Collision_);
            else
                return false;
        }
        public bool Triggered(CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
            var Key = new SContactPoint2D(Collider_, OtherCollider_);
            if (!_ContactPoint2Ds.ContainsKey(Key))
            {
                _ContactPoint2Ds.Add(Key, OtherMovingObject_);

                if (fTriggerEnter != null)
                    return fTriggerEnter.Invoke(OtherCollider_);
                else
                    return false;
            }

            if (fTriggerStay != null)
                return fTriggerStay.Invoke(OtherCollider_);
            else
                return false;
        }
        public void NotOverlapped(Int64 tick, CCollider2D Collider_, CCollider2D OtherCollider_)
        {
            var Key = new SContactPoint2D(Collider_, OtherCollider_);
            if (_ContactPoint2Ds.ContainsKey(Key))
            {
                var movingObject = _ContactPoint2Ds[Key];

                _ContactPoint2Ds.Remove(Key);

                if (Collider_.IsTrigger || OtherCollider_.IsTrigger)
                    fTriggerExit?.Invoke(OtherCollider_);
                else
                    fCollisionExit?.Invoke(tick, new SCollision2D(new SPoint(), new SPoint(), Collider_, OtherCollider_, movingObject));
            }
        }
        public void NotOverlapped(Int64 tick, CCollider2D OtherCollider_)
        {
            foreach (var k in _ContactPoint2Ds.Keys.ToArray())
            {
                if (k.OtherCollider != OtherCollider_)
                    continue;

                var movingObject = _ContactPoint2Ds[k];

                _ContactPoint2Ds.Remove(k);

                if (k.Collider.IsTrigger || OtherCollider_.IsTrigger)
                    fTriggerExit?.Invoke(OtherCollider_);
                else
                    fCollisionExit?.Invoke(tick, new SCollision2D(new SPoint(), new SPoint(), k.Collider, OtherCollider_, movingObject));
            }
        }
    }
}