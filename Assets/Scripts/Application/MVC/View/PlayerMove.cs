using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : View
{
    //奔跑速度
    public float speed = 20f;
    // 射门时球的移动速度
    public float goalSpeed = 40f;
    //水平移动速度
    private float moveSpeed = 43;
    private CharacterController m_cc;
    private GameObject ballObj;
    private GameObject trailObj;
    //角色皮肤
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MeshRenderer meshRenderer;

    private InputDirection m_inputDir = InputDirection.Null;
    private Vector3 m_mousePos = Vector3.zero;
    bool activeInput = false;

    //当前所处的跑道编号（0-最左边；1-中间；2最右边）
    int m_nowIndex = 1;
    //目标跑道
    int m_targetIndex = 1;
    //X轴的移动距离
    float m_xDistance;
    //Y轴的跳高距离
    float m_yDistance;
    //跳跃的最大高度
    const float m_jumpValue = 5f;
    //重力
    const float gravity = 9.8f;

    //是否是向下滑动状态
    private bool m_isSlide = false;
    //滑动时间
    private float m_slideTime;

    //记录当前跑了多少米
    private float m_speedAddCount;
    //通过跑了多远来增加速度
    private const float m_speedAddDis = 200f;
    //速度的增加增量
    private const float m_speedAddRate = 1f;
    //最大速度
    private const float m_maxSpeed = 50f;

    private GameModel gm;
    //记录撞击时候的速度
    private float m_maskSpeed;
    //撞击后提速的速率
    private const int m_AddRate = 3;
    //现在是否正在撞击
    private bool m_isHit = false;
    //是否进球
    private bool m_isGoal = false;

    //item相关
    //金币的倍数
    private int m_doubleTime = 1;
    //技能时间
    private int m_skillTime;
    //双倍金币协程
    private IEnumerator MultiplyCor;
    //磁铁协程
    private IEnumerator MagnetCor;
    //无敌状态协程
    private IEnumerator InvincibleCor;
    //射门倒计时协程
    private IEnumerator GoalCor;
    //磁铁的球形触发器
    private SphereCollider m_magnetCollider;
    //是否无敌
    private bool m_isInvincible = false;

    public override string Name => Const.V_PlayerMove;

    public float Speed { get => speed; 
        set {
            speed = value;
            if (speed > m_maxSpeed)
            {
                speed = m_maxSpeed;
            }
        } 
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Const.E_OnGoalClick:
                OnGoal();
                break;
            default:
                break;
        }
    }

    public override void RegisterAttentionEvent()
    {
        attentionList.Add(Const.E_OnGoalClick);
    }

    private void Awake()
    {
        m_cc = GetComponent<CharacterController>();
        ballObj = transform.Find("Ball").gameObject;
        trailObj = transform.Find("Effect/Trail").gameObject;
        m_magnetCollider = GetComponentInChildren<SphereCollider>();
        m_magnetCollider.enabled = false;
        gm = GetModel<GameModel>();
        m_skillTime = gm.SkillTime;

        skinnedMeshRenderer = transform.Find("Jersey_BaXi").GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = Game.Instance.staticData.GetPlayerClothInfo(gm.TakeOnSkinAndCloth.SkinId, gm.TakeOnSkinAndCloth.ClothId).material;
        meshRenderer = transform.Find("Ball/Ball_SangBaRongYao").GetComponent<MeshRenderer>();
        meshRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.TakeOnFootball).material;
    }

    private void Start()
    {
        StartCoroutine(UpdateAction());
    }

    IEnumerator UpdateAction()
    {
        while (true)
        {
            if (gm.IsPlay && !gm.IsPause) // 如果正在游戏，并且游戏没有暂停，才能移动主角
            {
                UpdateDis();
                m_yDistance -= gravity * Time.deltaTime;
                m_cc.Move((transform.forward * Speed + new Vector3(0, m_yDistance, 0)) * Time.deltaTime);
                MoveControl();
                UpdatePosition();
                UpdateSpeed();
            }
            yield return 0;
        }
    }

    /// <summary>
    /// 更新距离UI
    /// </summary>
    private void UpdateDis()
    {
        DistanceArgs e = new DistanceArgs { distance = (int)transform.position.z };
        SendEvent(Const.E_UpdateDis, e);
    }

    /// <summary>
    /// 更新位置
    /// </summary>
    void UpdatePosition()
    {
        GetInputDirection();
        switch (m_inputDir)
        {
            case InputDirection.Null:
                break;
            case InputDirection.Left:
                if (m_targetIndex > 0)
                {
                    m_targetIndex--;
                    m_xDistance = -2f;
                    SendMessage("AnimManager", m_inputDir);
                    Game.Instance.sound.PlayEffectAudio("Se_UI_Huadong");
                }
                break;
            case InputDirection.Right:
                if (m_targetIndex < 2)
                {
                    m_targetIndex++;
                    m_xDistance = 2f;
                    SendMessage("AnimManager", m_inputDir);
                    Game.Instance.sound.PlayEffectAudio("Se_UI_Huadong");
                }
                break;
            case InputDirection.Down:
                if (!m_isSlide)
                {
                    m_slideTime = 0.733f;
                    m_isSlide = true;
                    if (m_cc.isGrounded)
                    {
                        SendMessage("AnimManager", m_inputDir);
                        Game.Instance.sound.PlayEffectAudio("Se_UI_Slide");
                    }
                }
                break;
            case InputDirection.Up:
                if (m_cc.isGrounded)
                {
                    m_yDistance = m_jumpValue;
                    SendMessage("AnimManager", m_inputDir);
                    Game.Instance.sound.PlayEffectAudio("Se_UI_Jump");
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 更新速度
    /// </summary>
    private void UpdateSpeed()
    {
        m_speedAddCount += Speed * Time.deltaTime;
        if (m_speedAddCount > m_speedAddDis)
        {
            m_speedAddCount = 0;
            Speed += m_speedAddRate;
        }
    }

    /// <summary>
    /// 移动控制
    /// </summary>
    private void MoveControl()
    {
        //控制水平的移动
        if (m_nowIndex != m_targetIndex)
        {
            float move = Mathf.Lerp(0,m_xDistance,moveSpeed * Time.deltaTime);
            transform.position += new Vector3(move, 0, 0);
            m_xDistance -= move;
            if (Mathf.Abs(m_xDistance) < 0.1f) //已经移动到了对应跑道
            {
                m_xDistance = 0;
                m_nowIndex = m_targetIndex;
                int xResult = 0;
                switch (m_nowIndex)
                {
                    case 0:
                        xResult = -2;
                        break;
                    case 1:
                        xResult = 0;
                        break;
                    case 2:
                        xResult = 2;
                        break;
                }
                transform.position = new Vector3(xResult, transform.position.y, transform.position.z);
            }
        }

        if (m_isSlide)
        {
            m_slideTime -= Time.deltaTime;
            if (m_slideTime < 0)
            {
                m_isSlide = false;
                m_slideTime = 0;
            }
        }
    }

    /// <summary>
    /// 获取输入（鼠标/键盘）
    /// </summary>
    void GetInputDirection()
    {
        m_inputDir = InputDirection.Null;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            m_mousePos = Input.mousePosition;
        }

        //鼠标输入
        if (Input.GetMouseButton(0) && activeInput)
        {
            Vector3 dir = Input.mousePosition - m_mousePos;
            if (dir.magnitude > 15) // 如果滑动距离超过最小滑动距离
            {
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y) && dir.x > 0) // 向右滑动
                {
                    m_inputDir = InputDirection.Right;
                }
                else if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y) && dir.x < 0) // 向左滑动
                {
                    m_inputDir = InputDirection.Left;
                }
                else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y) && dir.y > 0) // 向上滑动
                {
                    m_inputDir = InputDirection.Up;
                }
                else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y) && dir.y < 0) // 向下滑动
                {
                    m_inputDir = InputDirection.Down;
                }
                activeInput = false;
            }
        }

        //键盘输入
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_inputDir = InputDirection.Right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_inputDir = InputDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_inputDir = InputDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_inputDir = InputDirection.Down;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gm.IsPause = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            gm.IsPause = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.BigFence)
        {
            if (m_isSlide || m_isInvincible)
            {
                return;
            }
            other.gameObject.SendMessage("HitPlayer", transform.position);
            //播放撞击音效
            Game.Instance.sound.PlayEffectAudio("Se_UI_Hit");
            //减速
            HitObstacles();
        }
        else if (other.gameObject.tag == Tag.SmallFence)
        {
            if (m_isInvincible)
            {
                return;
            }
            other.gameObject.SendMessage("HitPlayer", transform.position);
            //播放撞击音效
            Game.Instance.sound.PlayEffectAudio("Se_UI_Hit");
            //减速
            HitObstacles();
        }
        else if (other.gameObject.tag == Tag.Block || other.gameObject.tag == Tag.SmallBlock)
        {
            //如果是撞到这个，就结束游戏
            Game.Instance.sound.PlayEffectAudio("Se_UI_End");
            if (other.gameObject.tag == Tag.SmallBlock)
            {
                other.transform.parent.parent.SendMessage("HitPlayer", transform.position);
            }
            else
            {
                other.gameObject.SendMessage("HitPlayer", transform.position);
            }
            //发送游戏结束事件
            SendEvent(Const.E_EndGame);
        }
        else if (other.gameObject.tag == Tag.BeforeTrigger) // 撞击到汽车的触发器
        {
            other.transform.parent.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);
        }else if (other.gameObject.tag == Tag.BeforeGoalTrigger) // 撞击射门的before trigger
        {
            //发消息给UIBoard，准备射门
            SendEvent(Const.E_HitGoalTrigger);
            //显示加速特效
            Game.Instance.objectPool.Spawn("FX_JiaSu", trailObj.transform.parent);
        }else if (other.tag == Tag.Goalkeeper)
        {
            HitObstacles();
            other.transform.parent.parent.parent.SendMessage("HitGoalKeeper",SendMessageOptions.RequireReceiver);
        }else if (other.tag == Tag.BallDoor) // 撞到了球门
        {
            if (m_isGoal)
            {
                m_isGoal = false;
                return;
            }
            //减速
            HitObstacles();
            //生成球网在身上
            Game.Instance.objectPool.Spawn("Effect_QiuWang",trailObj.transform.parent);
            //发送消息到球网，处理其他事情
            other.transform.parent.parent.SendMessage("HitDoor", m_nowIndex);
        }
    }

    /// <summary>
    /// 撞击到障碍物后减速
    /// </summary>
    public void HitObstacles()
    {
        if (m_isHit)
        {
            return;
        }
        m_isHit = true;
        m_maskSpeed = Speed;
        Speed = 0;
        StartCoroutine(DecreaseSpeed());
    }

    IEnumerator DecreaseSpeed()
    {
        while (Speed <= m_maskSpeed)
        {
            Speed += m_AddRate * Time.deltaTime;
            yield return 0;
        }
        m_isHit = false;
    }

    /// <summary>
    /// 撞击到技能物品（磁铁/双倍金币/无敌）
    /// </summary>
    /// <param name="item">物品类型</param>
    public void HitItem(ItemKind item)
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 0,
            kind = item
        };
        SendEvent(Const.E_HitItem, e);
    }

    /// <summary>
    /// 吃金币
    /// </summary>
    public void HitCoin()
    {
        CoinArgs e = new CoinArgs { coin = m_doubleTime };
        SendEvent(Const.E_UpdateCoin, e);
    }

    /// <summary>
    /// 吃双倍金币
    /// </summary>
    public void HitCoinMultiply()
    {
        if (MultiplyCor != null)
        {
            StopCoroutine(MultiplyCor);
        }
        MultiplyCor = MultiplyCoroutine();
        StartCoroutine(MultiplyCor);
    }

    IEnumerator MultiplyCoroutine()
    {
        m_doubleTime = 2;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_skillTime);
        m_doubleTime = 1;
    }

    /// <summary>
    /// 吃到磁铁
    /// </summary>
    public void HitMagnet()
    {
        if (MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }

    IEnumerator MagnetCoroutine()
    {
        m_magnetCollider.enabled = true;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_skillTime);
        m_magnetCollider.enabled = false;
    }

    /// <summary>
    /// 吃到加时间道具
    /// </summary>
    public void HitAddTime()
    {
        SendEvent(Const.E_HitAddTime);
    }

    /// <summary>
    /// 吃到无敌道具
    /// </summary>
    public void HitInvincible()
    {
        if (InvincibleCor != null)
        {
            StopCoroutine(InvincibleCor);
        }
        InvincibleCor = InvincibleCoroutine();
        StartCoroutine(InvincibleCor);
    }

    IEnumerator InvincibleCoroutine()
    {
        m_isInvincible = true;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_skillTime);
        m_isInvincible = false;
    }

    /// <summary>
    /// 射门事件
    /// </summary>
    private void OnGoal()
    {
        if (GoalCor != null)
        {
            StopCoroutine(GoalCor);
        }
        ballObj.SetActive(false);
        trailObj.SetActive(true);
        SendMessage("NoticePlayShoot");
        GoalCor = GoalCoroutine();
        StartCoroutine(GoalCor);
    }

    IEnumerator GoalCoroutine()
    {
        while (true)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                trailObj.transform.Translate(trailObj.transform.forward * Time.deltaTime * goalSpeed);
                //trailObj.transform.position += Vector3.forward * Time.deltaTime * goalSpeed;
            }
            yield return 0;
        }
    }

    /// <summary>
    /// 进球事件
    /// </summary>
    public void HitBallDoor()
    {
        //停止射门的协程
        StopCoroutine(GoalCor);
        //球体归位
        trailObj.transform.localPosition = new Vector3(0,0.5f,5f);
        trailObj.SetActive(false);
        ballObj.SetActive(true);
        //修改进球状态
        m_isGoal = true;
        //生成进球特效
        Game.Instance.objectPool.Spawn("FX_GOAL", trailObj.transform.parent);
        //播放进球音效
        Game.Instance.sound.PlayEffectAudio("Se_UI_Goal");
        //发送事件给UIBoard，刷新分数
        SendEvent(Const.E_ShootGoal);
    }
}