using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹种类
/// </summary>
public enum BulletType
{
    BULLET0 ,
    BULLET1 ,
    BULLET2 ,
}
public class Bullet : MonoBehaviour
{
    //初始化子弹种类为第一种
    public BulletType typeOfBullet = BulletType.BULLET0;
    //子弹的移动速度
    public float moveSpeed = 10f;
    //超出屏幕的z坐标的值
    public float zOverShot = 5.33f;
    //超出屏幕的x坐标的值
    public float xOverShot = 2.8f;
    public AudioClip shootClip;
     public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( transform.position.z >= zOverShot || transform.position.x <= -xOverShot || transform.position.x >= xOverShot )
        {
            BulletManager.Instance.CollectionBullet( this );
        }
        else
        {
            transform.Translate( Vector3.forward * Time.deltaTime * moveSpeed );
        }
    }

    private void OnTriggerEnter( Collider coll )
    {
        switch ( coll.tag )
        {
            case "Enemy":
                BulletManager.Instance.CollectionBullet( this );
                Planet enemy = coll.GetComponent<Planet>();
                enemy.GetHeart();
                break;

            case "EnemyShip":
                BulletManager.Instance.CollectionBullet( this );
                Enemy enemyship = coll.GetComponent<Enemy>();
                enemyship.GetHurt();
                break;
        }
    }
}
