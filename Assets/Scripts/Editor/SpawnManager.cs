using System;
using UnityEngine;
using UnityEditor;

public class SpawnManager : EditorWindow
{
    [MenuItem("Tools/click me")]
	public static void PatternSystem()
    {
        GameObject spawnManager = GameObject.Find("PatternManager");
        if (spawnManager != null)
        {
            PatternManager patternManager = spawnManager.GetComponent<PatternManager>();
            if (Selection.gameObjects.Length == 1)
            {
                var item = Selection.gameObjects[0].transform.Find("Item");
                if (item != null)
                {
                    Pattern pattern = new Pattern();
                    foreach (var child in item)
                    {
                        Transform childTrans = child as Transform;
                        if (childTrans != null)
                        {
                            //找到它对应的预制体
                            var prefab = PrefabUtility.GetCorrespondingObjectFromSource(childTrans.gameObject);
                            if (prefab != null)
                            {
                                PatternItem patternItem = new PatternItem
                                {
                                    pos = childTrans.localPosition,
                                    name = prefab.name
                                };
                                pattern.patternItems.Add(patternItem);
                            }
                        }
                    }
                    patternManager.patterns.Add(pattern);
                }
            }
        }
    }
}