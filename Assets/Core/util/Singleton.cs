using System;
using UnityEngine;

namespace engine
{
    // 单体实例类
    public class Singleton<T> where T : new() {
        Singleton() { }

        public static T Instance {
            get { return SingletonCreator.instance; }
        }

        class SingletonCreator {
            static SingletonCreator() {}
            internal static readonly T instance = new T();
        }
    }

    // 专用于Unity脚本的单体类
    public class UnitySingleton<T> where T : MonoBehaviour {
        UnitySingleton() { }

        public static T I() { return SingletonCreator._inst; }

        class SingletonCreator {
            static SingletonCreator() {
                GameObject obj = new GameObject();
                obj.name = "____@" + typeof(T).ToString() + "(System Create)";
                _inst = obj.AddComponent<T>();
                UnityEngine.Object.DontDestroyOnLoad(obj);
            }
            internal static readonly T _inst;
        }
    }
}
