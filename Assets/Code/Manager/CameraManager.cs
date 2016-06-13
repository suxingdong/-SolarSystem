using UnityEngine;
using System.Collections;
using engine;
public class CameraManager :BaseMgr<CameraManager> {
    private Camera _uiCamera;
    private Camera _mainCamera;
    public Transform viewpoint = null;
    public AnimationCurve _distanceCurve;
    public bool isMoveing = false;
    Vector2 mDolly = new Vector2(0.5f, 0.5f);
    public Vector2 sensitivity = new Vector2(1.0f, 1.0f);
    float mLerpSpeed = 1f;
    Transform _Trans;
    Vector3 mPos;
    float mAngleY = 0.0f; // Target Y rotation
    public AnimationCurve _angleCurve;
    public float pinchSize;

    public Vector3 followPos = new Vector3(0.0f, 2.0f, 0.0f);


    public Transform mTrans
    {
        get 
        {
            _Trans = mainCamera.transform;
            return _Trans;
        }
    }
    public Camera mainCamera
    {
        get 
        {
            if (_mainCamera == null)
            {
                if (Camera.main != null)
                {
                    _mainCamera = Camera.main.GetComponent<Camera>();                    
                    //_mainCamera.gameObject.addOnce<FingerGestures>();
                    //_camFlowing = mainCamera.GetComponent<CameraFollowing>();
                }
            }
            return _mainCamera;
        }
        set
        {

        }
    }

    public AnimationCurve AngleCurveAnim
    {
        get
        {
            if (_angleCurve == null)
            {
                Keyframe[] ks = new Keyframe[3];
                _angleCurve = new AnimationCurve(ks);
                _angleCurve.AddKey(0, 30.0f);
                _angleCurve.AddKey(0.5f, 60.0f);
                _angleCurve.AddKey(1.0f, 80.0f);
            }
            return _angleCurve;
        }

    }

    public AnimationCurve DistanceCurveAnim
    {
        get 
        {
            if (_distanceCurve == null)
            {
                Keyframe[] ks = new Keyframe[3];
                _distanceCurve = new AnimationCurve(ks);
                _distanceCurve.AddKey(0, 8.0f);
                _distanceCurve.AddKey(1.0f, 120.0f);
            }
            return _distanceCurve;
        }
    }

    public Camera uiCamera 
    { 
        get
        {
            if(_uiCamera==null)
            {
                _uiCamera = GameObject.Find("uiCamera").GetComponent<Camera>();
            }
            return _uiCamera;
        }
        set
        {
            uiCamera = value;
        } 
    }

    public CameraFollowing _camFlowing;
	public void init() {
        if (Camera.main != null)
        {
            //mainCamera = Camera.main.camera;
            //_camFlowing = mainCamera.GetComponent<CameraFollowing>();
        }

        //uiCamera = GameObject.Find("UICamera").gameObject.camera;
	}

    public void setCamFlowTarget(Transform trans)
    {
        if(_camFlowing != null)
         _camFlowing.target = trans;
    }

    public void DragMoveByCamera(Vector3 targetPos)
    {
        float factor = Mathf.Min(1.0f, Time.deltaTime * 10.0f);
        
        mDolly.x = mDolly.x * (1.0f - factor) + mDolly.y * factor;
        Vector3 fw = mTrans.rotation * Vector3.forward;
		fw.y = 0.0f;
		fw.Normalize();

		Vector3 rt = mTrans.rotation * Vector3.right;
		rt.y = 0.0f;
		rt.Normalize();

		factor = Mathf.Lerp(0.5f, 2.0f, mDolly.x);
        Debug.Log("距离 = " + followPos);
        if (followPos.x >100)
            followPos.x = 100;
        else if (followPos.x < -100)
            followPos.x = -100;
        if (followPos.z >80)
            followPos.z = 80;
        else if (followPos.z < -80)
            followPos.z = -80;
		followPos -= fw * Input.GetAxis("Mouse Y") * factor;
		followPos -= rt * Input.GetAxis("Mouse X") * factor;
        updataCamera();
//         Vector3 v = Camera.main.transform.position;
//         v.x += targetPos.x * 0.1f;
//         v.y += targetPos.y * 0.1f;
//         v.z += targetPos.z * 0.1f;       
//         Camera.main.transform.position = v;
    }

    public void DragMoveToCamera(Vector3 targetPos)
    {

    }
    public void StretchCamera()
    {
//         if (mainCamera == null)
//             return;
//         if (isMoveing)
//             return;
//         mTrans = mainCamera.transform;
//         float factor = Mathf.Min(1.0f, Time.deltaTime * 1.0f);
//         Debug.Log("Mouse ScrollWheel ="+Input.GetAxis("Mouse ScrollWheel"));
//         mDolly.y = Mathf.Clamp01(mDolly.y - Input.GetAxis("Mouse ScrollWheel") * sensitivity.y * 0.25f*0.2f);
//         mDolly.x = mDolly.x * (1.0f - factor) + mDolly.y * factor;
//         updataCamera();
    }
	
    public void updataCamera()
    {
        //return;
        if (viewpoint != null)
        {
            float factor1 = Mathf.Min(1.0f, Time.deltaTime * 7.0f);
            mTrans.position = Vector3.Lerp(mTrans.position, viewpoint.position, factor1);
            mTrans.rotation = Quaternion.Slerp(mTrans.rotation, viewpoint.rotation, factor1);
            mLerpSpeed = 0f;
            return;
        }

        if (mainCamera == null)
            return;
        float factor0 = Mathf.Min(1.0f, Time.deltaTime * 1.0f);

        //pinchSize = Input.GetAxis("Mouse ScrollWheel");
        mDolly.y = Mathf.Clamp01(mDolly.y - pinchSize * sensitivity.y * 0.25f);
        mDolly.x = mDolly.x * (1.0f - factor0) + mDolly.y * factor0;

        // Lerp speed is interpolated so that the camera exits 'viewpoint focus' mode smoothly
        mLerpSpeed = Mathf.Lerp(mLerpSpeed, 1f, Mathf.Clamp01(Time.deltaTime * 5f));

        // Lerp the 'look at' position
        float factor = Mathf.Clamp01(Time.deltaTime * 20.0f * mLerpSpeed);
        mPos = Vector3.Lerp(mPos, followPos, factor);

        float angleX = AngleCurveAnim.Evaluate(mDolly.x);
        Quaternion targetRot = Quaternion.Euler(angleX, mAngleY, 0.0f);

        // Lerp the final position and rotation of the camera, offset by the dolly distance
        Vector3 offset = Vector3.forward * DistanceCurveAnim.Evaluate(mDolly.x);
        mTrans.position = Vector3.Lerp(mTrans.position, mPos - targetRot * offset, factor);
        mTrans.rotation = Quaternion.Slerp(mTrans.rotation, targetRot, factor);
    }


    public void OnPinchMove(float detlta)
    {
        pinchSize = detlta;
        //updataCamera();
        //target.transform.localScale += source.Delta * pinchScaleFactor * Vector3.one;
    }

    public void OnRotationMove(float source)
    {
        //UI.StatusText = "Rotation updated by " + source.RotationDelta + " degrees";
        //target.Rotate(0, 0, source.RotationDelta);
    }

    public void LookAroundCamera()
    {

    }



}
