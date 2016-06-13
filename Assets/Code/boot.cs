using UnityEngine;
using System.Collections;
using engine;
public class boot : MonoBehaviour {
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //GameObject.Find("logobg").GetComponent<UISprite>().SetRect(-Screen.width / 2, -Screen.height / 2, Screen.width, Screen.height);
    }
    void Start()
    {
        //App.init(gameObject);
        //加载登陆界面
        //UIManager.I().LoadView(UINameConst.ui_login);
        //SceneManager.I().onSceneLoaded(0);   
        StartCoroutine(StartCoutine());
       
    }

    void Update()
    {

    }

    IEnumerator StartCoutine()
    {
        yield return new WaitForSeconds(3f);        
        //SceneManager.I().onSceneLoaded(0);
    } 


    void LateUpdate()
    {;
    }
    void OnLevelWasLoaded(int level) 
    {
        if (SceneManager.I().currentScene != null)
            SceneManager.I().currentScene.Start();

    }


    void OnApplicationQuit()
    {
        NetManager.I().disconnect();
    }
}
