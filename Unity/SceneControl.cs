namespace rso.unity
{
    public class CSceneControl
    {
        public CScene CurScene { get; private set; } = null;
        CScene _NextScene;

        ~CSceneControl()
        {
            Clear();
        }
        public void SetNext(CScene Scene_)
        {
            _NextScene = Scene_; // don't care _NextScene is valid or not
        }
        public bool HaveNext()
        {
            return (_NextScene != null);
        }
        public void Update()
        {
            if (_NextScene != null)
            {
                if (CurScene != null)
                    CurScene._Exit();

                CurScene = _NextScene;
                _NextScene = null;
                CurScene._Enter();
            }

            if (CurScene != null)
                CurScene._Update();
        }
        public void Clear()
        {
            if (_NextScene != null)
                _NextScene = null;

            if (CurScene != null)
            {
                CurScene._Exit();
                CurScene = null;
            }
        }
    }
}