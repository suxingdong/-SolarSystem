using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 
using System.IO;  
using System.Text;
using LumenWorks.Framework.IO.Csv;  


public class DataManager : UnityAllSceneSingleton<DataManager>,IMessageObject
{
	public static readonly string PathURL = 
#if UNITY_ANDROID && !UNITY_EDITOR
		"jar:file://" + Application.dataPath + "!/assets/";
//#elif UNITY_IPHONE
//		Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
	"file://" + Application.dataPath+"/StreamingAssets"+"/"; 
//#else
//	string.Empty;
#endif
	public bool HasDoneResource = false;
	void Start()
	{
		//LoadMainGameObject (PathURL + "csv.assetbundle");
		StartCoroutine(LoadMainGameObject(PathURL ));
	}
	private IEnumerator LoadMainGameObject(string path)
	{

		this.PRINT("path" + path);
		string temppath = path + "csv.assetbundle";
		WWW bundle = new WWW(temppath);
		
		yield return bundle;
		Object[] objs = bundle.assetBundle.LoadAllAssets (typeof(soCsv));
		//Object[] objs = bundle.assetBundle.LoadAll(typeof(soCsv));
		this.PRINT ("count : " + objs.Length);
		for (int  i = 0 , max = objs.Length; i < max; i ++) 
		{
			this.PRINT ("name : " + objs[i].name);
			Object obj = objs[i];  
			soCsv csv = obj as soCsv;
			if(csv.name != "Planet" && csv.name != "Role" && csv.name != "SpaceStation")
			{
				continue;
			}
			MemoryStream ms = new MemoryStream(csv.content);
			if(ms == null)
			{
				Debug.LogWarning("covert csv failed");
				continue;
			}
			StreamReader sr = new StreamReader(ms, Encoding.Default, true);
			TextReader tr = sr as TextReader;
			if(tr == null)
			{
				Debug.LogWarning("text reader is null");
				continue;
			}

			CsvReader cr = new CsvReader(tr,true);
			if(cr  == null)
			{
				Debug.LogWarning("CsvReader is null");
				continue;
			}
			
			if(csv.name == "Planet")
			{
				ReadPlanet(cr);
				//CSVFileHelper.ReadCsv(cr, ReadBuilding);
			}
			else if(csv.name == "Role")
			{
				ReadRole(cr);
			}
			else if(csv.name == "SpaceStation")
			{
				ReadSpaceStation(cr);
			}

			//string [] headers = cr.GetFieldHeaders();
//			foreach(string szHeader in headers)
//				Debug.Log(szHeader);
		}
		//yield return Instantiate(bundle.assetBundle.mainAsset);
		bundle.assetBundle.Unload(false);

		temppath = path + "txt.assetbundle";
		bundle = new WWW(temppath);
		yield return bundle;
#if UNITY_4_6_8
		TextAsset asset = bundle.assetBundle.Load("string",  typeof(TextAsset)) as TextAsset;
#else
        TextAsset asset = bundle.assetBundle.LoadAsset("string") as TextAsset;
#endif
        StringReader reader = new StringReader (asset.text);
		string line = reader.ReadLine();
		while (line != null) 
		{
			string[] str = line.Split(new char[]{'='},  System.StringSplitOptions.RemoveEmptyEntries);
			StringConfManager.Instance.AddStringConf(str[0], str[1]);
			line = reader.ReadLine();
		}
		bundle.assetBundle.Unload(false);
		//
		GameManager.Instance.CurStatus = GameManager.Status.LOAD_RESOUCE;
		Debug.Log("=======================> data loaded <=====================" );
        SceneManager.I().onSceneLoaded(1);
    }

	protected void ReadPlanet(CsvReader cr)
	{
		int fieldCount = cr.FieldCount;
		string [] headers = cr.GetFieldHeaders();
		//cr.ReadNextRecord ();// the real head
		while (cr.ReadNextRecord())
		{
			int  i = 0;
			PlanetConfig conf = new PlanetConfig();
			conf.Id = int.Parse(cr[i++]);
			conf.Name = cr[i++];
			conf.Totalpeople = int.Parse(cr[i++]);
			conf.Water = int.Parse(cr[i++]);
			conf.Fire = int.Parse(cr[i++]);
			conf.Tree = int.Parse(cr[i++]);
			conf.Iron = int.Parse(cr[i++]);
            conf.BuildTime = int.Parse(cr[i++]);
            conf.WaterTime = float.Parse(cr[i++]);
            conf.FireTime = float.Parse(cr[i++]);
            conf.TreeTime = float.Parse(cr[i++]);
            conf.IronTime = float.Parse(cr[i++]);
            PlanetConfManager.Instance.AddConf(conf);

        }
	}

