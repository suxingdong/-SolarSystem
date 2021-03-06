using UnityEngine;
using System.Collections;
using engine;
public class FingerEvent :  MonoBehaviour {

    public bool isMoved = false;

    //public PinchGestureRecognizer pinchGesture;
    //public RotationGestureRecognizer rotationGesture;

    #region Input Mode

    public enum InputMode
    {
        PinchOnly,
        RotationOnly,
        PinchAndRotation
    }

    static InputMode inputMode = InputMode.PinchAndRotation;

    #endregion

    void OnEnable()
    {
    	//启动时调用，这里开始注册手势操作的事件。
    	
    	//按下事件： OnFingerDown就是按下事件监听的方法，这个名子可以由你来自定义。方法只能在本类中监听。下面所有的事件都一样！！！
        FingerGestures.OnFingerDown += OnFingerDown;
        //抬起事件
		FingerGestures.OnFingerUp += OnFingerUp;
	    //开始拖动事件
	    FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
        //拖动中事件...
        FingerGestures.OnFingerDragMove += OnFingerDragMove;
        //拖动结束事件
        FingerGestures.OnFingerDragEnd += OnFingerDragEnd; 
		//上、下、左、右、四个方向的手势滑动
		FingerGestures.OnFingerSwipe += OnFingerSwipe;
		//连击事件 连续点击事件
		FingerGestures.OnFingerTap += OnFingerTap;
		//按下事件后调用一下三个方法
		FingerGestures.OnFingerStationaryBegin += OnFingerStationaryBegin;
		FingerGestures.OnFingerStationary += OnFingerStationary;
		FingerGestures.OnFingerStationaryEnd += OnFingerStationaryEnd;
		//长按事件
		FingerGestures.OnFingerLongPress += OnFingerLongPress;


        FingerGestures.OnRotationBegin += FingerGestures_OnRotationBegin;
        FingerGestures.OnRotationMove += FingerGestures_OnRotationMove;
        FingerGestures.OnRotationEnd += FingerGestures_OnRotationEnd;

        FingerGestures.OnPinchBegin += FingerGestures_OnPinchBegin;
        FingerGestures.OnPinchMove += FingerGestures_OnPinchMove;
        FingerGestures.OnPinchEnd += FingerGestures_OnPinchEnd;
//         pinchGesture.OnStateChanged += Gesture_OnStateChanged;
//         pinchGesture.OnPinchMove += OnPinchMove;
//         pinchGesture.SetCanBeginDelegate(CanBeginPinch);
// 
//         rotationGesture.OnStateChanged += Gesture_OnStateChanged;
//         rotationGesture.OnRotationMove += OnRotationMove;
//         rotationGesture.SetCanBeginDelegate(CanBeginRotation		
    }

    void OnDisable()
    {
    	//关闭时调用，这里销毁手势操作的事件
    	//和上面一样
        FingerGestures.OnFingerDown -= OnFingerDown;
		FingerGestures.OnFingerUp -= OnFingerUp;
		FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd; 
		FingerGestures.OnFingerSwipe -= OnFingerSwipe;
		FingerGestures.OnFingerTap -= OnFingerTap;
		FingerGestures.OnFingerStationaryBegin -= OnFingerStationaryBegin;
		FingerGestures.OnFingerStationary -= OnFingerStationary;
		FingerGestures.OnFingerStationaryEnd -= OnFingerStationaryEnd;
		FingerGestures.OnFingerLongPress -= OnFingerLongPress;

        FingerGestures.OnRotationBegin -= FingerGestures_OnRotationBegin;
        FingerGestures.OnRotationMove -= FingerGestures_OnRotationMove;
        FingerGestures.OnRotationEnd -= FingerGestures_OnRotationEnd;

        FingerGestures.OnPinchBegin -= FingerGestures_OnPinchBegin;
        FingerGestures.OnPinchMove -= FingerGestures_OnPinchMove;
        FingerGestures.OnPinchEnd -= FingerGestures_OnPinchEnd;
    }


    #region  按下时调用
    void OnFingerDown( int fingerIndex, Vector2 fingerPos )
    {
		//int fingerIndex 是手指的ID 第一按下的手指就是 0 第二个按下的手指就是1。。。一次类推。
		//Vector2 fingerPos 手指按下屏幕中的2D坐标
		
		//将2D坐标转换成3D坐标
        transform.position = GetWorldPos( fingerPos );
		Debug.Log(" OnFingerDown ="  +fingerPos);
        //SceneManager.I().OnFingerDown();
    }

    #endregion

    #region 抬起时调用
	void OnFingerUp( int fingerIndex, Vector2 fingerPos, float timeHeldDown )
	{
        if (!UIManager.I().isFingerEnable)
            return;
        if (!isMoved)//
        {
            //没有移动
            Vector3 pos = GetWorldPos(fingerPos);
            SceneManager.I().ClickScene();
        }
        
	}
    #endregion

    #region 开始滑动
    void OnFingerDragBegin( int fingerIndex, Vector2 fingerPos, Vector2 startPos )
    {
        SceneManager.I().OnFingerDragBegin( fingerIndex,  fingerPos,  startPos);
        // Debug.Log("OnFingerDragBegin fingerIndex =" + fingerIndex  + " fingerPos ="+fingerPos +"startPos =" +startPos); 
    }
    #endregion
    
    #region 滑动结束
	void OnFingerDragEnd( int fingerIndex, Vector2 fingerPos )
	{
        //isMoved = false;
        //CameraManager.I().isMoveing = false;
        //Debug.Log("OnFingerDragEnd fingerIndex =" + fingerIndex  + " fingerPos ="+fingerPos); 
        SceneManager.I().OnFingerDragEnd(fingerIndex, fingerPos);
	}
    #endregion


