using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    LinkedList<EnemyBullet> lifeBulletsPool;
    LinkedList<EnemyBullet> deatehBulletPool;

    private static EnemyBulletManager instance;

    public static EnemyBulletManager Instance
    {
        get { return instance; } 
    }

    private void Awake()
    {
        lifeBulletsPool = new LinkedList<EnemyBullet>();
        deatehBulletPool = new LinkedList<EnemyBullet>();
        instance = this;
    }

    private void Start()
    {
 
    }
    /// <summary>
    /// 死亡池寻找
    /// </summary>
    /// <param name="type"> 子弹种类</param>
    /// <returns></returns>
    public EnemyBullet FindInDeathPool( BulletType type )
    {
        EnemyBullet eBullet = null;
        foreach ( EnemyBullet item in deatehBulletPool )
        {
            if ( item.typeOfBullet == type )
            {
                eBullet = item;
                deatehBulletPool.Remove( eBullet );
                eBullet.gameObject.SetActive( true );
                break;
            }
        }
        return eBullet;
    }
    /// <summary>
    /// 添加到生存池
    /// </summary>
    /// <param name="eBullet"></param>
    public void AddToLifePool( EnemyBullet eBullet )
    {
        lifeBulletsPool.AddLast( eBullet );
    }
    /// <summary>
    /// 回收作废子弹
    /// </summary>
    /// <param name="eBullet"></param>
    public void CollectionDestroy(EnemyBullet eBullet)
    {
        lifeBulletsPool.Remove( eBullet );
        deatehBulletPool.AddLast( eBullet );
        eBullet.gameObject.SetActive( false );
    }
}
