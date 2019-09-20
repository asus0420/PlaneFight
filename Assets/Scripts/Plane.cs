using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plane : MonoBehaviour
{
    #region 字段属性
    //当前血量
    private float hp = 10;
    //最大血量
    public float maxHp = 10;
    //当前mp
    private float mp = 0;
    //最大mp
    public float maxMp = 10;
    //玩家爆炸特效
    public GameObject exprisonPrefab;
    //玩家身上的meshRenderer
    private MeshRenderer mesh;
    //原始的颜色
    private Color currentColor;
    //计时器
    private float time = 0;
    //计时器属性
    public float TimeColck
    {
        get
        {
            return time;
        }
        set
        {
            time = value;
        }
    }
    //cd时间
    private float skillTime = 15.0f;
    //cd属性
    public float SkillTime
    {
        get
        {
            return skillTime;
        }
    }
    //玩家单实例
    private static Plane instance;
    public static Plane Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// hp属性
    /// </summary>
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    /// <summary>
    /// mp属性
    /// </summary>
    public float Mp
    {
        get
        {
            return mp;
        }
        set
        {
            mp = value;
        }
    }

    private bool isDie = false;
    public bool IsDie
    {
        get
        {
            return isDie;
        }
        set
        {
            isDie = value;
        }
    }
    #endregion

    #region  生命周期函数
    private void  Awake()
    {
        instance = this;
    }
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        currentColor = mesh.material.color;
    }
    void Update()
    {
        
        if ( hp<=0 )
        {
            Die();
        }
#if UNITY_ANDROID
        TouchMove();
#endif
        ChangeMp();
    }
    #endregion

    #region  逻辑函数
#if !UNITY_ANDROID || UNITY_EDITOR
    /// <summary>
    /// 鼠标拖动，但是会出现多指误触的
    /// </summary>
    private void OnMouseDrag()
    {
        if ( EventSystem.current.IsPointerOverGameObject() )  return;
        //获取鼠标的坐标
        Vector3 mousePosition = Input.mousePosition;
        //由于鼠标坐标没有z轴，所以将z轴固定死
        mousePosition.z = 10;
        //将屏幕坐标装换为世界坐标
        Vector3 pos = Camera.main.ScreenToWorldPoint( mousePosition );
        transform.position = pos;
    }
#endif
    //重力感应函数
    //private void GarvityMove()
    //{
    //    Vector3 acc = Input.acceleration;
    //    Vector3 dir = Vector3.zero;
    //    dir.x = acc.x;
    //    dir.z = acc.y;
    //    transform.Translate( dir * Time.deltaTime * 30.0f );
    //}
#if UNITY_ANDROID
    /// <summary>
    /// 手机移动函数，单指移动，防止多指误触
    /// </summary>
    private void TouchMove()
    {
        if ( EventSystem.current.currentSelectedGameObject == this )
        {
            return;
        }
        if ( Input.touchCount>0 )
        {
            Touch touch = Input.GetTouch( 0 );
            if ( touch.phase == TouchPhase.Moved )
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint( touch.position );
                pos.y = 0;
                transform.position = pos;
            }
        }
    }
#endif
    /// <summary>
    /// 触发函数
    /// </summary>
    /// <param name="coll"></param>
    private void OnTriggerEnter( Collider coll )
    {
        switch ( coll.tag )
        {
            case "Enemy":
            case "EnemyShip":
                GetHurt();
                break;
            case "HPProps":
                PropManager.Instance.CollectionDestroy( coll.GetComponent<Props>() );
                AddHp( 2 );
                break;
            case "MPProps":
                PropManager.Instance.CollectionDestroy( coll.GetComponent<Props>() );
                AddMp( 2 );
                break;
        }
    }
    /// <summary>
    /// 玩家受伤
    /// </summary>
    public void GetHurt()
    {
        if ( hp > 0 )
        {
            --hp;
            AttackFeedBack();
            if ( !IsInvoking( "CheckColor" ) )
            {
                Invoke( "CheckColor" , 0.3f );
            }
        }
    }
    /// <summary>
    /// 蓝量随时间增加
    /// </summary>
    public void ChangeMp()
    {
       
        if ( time <= skillTime )
        {
            //时间增加
            time += Time.deltaTime;
            //蓝量随时间增加
            mp += Time.deltaTime;
            //时间跟随蓝量变化
            time = mp / maxMp*skillTime;
            if ( mp>= maxMp )
            {
                mp = maxHp;
            }
        }
    }
    /// <summary>
    /// 玩家死亡特效
    /// </summary>
    private void Die()
    {
        isDie = true;
        //产生爆炸粒子效果
        Instantiate( exprisonPrefab , transform.position , Quaternion.identity );
        mesh.material.color = currentColor;
        Destroy( gameObject);
    }
    /// <summary>
    /// 攻击反馈
    /// </summary>
    private void AttackFeedBack()
    {
        mesh.material.color = Color.red;
    }
    /// <summary>
    /// 颜色检查，是否还原
    /// </summary>
    private void CheckColor()
    {
        if ( mesh.material.color == Color.red )
        {
            mesh.material.color = currentColor;
        }
    }
  
    /// <summary>
    /// 增加血量
    /// </summary>
    /// <param name="_hp"> 血量</param>
    public void AddHp( int _hp )
    {
        if ( hp<maxHp )
        {
            hp += _hp;
            if ( hp>=maxHp )
            {
                hp = maxHp;
            }
        }
    }
    /// <summary>
    /// 增加蓝量
    /// </summary>
    /// <param name="_mp"> 蓝量</param>
    public void AddMp( int _mp )
    {
        if ( mp<maxMp )
        {
            mp += _mp;
            if ( mp >= maxMp )
            {
                mp = maxMp;
            }
        }
    }

    #endregion
}