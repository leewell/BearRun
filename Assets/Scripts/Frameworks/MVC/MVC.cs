using System;
using System.Collections.Generic;
using UnityEngine;

public class MVC
{
	public static Dictionary<string, View> views = new Dictionary<string, View>(); // 名称 - view
	public static Dictionary<string, Model> models = new Dictionary<string, Model>(); // 名称 - model
	public static Dictionary<string, Type> commandMap = new Dictionary<string, Type>(); // 事件名称 - controller

	/// <summary>
	/// 注册view
	/// </summary>
	public static void RegisterView(View view)
    {
		//防止view的重复注册
        if (views.ContainsKey(view.Name))
        {
			views.Remove(view.Name);
		}
		view.RegisterAttentionEvent();
		views[view.Name] = view;
	}

	/// <summary>
	/// 注册model
	/// </summary>
	public static void RegisterModel(Model model)
    {
		models[model.Name] = model;
    }

	/// <summary>
	/// 注册controller
	/// </summary>
	public static void RegisterController(string eventName,Type controllerType)
    {
		commandMap[eventName] = controllerType;
    }

	/// <summary>
	/// 获取view
	public static T GetView<T>() where T : View
    {
        foreach (var v in views.Values)
        {
            if (v is T)
            {
				return (T)v;
            }
        }
		return null;
    }

	/// <summary>
	/// 获取model
	/// </summary>
	public static T GetModel<T>() where T : Model
	{
		foreach (var m in models.Values)
		{
			if (m is T)
			{
				return (T)m;
			}
		}
		return null;
	}

	/// <summary>
	/// 发送事件
	/// </summary>
	/// <param name="eventName">事件名称</param>
	/// <param name="data">事件参数</param>
	public static void SendEvent(string eventName,object  data = null)
    {
		//controller执行
        if (commandMap.ContainsKey(eventName))
        {
			Type t = commandMap[eventName];
			//利用反射生成控制器controller
			Controller controller = Activator.CreateInstance(t) as Controller;
			controller.Excute(data);
		}
		//view层处理
        foreach (var v in views.Values)
        {
            if (v.attentionList.Contains(eventName))
            {
				//执行
				v.HandleEvent(eventName, data);
            }
        }
    }
}