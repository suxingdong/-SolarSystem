using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public abstract class BaseScene
{
    public abstract void Start();

    public BaseScene()
    {

    }
    public virtual void Updata() { }
    public virtual void init() { }
    public virtual void ClickScene() { }
    public void reset()
    {
        
    }
}

