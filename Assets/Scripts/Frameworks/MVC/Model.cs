using System;
using UnityEngine;

public abstract class Model
{
	//名字标识
	public abstract string Name { get; }

	/// <summary>
	/// 发送事件
	/// </summary>
	/// <param name="eventName">事件名称</param>
	/// <param name="data">事件参数</param>
	protected void SendEvent(string eventName,object data = null)
    {
		MVC.SendEvent(eventName, data);
    }

	/// <summary>
	/// 获取视图
	/// </summary>
	public T GetView<T>() where T : View
	{
		return MVC.GetView<T>();
	}
}