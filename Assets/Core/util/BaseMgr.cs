﻿using UnityEngine;
using System.Collections;


public class BaseMgr<T> where T :  new()
{
    private static T instance;

    public static T I()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}

