using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour 
{
    class global
    {
        public enum EPhase
        {
            Down,
            Stationary,
            Move,
            Up,
            Canceled,
            Max,
        }

        static float _Mouse_x = 0.0f;
        static float _Mouse_y = 0.0f;

        public class CTouch
        {
            public EPhase Phase;
            public int TouchID;
            public float x;
            public float y;

            public CTouch(EPhase Phase_, int TouchID_, float x_, float y_)
            {
                Phase = Phase_;
                TouchID = TouchID_;
                x = x_;
                y = y_;
            }
        }

        public static int TouchCount()
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (Input.GetMouseButton(0))
                return 1;
            else
                return 0;
#else
        return Input.touchCount;
#endif
        }

        public static CTouch GetTouch(int Index_)
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (Index_ > 0)
                return null;

            if (!Input.GetMouseButton(0))
                return null;

            if (Input.GetMouseButtonDown(0))
            {
                _Mouse_x = Input.mousePosition.x;
                _Mouse_y = Input.mousePosition.y;
                return new CTouch(EPhase.Down, 0, Input.mousePosition.x, Input.mousePosition.y);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                return new CTouch(EPhase.Up, 0, Input.mousePosition.x, Input.mousePosition.y);
            }
            else if (Input.mousePosition.x != _Mouse_x ||
                Input.mousePosition.y != _Mouse_y)
            {
                _Mouse_x = Input.mousePosition.x;
                _Mouse_y = Input.mousePosition.y;
                return new CTouch(EPhase.Move, 0, Input.mousePosition.x, Input.mousePosition.y);
            }
            else
            {
                return new CTouch(EPhase.Stationary, 0, Input.mousePosition.x, Input.mousePosition.y);
            }
#else
        if (Index_ >= Input.touchCount)
            return null;

        var Touch = Input.GetTouch(Index_);
        if (Touch.phase == TouchPhase.Began)
            return new CTouch(EPhase.Down, Touch.fingerId, Touch.position.x, Touch.position.y);
        else if (Touch.phase == TouchPhase.Ended)
            return new CTouch(EPhase.Up, Touch.fingerId, Touch.position.x, Touch.position.y);
        else if (Touch.phase == TouchPhase.Moved)
            return new CTouch(EPhase.Move, Touch.fingerId, Touch.position.x, Touch.position.y);
        else if (Touch.phase == TouchPhase.Stationary)
            return new CTouch(EPhase.Stationary, Touch.fingerId, Touch.position.x, Touch.position.y);
        else
            return new CTouch(EPhase.Canceled, Touch.fingerId, Touch.position.x, Touch.position.y);
#endif
        }
    }
    
    static float g_Far = 1000.0f;
    static float g_FOV = 15.0f;
    static float g_CamHeight = Mathf.Tan(g_FOV * Mathf.PI / 360.0f) * g_Far * 2.0f;
    static float g_CamWidth = g_CamHeight * Screen.width / Screen.height;

    static Vector3 g_MonitorStartPos;
    static Vector3 g_CamStartPos;
    float g_StartTwoTouchLength = 0.0f;

    float ScreenMidX()
    {
        return (Screen.width / 2.0f);
    }
    float ScreenMidY()
    {
        return (Screen.height / 2.0f);
    }

    public GUIText _Text = null;
    int m_LineCnt = 0;
    string m_strText = "";

    public void Log(object obj)
    {
        Debug.Log(obj);

        if (m_LineCnt + 1 > 30)
            m_strText = m_strText.Substring(m_strText.IndexOf("\n") + 1);
        else
            ++m_LineCnt;

        m_strText += obj.ToString() + "\n";
    }

    float _GetDist(global.CTouch Touch0_, global.CTouch Touch1_)
    {
        return Mathf.Sqrt((Touch0_.x - Touch1_.x) * (Touch0_.x - Touch1_.x) + (Touch0_.y - Touch1_.y) * (Touch0_.y - Touch1_.y));
    }
    void _MoveZoom(float X_, float Y_, float Scale_)
    {
        Vector3 Pos = transform.position;

        Pos.z = g_CamStartPos.z / Scale_;

        // Move Offset Calculate /////////////////////////
        float Screen_dx = X_ - g_MonitorStartPos.x;
        float Screen_dy = Y_ - g_MonitorStartPos.y;

        float Cam_dx = Screen_dx * g_CamWidth / Screen.width;
        float Cam_dy = Screen_dy * g_CamHeight / Screen.height;


        // Sccaling
        Cam_dx = (Cam_dx * (-Pos.z) / g_Far);
        Cam_dy = (Cam_dy * (-Pos.z) / g_Far);

        Pos.x = g_CamStartPos.x - Cam_dx;
        Pos.y = g_CamStartPos.y - Cam_dy;

        transform.position = Pos;
        //        transform.position = CalcLimitPos(Pos);

        Log(string.Format("{0,15}{1,15}{2,15}", X_, Y_, Scale_));
    }

    public void LimitedMoveEx()
    {
        if (global.TouchCount() > 0 &&
            global.TouchCount() < 3)
        {
            if (global.TouchCount() == 1)
            {
                var Touch = global.GetTouch(0);
                if (Touch == null)
                    return;

                if (Touch.Phase == global.EPhase.Down)
                {
                    g_MonitorStartPos.x = Touch.x;
                    g_MonitorStartPos.y = Touch.y;
                    g_CamStartPos = transform.position;
                }
                else
                {
                    _MoveZoom(Touch.x, Touch.y, 1.0f);
                }
            }
            else if (global.TouchCount() == 2)
            {
                var Touch0 = global.GetTouch(0);
                var Touch1 = global.GetTouch(1);
                if (Touch0 == null ||
                    Touch1 == null)
                    return;

                if (Touch0.Phase == global.EPhase.Down)
                {
                    if (Touch1.Phase == global.EPhase.Down)
                    {
                        g_MonitorStartPos.x = (Touch0.x + Touch1.x) / 2.0f;
                        g_MonitorStartPos.y = (Touch0.y + Touch1.y) / 2.0f;
                        g_CamStartPos = transform.position;
                        g_StartTwoTouchLength = _GetDist(Touch0, Touch1);
                    }
                    else
                    {
                        _MoveZoom(Touch1.x, Touch1.y, 1.0f);

                        if (Touch1.Phase == global.EPhase.Up ||
                            Touch1.Phase == global.EPhase.Canceled)
                        {
                            g_MonitorStartPos.x = Touch0.x;
                            g_MonitorStartPos.y = Touch0.y;
                        }
                        else
                        {
                            g_MonitorStartPos.x = (Touch0.x + Touch1.x) / 2.0f;
                            g_MonitorStartPos.y = (Touch0.y + Touch1.y) / 2.0f;
                            g_StartTwoTouchLength = _GetDist(Touch0, Touch1);
                        }

                        g_CamStartPos = transform.position;
                    }
                }
                else if (Touch0.Phase == global.EPhase.Up ||
                    Touch0.Phase == global.EPhase.Canceled)
                {
                    if (Touch1.Phase == global.EPhase.Down)
                    {
                        _MoveZoom(Touch0.x, Touch0.y, 1.0f);

                        g_MonitorStartPos.x = Touch1.x;
                        g_MonitorStartPos.y = Touch1.y;

                        g_CamStartPos = transform.position;
                    }
                    else
                    {
                        _MoveZoom((Touch0.x + Touch1.x) / 2.0f, (Touch0.y + Touch1.y) / 2.0f, _GetDist(Touch0, Touch1) / g_StartTwoTouchLength);

                        if (Touch1.Phase == global.EPhase.Up ||
                            Touch1.Phase == global.EPhase.Canceled)
                        {
                        }
                        else
                        {
                            g_MonitorStartPos.x = Touch1.x;
                            g_MonitorStartPos.y = Touch1.y;

                            g_CamStartPos = transform.position;
                        }
                    }
                }
                else
                {
                    if (Touch1.Phase == global.EPhase.Down)
                    {
                        _MoveZoom(Touch0.x, Touch0.y, _GetDist(Touch0, Touch1) / g_StartTwoTouchLength);

                        g_MonitorStartPos.x = (Touch0.x + Touch1.x) / 2.0f;
                        g_MonitorStartPos.y = (Touch0.y + Touch1.y) / 2.0f;

                        g_CamStartPos = transform.position;
                        g_StartTwoTouchLength = _GetDist(Touch0, Touch1);
                    }
                    else
                    {
                        _MoveZoom((Touch0.x + Touch1.x) / 2.0f, (Touch0.y + Touch1.y) / 2.0f, _GetDist(Touch0, Touch1) / g_StartTwoTouchLength);

                        if (Touch1.Phase == global.EPhase.Up ||
                            Touch1.Phase == global.EPhase.Canceled)
                        {
                            g_MonitorStartPos.x = Touch0.x;
                            g_MonitorStartPos.y = Touch0.y;

                            g_CamStartPos = transform.position;
                        }
                    }
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _Text.text = m_strText;

        LimitedMoveEx();
	}
}