	protected void ReadRole(CsvReader cr)
	{
		int fieldCount = cr.FieldCount;
		string [] headers = cr.GetFieldHeaders();
		//cr.ReadNextRecord ();// the real head
		while (cr.ReadNextRecord())
		{
			int  i = 0;
			RoleConfig conf = new RoleConfig();
			conf.Id = int.Parse(cr[i++]);
			conf.Name = cr[i++];
			conf.HP = int.Parse(cr[i++]);
			conf.Attack = int.Parse(cr[i++]);
			conf.Speed = int.Parse(cr[i++]);
			conf.Unit = int.Parse(cr[i++]);
			conf.AttackRange = int.Parse(cr[i++]);
		}
	}

	private void ReadSpaceStation(CsvReader cr)
	{
		int fieldCount = cr.FieldCount;
		string [] headers = cr.GetFieldHeaders();
		//cr.ReadNextRecord ();// the real head
		while (cr.ReadNextRecord())
		{
			int  i = 0;
			SpaceStationConfig conf = new SpaceStationConfig();
			conf.Id = int.Parse(cr[i++]);
			conf.Name = cr[i++];
			conf.Attack = int.Parse(cr[i++]);
			conf.BuildTime = int.Parse(cr[i++]);
			conf.AttackRange = int.Parse(cr[i++]);
		}
	}



	private string empty2number(string val)
	{
		return val == "" ? "0" : val;
	}

}

public class StringConfManager
{
	private static StringConfManager _Instance;
	public Dictionary<string, string> stringConfs = new Dictionary<string, string> ();
	public static StringConfManager  Instance
	{
		get{
			if (_Instance == null)
				_Instance = new StringConfManager ();
			return _Instance;
		}
	}
	public void AddStringConf(string name, string txt )
	{
		stringConfs[name]= txt;
	}
	public string GetStringbyName(string name)
	{
		if (stringConfs.ContainsKey (name)) {
			return stringConfs[name];
		}
		return null;
	}
}




public class PlanetConfig
{
	public int Id;
	public string Name;
	public int Totalpeople;
	public int Water;
	public int Fire;
	public int Tree;
	public int Iron;
    public float WaterTime;
    public float FireTime;
    public float IronTime;
    public float TreeTime;
    public int BuildTime;
}

public class RoleConfig
{
	public int Id;
	public string Name;
	public int HP;
	public int Attack;
	public int Speed;
    public float RotationSpeed;
	public int Unit;
	public int AttackRange;
}

public class SpaceStationConfig
{
	public int Id;
	public string Name;
	public int Attack;
	public int BuildTime;
	public int AttackRange;
}


public class PlanetConfManager
{
	private static  PlanetConfManager _Instance;
	public Dictionary<int, PlanetConfig> Confs = new Dictionary<int, PlanetConfig>();

	public static PlanetConfManager Instance
	{
		get{
			if (_Instance == null)
				_Instance = new PlanetConfManager ();
			return _Instance;
		}
	}
	public void AddConf(PlanetConfig conf )
	{
		//	Debug.LogError ("conf.id" + conf.id);
		if (Confs.ContainsKey (conf.Id))
			return;
		Confs.Add (conf.Id, conf);
	}
	public PlanetConfig GetConfById(int id)
	{
		if (Confs.ContainsKey (id)) {
			return Confs[id];
		}
		return null;
	}
}

public class RoleConfManager
{
	private static  RoleConfManager _Instance;
	public Dictionary<int, RoleConfig> Confs = new Dictionary<int, RoleConfig>();
	public static RoleConfManager Instance
	{
		get{
			if (_Instance == null)
				_Instance = new RoleConfManager ();
			return _Instance;
		}
	}
	public void AddBuildingConf(RoleConfig conf )
	{
		//	Debug.LogError ("conf.id" + conf.id);
		if (Confs.ContainsKey (conf.Id))
			return;
		Confs.Add (conf.Id, conf);
	}
	public RoleConfig GetConfById(int id)
	{
		if (Confs.ContainsKey (id)) {
			return Confs[id];
		}
		return null;
	}
}

public class SpaceStationConfManager
{
	private static  SpaceStationConfManager  _Instance;
	public Dictionary<int, SpaceStationConfig> Confs = new Dictionary<int, SpaceStationConfig>();
	public static SpaceStationConfManager Instance
	{
		get{
			if (_Instance == null)
				_Instance = new SpaceStationConfManager ();
			return _Instance;
		}
	}
	public void AddBuildingConf(SpaceStationConfig conf )
	{
		//	Debug.LogError ("conf.id" + conf.id);
		if (Confs.ContainsKey (conf.Id))
			return;
		Confs.Add (conf.Id, conf);
	}
	public SpaceStationConfig GetConfById(int id)
	{
		if (Confs.ContainsKey (id)) {
			return Confs[id];
		}
		return null;
	}
}


