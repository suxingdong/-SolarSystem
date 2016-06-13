using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;

public class BuildObj : Editor
{
	[MenuItem("BuildAssetBundle/Build Obj")]
	static void BuildAsset()
	{
		string applicationPath = Application.dataPath;
		string saveDir = applicationPath + "/StreamingAssets/";

		BuildAssetBundleOptions buildOp = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets
						| BuildAssetBundleOptions.DeterministicAssetBundle;

		BuildPipeline.PushAssetDependencies();
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);  
		Debug.Log("Selected Folder: " + path);
		if (path.Length != 0) 
		{
			path = path.Replace("Asset/", "");
			//string [] fileEntries = Directory.GetFiles(
		}
		BuildPipeline.PopAssetDependencies();
		
		EditorUtility.DisplayDialog("", "Completed", "OK");
		AssetDatabase.Refresh();
	}
}

