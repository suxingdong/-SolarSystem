using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using engine;
public class UIManager: BaseMgr<UIManager> {

    GameObject _curOpenView;//当前打开的窗口
    GameObject _uititle;
    List<GameObject> uiList = new List<GameObject>();
    Dictionary<string, GameObject> _allOpenViews = new Dictionary<string, GameObject>();
    public bool isFingerEnable = true;
    public UIManager()
    {
       
    }
    public GameObject LoadView(string uiName, bool isTitle=true)
    {
        if(!IsOpen(uiName))
        {
            _curOpenView = GameObject.Instantiate(Resources.Load(uiName)) as GameObject;
            if (CameraManager.I().uiCamera != null)
            {
                _curOpenView.transform.parent = CameraManager.I().uiCamera.transform;
                _curOpenView.transform.localPosition = Vector3.zero;
                _curOpenView.transform.localScale = Vector3.one;
            }            
            _allOpenViews[uiName] = _curOpenView;
        }
        else
        {
            _curOpenView = _allOpenViews[uiName];
            _allOpenViews[uiName].SetActive(true);
        }

        if (isTitle)
        {
            _uititle = _uititle == null ? GameObject.Instantiate(Resources.Load(UINameConst.ui_uititle)) as GameObject : _uititle;
            if (CameraManager.I().uiCamera != null)
            {
                _uititle.transform.parent = CameraManager.I().uiCamera.transform;
                _uititle.transform.localPosition = Vector3.zero;
                _uititle.transform.localScale = Vector3.one;
            }
            isFingerEnable = false;
            ScreenBoxcollider(_curOpenView);
            Utility.AdjustmentPixelSize(_curOpenView);
            //ScreenBoxcollider(_uititle);
            _uititle.SetActive(true);
            if (!uiList.Contains(_curOpenView))
                uiList.Add(_curOpenView);
        }
        else
        {
              if (_uititle)
                  _uititle.SetActive(false);
            
        }

        
        return _curOpenView;
    }

    public void ScreenBoxcollider(GameObject go)
    {
        BoxCollider box = go.addOnce<BoxCollider>();
        Vector3 v = new Vector3(Screen.width,Screen.height,1);
        box.size = v;
    }
    public void CloseView(string uiName, bool isDestroy=false)
    {
        if (isDestroy)
        {
            if (_allOpenViews[uiName] != null)
            {
                GameObject.Destroy(_allOpenViews[uiName]);
                _allOpenViews.Remove(uiName);
            }  
        }
        else
        {
            _allOpenViews[uiName].SetActive(false);
        }
        uiList.Remove(_allOpenViews[uiName]);
        isFingerEnable = true;
    }

    public void ShowLoading()
    {
        UIManager.I().LoadView(UINameConst.ui_Loading,false);
    }

    public void CloseLoading()
    {
        UIManager.I().CloseView(UINameConst.ui_Loading);
    }

    public void ShowMessage()
    {

    }

    public void CloseMessage()
    {
       
    }

    /*
     * true 表示打开过的，false 表示没有打开的 
     */
    public bool IsOpen(string uiName)
    {
        if (_allOpenViews.ContainsKey(uiName))
        {
            if (_allOpenViews[uiName] != null)
                return true;
        }
        return false;
    }

    public void onCloseCurView()
    {
        _curOpenView.SetActive(false);
    }
    public void onBack()
    {
        onCloseCurView();
        uiList.Remove(_curOpenView);
        if (uiList.Count == 0)
        {
            isFingerEnable = true;
            _uititle.SetActive(false);
            _curOpenView = null;
        }
        else
        {
            _curOpenView = uiList[uiList.Count-1];
        }
        
    }


}
