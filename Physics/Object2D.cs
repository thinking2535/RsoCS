using System;

namespace rso.physics
{
    // Collider �� Transform ���迡���� Collider�� Offset, Size �� ���� ����ϰ� ���� Transform �� ����Ұ�.
    // Transform ������ Rotation, Scale�� ���� ����ϰ� ���� Position �� ����Ұ�.
    // Rotation ������ Z, X, Y �� ������ �ϵ� ���� World �� ���� �����̶�� �����ϰ� �𵨸� ȸ���ϴ� ������� ó���Ұ�.
    // ��ü�� ��Ӱ��迡���� �ڽĿ��� �θ������� ������� ó���Ұ�.
    public class CObject2D : STransform
    {
        CObject2D _Parent = null;
        bool _Enabled = true;
        public bool Enabled
        {
            get
            {
                return _Enabled && (_Parent == null || _Parent.Enabled);
            }
            set
            {
                _Enabled = value;
            }
        }
        public SPoint Position
        {
            get
            {
                if (_Parent != null)
                    return _Parent.Position.GetAdd(LocalPosition);
                else
                    return LocalPosition;
            }
        }
        public CObject2D(STransform Transform_) :
            base(Transform_)
        {
        }
        public CObject2D(STransform Transform_, CObject2D Parent_) :
            base(Transform_)
        {
            _Parent = Parent_;
        }
        public void SetParent(CObject2D Parent_)
        {
            _Parent = Parent_;
        }
    }
}
