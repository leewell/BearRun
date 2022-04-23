using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
	/// <summary>
	/// 事件关心列表
	/// </summary>
	[HideInInspector]
	public List<string> attentionList = new List<string>();
	//名字标识
	public abstract string Name { get; }

	/// <summary>
	/// 处理事件
	/// </summary>
	/// <param name="eventName">事件名称</param>
	/// <param name="data">事件参数</param>
	public abstract void HandleEvent(string eventName,object data);

	/// <summary>
	/// 显示view
	/// </summary>
	public virtual void Show()
    {
		gameObject.SetActive(true);
    }

	/// <summary>
	/// 隐藏view
	/// </summary>
	public virtual void Hide()
    {
		gameObject.SetActive(false);
	}

	/// <summary>
	/// 发送事件
	/// </summary>
	/// <param name="eventName">事件名称</param>
	/// <param name="data">事件参数</param>
	protected void SendEvent(string eventName, object data = null)
	{
		MVC.SendEvent(eventName, data);
	}

	/// <summary>
	/// 注册关心事件列表
	/// </summary>
	public virtual void RegisterAttentionEvent()
    {

    }

	/// <summary>
	/// 获取model
	/// </summary>
	protected T GetModel<T>() where T : Model
    {
		return MVC.GetModel<T>();
    }
}