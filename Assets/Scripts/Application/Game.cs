using System;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]
public class Game : MonoSingleton<Game>
{
    [HideInInspector]
	public ObjectPool objectPool;
    [HideInInspector]
	public Sound sound;
    [HideInInspector]
	public StaticData staticData;

    protected override void Awake()
    {
        base.Awake();
        //注册委托调用
        //SceneManager.sceneLoaded += LevelWasLoaded;
    }

    private void Start()
    {
        //跳转场景时不删除对象
        DontDestroyOnLoad(gameObject);

        objectPool = ObjectPool.Instance;
        sound = Sound.Instance;
        staticData = StaticData.Instance;

        //初始化注册startup controller
        RegisterController(Const.E_StartUp, typeof(StartUpController));
        //游戏启动
        SendEvent(Const.E_StartUp);

        //跳转场景
        LoadLevel(1);
    }

    public void LoadLevel(int level)
    {
        //获取当前场景索引
        SceneArgs args = BuildScenesArgs(SceneManager.GetActiveScene().buildIndex);
        //发送退出场景事件
        SendEvent(Const.E_ExitScene, args);

        //发送加载新场景的事件
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    /// <summary>
    /// 当场景加载完成时回调此方法，注意，此方法已被弃用，和下面的那个方法2选1
    /// </summary>
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("跳转场景：" + level);
        SceneArgs args = BuildScenesArgs(level);
        SendEvent(Const.E_EnterScene, args);
    }

    /// <summary>
    /// 由于OnLevelWasLoaded方法被弃用，所以用委托的方法调用此方法，和上面的方法2选1
    /// </summary>
    //void LevelWasLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("跳转场景：" + scene.buildIndex);
    //    SceneArgs args = BuildScenesArgs(scene.buildIndex);
    //    SendEvent(Const.E_EnterScene, args);

    //    //方法执行完成后，移除委托
    //    SceneManager.sceneLoaded -= LevelWasLoaded;
    //}

    /// <summary>
    /// 构建SceneArgs对象
    /// </summary>
    /// <param name="level">场景索引</param>
    SceneArgs BuildScenesArgs(int level)
    {
        SceneArgs args = new SceneArgs() { scenesIndex = level };
        return args;
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="data">事件参数</param>
    void SendEvent(string eventName,object data = null)
    {
        MVC.SendEvent(eventName, data);
    }

    /// <summary>
    /// 注册startup controller
    /// </summary>
    void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }
}