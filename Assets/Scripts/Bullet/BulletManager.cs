using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹管理类
/// </summary>
public class BulletManager : MonoBehaviour
{
   //生存池
    private LinkedList<Bullet> lifePool;
    //死亡池
    private LinkedList<Bullet> deathPool;
    //单例
    private static BulletManager instance;
    public static BulletManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        //初始化对象池
        lifePool = new LinkedList<Bullet>();
        deathPool = new LinkedList<Bullet>();
        //初始化管理类的对象
        instance = this;
    }
    /// <summary>
    /// 寻找子弹
    /// </summary>
    /// <param name="type"> 枚举子弹类型</param>
    /// <returns></returns>
    public Bullet FindBullet(BulletType type)
    {
        Bullet bullet = null;
        //遍历死亡池
        foreach ( Bullet node in deathPool )
        {
            //如果找到该种类的
            if (  type == node.typeOfBullet)
            {
                bullet = node;
                //从死亡池移除
                deathPool.Remove( bullet );
                bullet.gameObject.SetActive( true );
                break;
            }
        }
        return bullet;
    }
    /// <summary>
    /// 添加到生存池
    /// </summary>
    public void AddToLifePool(Bullet bullet)
    {
        lifePool.AddLast( bullet );
    }
    /// <summary>
    /// 回收，从生存池中移除，添加到死亡池中
    /// </summary>
    /// <param name="bullet"></param>
    public void CollectionBullet(Bullet bullet)
    {
       
        lifePool.Remove( bullet );
        deathPool.AddLast( bullet );
        bullet.gameObject.SetActive( false );
    }
}