    #region 滑动中
    void OnFingerDragMove( int fingerIndex, Vector2 fingerPos, Vector2 delta )
    {
        //if (!UIManager.I().isFingerEnable)
        //    return;
        ////Debug.Log("滑动中");
        //if (!SceneManager.I().isCanMove)
        //{
        //    return;
        //}
        ////判断移动距离
        //float distance = Vector2.Distance(delta, Vector2.zero);
        //if (distance > 5)
        //{
        //    CameraManager.I().isMoveing = true;
        //    isMoved = true;
        //    Vector3 vPos = new Vector3();
        //    vPos.y = 0;
        //    vPos.x = delta.x*0.5f;
        //    vPos.z = delta.y * 0.5f;
        //    CameraManager.I().DragMoveByCamera(vPos);
        //}

        SceneManager.I().OnFingerDragMove(fingerIndex, fingerPos, delta);
    }
    #endregion

    #region 上下左右四方方向滑动手势操作
	void OnFingerSwipe( int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity )
    {
		//结果是 Up Down Left Right 四个方向
		//Debug.Log("OnFingerSwipe " + direction + " with finger " + fingerIndex);

    }
    #endregion
    
    #region 其它事件
    //连续按下事件， tapCount就是当前连续按下几次
	void OnFingerTap( int fingerIndex, Vector2 fingerPos, int tapCount )
    {
			
        //Debug.Log("OnFingerTap " + tapCount + " times with finger " + fingerIndex);

    }
	
	//按下事件开始后调用，包括 开始 结束 持续中状态只到下次事件开始！
	void OnFingerStationaryBegin( int fingerIndex, Vector2 fingerPos )
	{
		
		 //Debug.Log("OnFingerStationaryBegin " + fingerPos + " times with finger " + fingerIndex);
	}
	
	
	void OnFingerStationary( int fingerIndex, Vector2 fingerPos, float elapsedTime )
	{
		
		 //Debug.Log("OnFingerStationary " + fingerPos + " times with finger " + fingerIndex);
		
	}
	
	void OnFingerStationaryEnd( int fingerIndex, Vector2 fingerPos, float elapsedTime )
	{
		
		 //Debug.Log("OnFingerStationaryEnd " + fingerPos + " times with finger " + fingerIndex);
	}
	
	
	//长按事件
	void OnFingerLongPress( int fingerIndex, Vector2 fingerPos )
	{
		
		//Debug.Log("OnFingerLongPress " + fingerPos );
	}
    #endregion

    public void PinchEventHandler(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        //Debug.Log("PinchEventHandler");
    }



    #region Rotation gesture

    bool rotating = false;
    bool Rotating
    {
        get { return rotating; }
        set
        {
            if (rotating != value)
            {
                rotating = value;
            }
        }
    }

    public bool RotationAllowed
    {
       // Debug.Log("RotationAllowed");
        get { return inputMode == InputMode.RotationOnly || inputMode == InputMode.PinchAndRotation; }
    }

    void FingerGestures_OnRotationBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        if (!UIManager.I().isFingerEnable)
            return;

        if (RotationAllowed)
        {
            Rotating = true;
        }
    }

    void FingerGestures_OnRotationMove(Vector2 fingerPos1, Vector2 fingerPos2, float rotationAngleDelta)
    {
        if (!UIManager.I().isFingerEnable)
            return;
        if (Rotating)
        {
            CameraManager.I().OnRotationMove(rotationAngleDelta);
            //target.Rotate(0, 0, rotationAngleDelta);
        }
    }

    void FingerGestures_OnRotationEnd(Vector2 fingerPos1, Vector2 fingerPos2, float totalRotationAngle)
    {
        if (!UIManager.I().isFingerEnable)
            return;
        if (Rotating)
        {
            //UI.StatusText = "Rotation gesture ended. Total rotation: " + totalRotationAngle;
            Rotating = false;
        }
    }

    #endregion


    #region Pinch Gesture

    bool pinching = false;
    bool Pinching
    {
        get { return pinching; }
        set
        {
            if (pinching != value)
            {
                pinching = value;
            }
        }
    }

    public bool PinchAllowed
    {
        get { return inputMode == InputMode.PinchOnly || inputMode == InputMode.PinchAndRotation; }
    }

    void FingerGestures_OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        if (!UIManager.I().isFingerEnable)
            return;
        if (!PinchAllowed)
            return;

        Pinching = true;
    }

    void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        if (!UIManager.I().isFingerEnable)
            return;
        if (Pinching)
        {
            Debug.Log("pinch move1 = "+ delta);
            CameraManager.I().pinchSize = delta * 0.002f;
            //CameraManager.I().OnPinchMove(delta*0.2f);
            // change the scale of the target based on the pinch delta value
            //target.transform.localScale += delta * pinchScaleFactor * Vector3.one;
        }
    }

    void FingerGestures_OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        if (!UIManager.I().isFingerEnable)
            return;
        if (Pinching)
        {
            Pinching = false;
            CameraManager.I().pinchSize = 0;
        }
    }

    #endregion

	//把Unity屏幕坐标换算成3D坐标
    Vector3 GetWorldPos( Vector2 screenPos )
    {
        Camera mainCamera = Camera.main;
        return mainCamera.ScreenToWorldPoint( new Vector3( screenPos.x, screenPos.y, Mathf.Abs( transform.position.z - mainCamera.transform.position.z ) ) ); 
    }

}
