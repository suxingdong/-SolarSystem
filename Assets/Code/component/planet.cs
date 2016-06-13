using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable All

public class planet : MonoBehaviour {

    public int id;
    public bool isCanGen = false;
    public float period = 1;
    public float timer;
    public ArcReactor_Launcher aLauncher;
    private PlanetConfig config;
    private Vector3 temp = new Vector3(10, 10,0);
    private float angle = 0;

    private Dictionary<Role_Type, int> planetList;
    private float waterTime;
    private float fireTime;
    private float ironTime;
    private float treeTime;
    

    public int totalPopulation;
    void Start () {
        totalPopulation = 0;
        planetList = new Dictionary<Role_Type, int>();
        planetList[Role_Type.WATER] = 0;
        planetList[Role_Type.FIRE] = 0;
        planetList[Role_Type.IRON] = 0;
        planetList[Role_Type.TREE] = 0;
        waterTime = 0f;
        ironTime  = 0f;
        treeTime  = 0f;
        fireTime  = 0f;
        aLauncher = GetComponent<ArcReactor_Launcher>();
        config = PlanetConfManager.Instance.GetConfById(id);
    }
	
    
    void FixedUpdate()
    {
        if (!isCanGen)
            return;
        
        if (planetList[Role_Type.WATER] < config.Water && waterTime> config.WaterTime)
        {
            waterTime = 0;
            GenRole(Role_Type.WATER);
        }

        if (planetList[Role_Type.IRON] < config.Iron && ironTime > config.IronTime)
        {
            ironTime = 0;
            GenRole(Role_Type.IRON);
        }

        /*if (planetList[Role_Type.TREE] < config.Tree && treeTime > config.TreeTime)
        {
            treeTime = 0;
            GenRole(Role_Type.TREE);
        }

        if (planetList[Role_Type.FIRE] < config.Fire && fireTime > config.FireTime)
        {
            fireTime = 0;
            GenRole(Role_Type.FIRE);
        }*/

        waterTime += Time.deltaTime;
        //fireTime += Time.deltaTime;
        ironTime += Time.deltaTime;
        //treeTime += Time.deltaTime;

    }


    public void GenRole(Role_Type type)
    {
        switch (type)
        {
            case Role_Type.WATER:
                {
                    WaterSprite water = SpriteManager.Instance.Create<WaterSprite>(0);
                    if (water != null)
                    {
                        water.isRun = true;
                        water.curTurnPlanet = this;
                        planetList[type]++;
                        water.transform.parent= transform;
                        water.Init();
                    }
                }
                break;
            case Role_Type.FIRE:
                {
                    FireSprite water = SpriteManager.Instance.Create<FireSprite>(0);
                    if (water != null)
                    {
                        water.isRun = true;
                        water.curTurnPlanet = this;
                        planetList[type]++;
                        water.transform.parent= transform;
                        water.Init();
                    }
                }
                break;
            case Role_Type.IRON:
                {
                    IronSprite water = SpriteManager.Instance.Create<IronSprite>(0);
                    if (water != null)
                    {
                        water.isRun = true;
                        water.curTurnPlanet = this;
                        planetList[type]++;
                        water.transform.parent= transform;
                        water.Init();
                    }
                }
                break;
            case Role_Type.TREE:
                {
                    TreeSprite water = SpriteManager.Instance.Create<TreeSprite>(0);
                    if (water != null)
                    {
                        water.isRun = true;
                        water.curTurnPlanet = this;
                        planetList[type]++;
                        water.transform.parent = transform;
                        water.Init();
                    }
                }
                break;
            default:
                break;
        }
        //WaterSprite water = SpriteManager.Instance.Create<WaterSprite>(0);
        //if (water != null)
        //{
        //    water.isRun = true;
        //    water.curTurnPlanet = this;
        //    planetList[type]++;
        //    //water.transform.parent= planetArry[0].transform;
        //}
        //water.curTurnPlanet = 

    }
    void Update () {

   //     if(SceneManager.I().islasetline)
   //     {
			//Debug.Log ("POS " + SceneManager.I ().TargetPos);
			//aLauncher.transform.LookAt(SceneManager.I().TargetPos);

   //         timer -= Time.deltaTime;
   //         if (timer <= 0)
   //         {
   //             timer = timer + period;
			//	aLauncher.LaunchRay(SceneManager.I().TargetPos);
   //         }
   //     }
        
    }
}
