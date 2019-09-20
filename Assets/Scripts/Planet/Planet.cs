using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 行星类
/// </summary>
/// 
public enum PlanetType
{
    PLANET0,
    PLANET1,
    PLANET2,
}
public class Planet : MonoBehaviour
{
    //敌人移动速度
    public float moveSpeed;
    //敌人血量
    public float hp;
    //敌人初始血量
    public float initialHp;
    //敌人的爆炸预制体
    public GameObject explisionPrefab;
    //屏幕的边界值
    public float minX;
    public float maxX;
    //敌人种类
    public PlanetType typeOfPlanet = PlanetType.PLANET0;
    //原始的颜色
    protected Color nowColor;
    //MeshRenderer组件
    protected MeshRenderer mesh;
    //超出屏幕的z坐标的值
    public float zOverShot = 5.33f;
    //超出屏幕的x坐标的值
    public float xOverShot;
    //敌人死亡的分数
    public float score;


    void Awake()
    {
        mesh = transform.GetComponentInChildren<MeshRenderer>();
        nowColor = mesh.material.color;
    }
    private void Start()
    {
        //获取改分辨率下的z轴长度
       
    }
    void OnEnable()
    {
        RestoreHp();
    }
    public void Update()
    {
        Move();
        //如果血量小于0敌人死亡
        if ( hp<=0 )
        {
            Die();
        }
    }
    /// <summary>
    /// 敌人移动基本方法
    /// </summary>
    public virtual void Move()
    {
        if ( transform.position.z <= -zOverShot )
        {
            PlanetManager.Instance.CollectionPlanet( this );
        }
        transform.Translate( -Vector3.forward * Time.deltaTime * moveSpeed );
    }
    /// <summary>
    /// 敌人受伤
    /// </summary>
    public void GetHeart()
    {
        //敌人血量大于0时
        if ( hp > 0 )
        {
            --hp;
            AttackFeedBack();
            if ( !IsInvoking("CheckColor") )
            {
                  Invoke( "CheckColor" , 0.3f );
            }
        }
    }
    /// <summary>
    /// 敌人死亡
    /// </summary>
    public void Die()
    {
        //执行爆炸特效
        Instantiate( explisionPrefab , transform.position , Quaternion.identity );
        //敌人颜色还原
        mesh.material.color = nowColor;
        //加分
        UICanvas.Instance.scoreTotal += score;
        //回收死亡敌人
        PlanetManager.Instance.CollectionPlanet( this );
    }
    /// <summary>
    /// 攻击反馈
    /// </summary>
    public void AttackFeedBack()
    {
        mesh.material.color = Color.green;
    }
    /// <summary>
    /// 检查颜色
    /// </summary>
    public void CheckColor()
    {
        if ( mesh.material.color == Color.green )
        {
            mesh.material.color = nowColor;
        }
    }
    /// <summary>
    /// 随机位置
    /// </summary>
    public void RandomPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Random.Range( minX , maxX );
        transform.position = pos;
    }
    /// <summary>
    /// 重置血量虚方法
    /// </summary>
    public  void RestoreHp()
    {
        hp = initialHp;
    }
}
