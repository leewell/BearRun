using System;
using UnityEngine;

public interface IReusable
{
	/// <summary>
	/// 取出的时候调用
	/// </summary>
	void OnSpawn();

	/// <summary>
	/// 回收的时候调用
	/// </summary>
	void OnUnSpawn();
}