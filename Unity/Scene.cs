using UnityEngine;

namespace rso.unity
{
    public abstract class CScene
    {
        string _PrefabName;
        Vector3 _Pos;
        protected GameObject _Object = null;
        public CScene(string PrefabName_, Vector3 Pos_)
        {
            _PrefabName = PrefabName_;
            _Pos = Pos_;
        }
        internal void _Enter()
        {
            if (_Object != null)
                return;

            _Object = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(_PrefabName), _Pos, Quaternion.identity);
            _Object.name = _PrefabName;
            Enter();
        }
        internal void _Update()
        {
            Update();
        }
        internal void _Exit()
        {
            if (_Object == null)
                return;

            Exit();

            GameObject.Destroy(_Object);
            _Object = null;
        }
        protected virtual void Enter()
        {
        }
        protected virtual void Update()
        {
        }
        protected virtual void Exit()
        {
        }
    }
}
