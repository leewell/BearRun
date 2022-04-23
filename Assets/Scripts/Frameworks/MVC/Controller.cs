using System;
using UnityEngine;

public abstract class Controller
{
	/// <summary>
	/// 执行事件
	/// </summary>
	public abstract void Excute(object data);

	/// <summary>
	/// 获取model
	/// </summary>
	protected T GetModel<T>() where T : Model
	{
		return MVC.GetModel<T>();
	}

	/// <summary>
	/// 获取view
	/// </summary>
	protected T GetView<T>() where T : View
	{
		return MVC.GetView<T>();
	}

	/// <summary>
	/// 注册模型
	/// </summary>
	protected void RegisterModel(Model model)
    {
		MVC.RegisterModel(model);
    }

	/// <summary>
	/// 注册视图
	/// </summary>
	protected void RegisterView(View view)
    {
		MVC.RegisterView(view);
    }

	/// <summary>
	/// 注册controller
	/// </summary>
	protected void RegisterController(string eventName, Type controllerType)
    {
		MVC.RegisterController(eventName, controllerType);
    }
}