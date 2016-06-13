using UnityEngine;
using System.Collections;

public class GameManager : UnityAllSceneSingleton<GameManager>,IMessageObject
{
	    public enum Status
		{
			NONE = 1,
			LOAD_RESOUCE,
			LOAD_SCENE,
			PREPARE_SCAN,
			START_GAME,
			END_GAME,
		}
		//init manager
		public Status CurStatus = Status.NONE;	
		public Vector3 startRealPos = Vector3.zero;
		public Vector3 startPos = Vector3.zero;
		Vector3 startDir  = Vector3.zero;

		public Transform target;
		bool startScan = false;
		public override void  Awake()
		{
			base.Awake ();
			
		}
		// Use this for initialization
		void Start ()
		{

		}
		void OnDestroy()
		{
			
		}
		public void ReloadScene(int scene)
		{
		
		this.START_METHOD ("ReloadScene");

		StartCoroutine (DoDelay ());
		this.END_METHOD("ReloadScene");
		}
		IEnumerator DoDelay()
		{
		yield return  new WaitForSeconds(1.0f);
		startScan = true;
			//BowMan bowMan = (BowMan)cManager.SpawnChar (CharaData.CharClassType.CHARACTER, (int)CharaData.charModel.BOWMAN, 1, startPos, startDir, CharaStatus.Pose.Run);
			//bowMan.DoAI ();
			//bowMan.SetGimoz (gizmos);
		}


		// Update is called once per frame
		void Update ()
		{
			switch (CurStatus) 
			{
		case Status.LOAD_RESOUCE:
			break;
		case Status.LOAD_SCENE:
				ReloadScene(1);
				CurStatus = Status.PREPARE_SCAN;
			break;
		case Status.PREPARE_SCAN:
			break;
		case Status.START_GAME:
			break;
			}
		}
}

