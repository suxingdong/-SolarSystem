using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class genSceneMap : Editor
{
	[MenuItem("SceneMap/Scene01")]
	static void Scene01()
	{
		Debug.Log ("scene01 test");
	}
	
	[MenuItem("SceneMap/Scene02")]
	static void Scene02()
	{
		Debug.Log ("Scene02 test");
	}


}
