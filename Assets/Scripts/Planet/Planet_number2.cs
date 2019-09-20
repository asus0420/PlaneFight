using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_number2 :Planet
{
    private Vector3 dir = Vector3.left;
    private void Start()
    {
        zOverShot = 5.33f;
    }

    public override void Move()
    {
        base.Move();
        transform.Translate( dir * Time.deltaTime * moveSpeed );
        Vector3 pos = transform.position;
        if ( transform.position.x <=minX )
        {
            dir = -dir;
            pos.x = minX;
        }
        else if ( transform.position.x>=maxX )
        {
            dir = -dir;
            pos.x = maxX;
        }
        transform.position = pos;
    }   
}
