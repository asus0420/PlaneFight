using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    void Start()
    {
        typeOfBullet = BulletType.BULLET2;
        zOverShot = 5.33f;
        xOverShot = 2.8f;
    }
    void Update()
    {
        if ( transform.position.z <= -zOverShot || transform.position.x <= -xOverShot || transform.position.x >= xOverShot )
        {
            EnemyBulletManager.Instance.CollectionDestroy( this );
        }
        else
        {
            transform.Translate( -Vector3.forward * Time.deltaTime * moveSpeed );
        }
    }

    private void OnTriggerEnter( Collider coll )
    {
        switch ( coll.tag )
        {
            case "Player":
                EnemyBulletManager.Instance.CollectionDestroy( this );
                Plane.Instance.GetHurt();
                break;
            case "Bullet":
                EnemyBulletManager.Instance.CollectionDestroy( this );
                BulletManager.Instance.CollectionBullet( coll.GetComponent<Bullet>() );
                break;
        }
    }
}
