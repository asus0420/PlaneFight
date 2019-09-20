using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Enemy
{
    void Start()
    {

    }

    void Update()
    {
        Move();
        RestoreColor();
    }
    public override void Move()
    {
        base.Move();
    }
}
