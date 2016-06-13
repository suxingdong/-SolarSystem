using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : UnityAllSceneSingleton<SpriteManager> {


    public List<WaterSprite> waterSpriteList;
    public List<TreeSprite> treeSpriteList;
    public List<FireSprite> fireSpriteList;
    public List<IronSprite> ironSpriteList;

    public override void Awake()
    {
        base.Awake();
        waterSpriteList = new List<WaterSprite>();
        treeSpriteList = new List<TreeSprite>();
        fireSpriteList = new List<FireSprite>();
        ironSpriteList = new List<IronSprite>();
    }

    public T Create<T>(int localID) where T : Role ,new()
    {
        T t = new T();
        t.localID = localID;     
        AddToList(t);
        return t;
    }

    protected void AddToList(Role role)
    {
        switch (role.type)
        {
            case Role_Type.WATER:
                waterSpriteList.Add(role as WaterSprite);
                break;
            case Role_Type.FIRE:
                fireSpriteList.Add(role as FireSprite);
                break;
            case Role_Type.IRON:
                ironSpriteList.Add(role as IronSprite);
                break;
            case Role_Type.TREE:
                treeSpriteList.Add(role as TreeSprite);
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        if (waterSpriteList != null)
        {
            foreach (var kv in waterSpriteList)
            {
                kv.FixedUpdate();
            }
        }

        if (fireSpriteList != null)
        {
            foreach (var kv in fireSpriteList)
            {
                kv.FixedUpdate();
            }
        }

        if (ironSpriteList != null)
        {
            foreach (var kv in ironSpriteList)
            {
                kv.FixedUpdate();
            }
        }

        if (treeSpriteList != null)
        {
            foreach (var kv in treeSpriteList)
            {
                kv.FixedUpdate();
            }
        }

    }



    public void ChangeTarget(planet p)
    {
        foreach (var kv in treeSpriteList)
        {
            kv.ChangePlanet(p);
        }

        foreach (var kv in waterSpriteList)
        {
            kv.ChangePlanet(p);
        }

        foreach (var kv in fireSpriteList)
        {
            kv.ChangePlanet(p);
        }

        foreach (var kv in ironSpriteList)
        {
            kv.ChangePlanet(p);
        }
    }

}
