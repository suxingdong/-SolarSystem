using UnityEngine;
using System.Collections;


public struct strPlayer
{
    public long uid;
    public int level;
    public string name;
}
public class BasePlayer
{
    protected strPlayer info;

    public virtual void commond()
    {
        
    }

}
