/**该文件实现的基本功能等
function: 资源管理器
date:2016-02-17
**/

using UnityEngine;
using System.Collections.Generic;

public class ResManager
{
    private static ResManager _instance;
    private Dictionary<string, Object> _loadedPrefab;

    private ResManager()
    {
        _loadedPrefab = new Dictionary<string, Object>();
    }

    public static ResManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ResManager();
            return _instance;
        }
    }

    public Object loadObject(string name)
    {
        if (_loadedPrefab.ContainsKey(name))
        {
            return _loadedPrefab[name];
        }
        Object prefab = Resources.Load<Object>(name);
        _loadedPrefab[name] = prefab;
        return prefab;
    }

    public GameObject loadPrefab(string name)
    {
        if (_loadedPrefab.ContainsKey(name))
        {
            return _loadedPrefab[name] as GameObject;
        }
        try
        {
            GameObject prefab = Resources.Load<GameObject>(name);
            _loadedPrefab[name] = prefab;
            return prefab;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
        }
        return null;
    }

    public GameObject createSingle(string name)
    {
        GameObject prefab = loadPrefab(name);
        if (prefab == null) return null;
        return Object.Instantiate(prefab) as GameObject;
    }

    public GameObject createSingle(string name, Vector3 pos)
    {
        GameObject prefab = loadPrefab(name);
        if (prefab == null) return null;
        return Object.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
    }

    public Material createMaterial(string fileName)
    {
        Object obj = loadObject(fileName);
        if (obj == null) return null;
        return new Material(obj as Material);
    }

    public GameObject createSingle(string name, Vector3 pos, Vector3 euler)
    {
        Quaternion quater = new Quaternion();
        quater.eulerAngles = euler;
        GameObject prefab = loadPrefab(name);
        if (prefab == null) return null;
        return Object.Instantiate(prefab, pos, quater) as GameObject;
    }

    public string loadText(string name)
    {
        return Resources.Load<TextAsset>(name).text;
    }

    public AudioClip loadSound(string name)
    {
        if (_loadedPrefab.ContainsKey(name))
        {
            return _loadedPrefab[name] as AudioClip;
        }
        AudioClip a = Resources.Load<AudioClip>(name);
        _loadedPrefab[name] = a;
        return a;
    }

    public Texture2D loadIcon(string name)
    {
        if (_loadedPrefab.ContainsKey(name))
        {
            return _loadedPrefab[name] as Texture2D;
        }
        Texture2D t = Resources.Load<Texture2D>(name);
        _loadedPrefab[name] = t;
        return t;
    }
}
