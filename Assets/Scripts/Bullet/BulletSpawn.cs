using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletSpawn : MonoBehaviour
{
    //获取子弹的预制体
    public GameObject[] bulletPrefab = new GameObject[2];
    //子弹的数量
    private int bulletNum = 0;
    //初始化子弹的种类
    private BulletType type = BulletType.BULLET0;
    //预制体的下标
    private int count  = 0;
    void Start()
    {
        //重复生成子弹
        InvokeRepeating( "CreateBullets" , 0.5f , 0.3f );
    }


    void Update()
    {
        ShotGun();
    }

    /// <summary>
    /// 创建子弹
    /// </summary>
    public void CreateBullets()
    {
        for ( int i = -bulletNum/2 ; i <= bulletNum/2 ; i++ )
        {
             //创建子弹的对象，并赋空
            Bullet bullet = null;
            //在死亡池中寻找
            bullet = BulletManager.Instance.FindBullet( type );
            //如果为空
            if ( null == bullet )
            {
                //实例化生成一个新的子弹
                GameObject go = Instantiate( bulletPrefab[count] );
                bullet = go.GetComponent<Bullet>();
                go.transform.SetParent( GameObject.FindGameObjectWithTag( "BulletCollection" ).transform );
            }
            //设置子弹的种类为当前type
            bullet.typeOfBullet = type;
            //设置子弹生成位置
            bullet.transform.position = transform.position;
            AudioSource.PlayClipAtPoint( bullet.shootClip , transform.position );
            //设置子弹旋转角度
            bullet.transform.rotation = Quaternion.Euler( new Vector3( 0 , i * 10 , 0 ) );
            //添加到生存池中
            BulletManager.Instance.AddToLifePool( bullet );
        }
    }
    /// <summary>
    /// 按下空格键，变成散弹
    /// </summary>
    private void ShotGun()
    {
        if ( Input.GetKeyDown(KeyCode.Space) || UICanvas.Instance.isCanShoot )
        {
            bulletNum = 7;
            type = BulletType.BULLET1;
            count = 1;
            UICanvas.Instance.isCanShoot = false;
            if ( !IsInvoking( "BulletRestore" ) )
            {
                Invoke( "BulletRestore" , 5 );
            }
        }
    }
    /// <summary>
    /// 子弹还原成单条
    /// </summary>
    private void BulletRestore()
    {
        bulletNum = 0;
        type = BulletType.BULLET0;
        count = 0;
    }
}
