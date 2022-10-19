using System;

namespace rso.physics
{
    public class SCollision2D
    {
        public SPoint RelativeVelocity { get; }
        public SPoint Normal { get; }
        public CCollider2D Collider { get; }
        public CCollider2D OtherCollider { get; }
        public CMovingObject2D OtherMovingObject { get; }
        public SCollision2D(SPoint RelativeVelocity_, SPoint Normal_, CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
            RelativeVelocity = RelativeVelocity_;
            Normal = Normal_;
            Collider = Collider_;
            OtherCollider = OtherCollider_;
            OtherMovingObject = OtherMovingObject_;
        }
    }

    public abstract class CCollider2D : CObject2D
    {
        public Int32 Number { get; protected set; }
        public bool IsTrigger = false;
        public void SetParentCollider(CCollider2D ParentCollider_)
        {
            SetParent(ParentCollider_);
        }
        public CCollider2D(STransform Transform_, Int32 Number_) :
            base(Transform_)
        {
            Number = Number_;
        }
        public CCollider2D(STransform Transform_, CObject2D Parent_, Int32 Number_) :
            base(Transform_, Parent_)
        {
            Number = Number_;
        }

        // �ᱹ CEngineObjectRect �� CEngineObjectRect �� ���� ��길 �����ϰ�, �Ķ���Ͱ� CEngineObject�� ���� ��ü�� �Ǿ�� CEngineObjectRect���� CEngineObjectContainer���� �ľ��� �����ϹǷ�
        // ���� ��ü�� CEngineObjectRect�϶����� �� �� ���� �Ķ���͸� �ٽ� ��ü�� �ٲپ� CEngineObjectRect �϶� ���� CollisionCheck �� ȣ���Ͽ�
        // ��ü�� �Ķ���͸� ��� CEngineObjectRect �� ���� �� ó��
        public abstract bool CheckOverlapped(Int64 tick, CMovingObject2D MovingObject_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_);
        public abstract bool CheckOverlapped(Int64 tick, CMovingObject2D MovingObject_, CRectCollider2D OtherRectCollider_, CMovingObject2D OtherMovingObject_);
    }
}
