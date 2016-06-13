using UnityEngine;
using System.Collections;

public class GlobalManager : UnityAllSceneSingleton<GlobalManager>
{
			public float f_UpdateInterval = 0.5F;
			
			private float f_LastInterval;
			
			private int i_Frames = 0;
			
			private float f_Fps;
			public static int verts;
			public static int tris;
    // Use this for initialization
        public override void Awake()
		{
            gameObject.AddComponent<SpriteManager>();
            gameObject.AddComponent<DataManager>();
			Application.targetFrameRate = 300;
		}

		void Start ()
		{
			//loadlevel
			GameManager.Instance.CurStatus = GameManager.Status.LOAD_RESOUCE;
			DataManager dataIns = DataManager.Instance;		
			f_LastInterval = Time.realtimeSinceStartup;
			
			i_Frames = 0;
				//water.SetActive(true);
		}
	void GetObjectStats() {
		verts = 0;
		tris = 0;
		GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach (GameObject obj in ob) {
			GetObjectStats(obj);
		}
	}
	
	void GetObjectStats(GameObject obj) {
		Component[] filters;
		filters = obj.GetComponentsInChildren<MeshFilter>();
		foreach( MeshFilter f  in filters )
		{
			tris += f.sharedMesh.triangles.Length/3;
			verts += f.sharedMesh.vertexCount;
		}
	}
	void OnGUI() 
	{
		GUI.skin.label.normal.textColor =new  Color( 255,255, 255, 1.0f );
		GUI.Label(new Rect(0, 10, 200, 200), "FPS:" + (f_Fps).ToString("f2"));
		string vertsdisplay = verts.ToString ("#,##0 verts");
		GUILayout.Label(vertsdisplay);
		string trisdisplay = tris.ToString ("#,##0 tris");
		GUILayout.Label(trisdisplay);
	}
		// Update is called once per frame
		void Update ()
		{
		
			++i_Frames;
			
			if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval) 
			{
				f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);
				
				i_Frames = 0;
				
				f_LastInterval = Time.realtimeSinceStartup;
				GetObjectStats();
			}
		}
}

