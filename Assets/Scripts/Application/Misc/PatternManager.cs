using System;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoSingleton<PatternManager>
{
	public List<Pattern> patterns = new List<Pattern>();
}

[Serializable]
//一个游戏物体的信息
public class PatternItem
{
	public string name;
	public Vector3 pos;
}

[Serializable]
public class Pattern
{
	public List<PatternItem> patternItems = new List<PatternItem>();
}