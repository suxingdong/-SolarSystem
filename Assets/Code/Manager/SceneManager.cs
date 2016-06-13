using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using engine;
public class CoroutineManager : MonoBehaviour
{

}
public class SceneManager : BaseMgr<SceneManager>
{
    public BaseScene currentScene { get{return _currentScene;} }
    BaseScene _currentScene;
    public bool isFighting = false;
    public bool isWin = false;
    public bool isCanMove = true;
    //public List LineRender
    public bool islasetline;
    public GameObject[] planetArry;
    public void onSceneLoaded(int SceneID)
    {
       //GameObject go = GameObject.Instantiate(new GameObject()) as GameObject;
       //go.name = "temp";
       isCanMove = true;
       //App.coroutine = go.AddComponent<CoroutineManager>();
       UIManager.I().isFingerEnable = true;
        switch (SceneID)
        {
            case 0:
                 Application.LoadLevel(0);
                break;
            case 1:
                Application.LoadLevel(1);
                break;
            case 2:
                Application.LoadLevel(2);
                break;
            case 3:
                Application.LoadLevel(3);
                break;
            case 4:
                Application.LoadLevel(4);
                break;
            default:
                break;
        }
    }


    


    public void FinshGame(bool iswin)
    {
        isCanMove = false;
        UIManager.I().CloseView(UINameConst.ui_battle);
        if (isWin)
        {
            UIManager.I().LoadView(UINameConst.ui_fightWin,false);
        }
        else 
        {
            GameObject partical = GameObject.Instantiate(Resources.Load(ParticalConst.FireRain)) as GameObject;
            partical.transform.parent = CameraManager.I().mainCamera.transform;
            partical.transform.localPosition = Vector3.zero;
            partical.transform.localEulerAngles = Vector3.zero;
            UIManager.I().LoadView(UINameConst.ui_fightWin,false);
        }
    }

    public void OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        RaycastHit hitInfo;
        Camera cam = CameraManager.I().mainCamera;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 300f))
        {
			int planet = LayerMask.NameToLayer("planet");
			int wall = LayerMask.NameToLayer("wall");
			if (hitInfo.collider.gameObject.layer == planet) {
				islasetline = true;
				TargetPos = hitInfo.collider.transform.position;
				//Debug.Log("planet");
				//LineRenderer
			}
			else if (hitInfo.collider.gameObject.layer == wall) 
			{
				Debug.Log("===========");
				TargetPos = hitInfo.point;
			}
        }
        
    }

    public void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
        Debug.Log("结束滑动");
        islasetline = false;
    }

	public Vector3 TargetPos {
		set;
		get;
	}
    public void OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
    {
        Debug.Log("开始滑动");
        RaycastHit hitInfo;
        Camera cam = CameraManager.I().mainCamera;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000f))
        {
            int planet = LayerMask.NameToLayer("planet");
			int wall = LayerMask.NameToLayer("wall");
			if (hitInfo.collider.gameObject.layer == planet) {
				islasetline = true;
				//Debug.Log("planet");
				//LineRenderer
			}
        }
    }
    public void ClickScene()
    {
        RaycastHit hitInfo;
        Camera cam = CameraManager.I().mainCamera;
        //_currentScene.ClickScene();
         if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 300f))
         {
             int planetLayer = LayerMask.NameToLayer("planet");
 
             if (hitInfo.collider.gameObject.layer == planetLayer)
             {
                 planet p = hitInfo.collider.gameObject.GetComponent<planet>();
                 SpriteManager.Instance.ChangeTarget(p);
             }
             else
             {
                 
             }
         }
    }
    public void updata()
    {
        if(_currentScene != null)
            _currentScene.Updata();
    }
}
