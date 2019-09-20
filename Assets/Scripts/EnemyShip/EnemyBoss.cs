using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField]
    [Tooltip("敌机子弹孵化器")]
    private Behaviour spawn; 
    void Start()
    {
        spawn.enabled = false;
    }

    void Update()
    {
        Move();
        RestoreColor();
    }
    public override void Move()
    {
        if ( transform.position.z >= 4 )
        {
            transform.Translate( -Vector3.forward * Time.deltaTime * moveSpeed );
        }
        else
        {
            moveSpeed = 0;
            if ( !spawn.enabled )
            {
                spawn.enabled = true;
            }
          
        }
       
    }
}
