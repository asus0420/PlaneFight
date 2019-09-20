using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawn : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int shootNum;
    private void Start()
    {
        InvokeRepeating( "CreateBullets" , 1f , 1f );
    }

    void CreateBullets()
    {
        for ( int i = -shootNum / 2 ; i <= shootNum / 2 ; i++ )
        {
            EnemyBullet eBullet = null;
            eBullet = EnemyBulletManager.Instance.FindInDeathPool(BulletType.BULLET2);
            if ( null == eBullet ) 
            {
                GameObject gObject = Instantiate( bulletPrefab , transform.position , Quaternion.identity );
                eBullet = gObject.GetComponent<EnemyBullet>();
                gObject.transform.SetParent( GameObject.FindGameObjectWithTag( "BulletCollection" ).transform );
            }
            eBullet.transform.position = transform.position;
            eBullet.transform.rotation = Quaternion.Euler( new Vector3( 0 , i * 10 , 0 ) );
            EnemyBulletManager.Instance.AddToLifePool( eBullet );
        }
    }
}
