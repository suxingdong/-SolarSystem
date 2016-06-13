using UnityEngine;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable All

public enum Role_Type
{
    WATER,
    FIRE,
    TREE,
    IRON
}

public enum Role_State
{
    
    REVOLUTION,
    MOVETO,
}


public class Role
{
    public Role_Type type;
    public int Id;
    public int localID;
	public string name;
    public GameObject gameObject;
    public Transform transform;
    protected float mass;
    protected bool isChangePlant = false;
    protected GameObject virtualObj;
    // ReSharper disable once InconsistentNaming
    public float curHp; //当前血量


    // ReSharper disable once InconsistentNaming
    public bool isRun;
    public Role()
    {

    }

    public virtual void Init()
    {
        
    }
    public virtual void FixedUpdate()
    {

    }

    public virtual void ChangePlanet(planet p)
    {

    }

    public void RotateAround(float r)
    {
        transform.RotateAround(curTurnPlanet.transform.position, new Vector3(0, r, 0), -60 * Time.deltaTime);
    }

    public planet curTurnPlanet //当前围绕的星球
    {
        set; get;
    }
    public planet tarTurnPlanet //将要围绕的星球
    {
        set; get;
    }

    public virtual void Accack(Role target)
    {
        
    }


}
