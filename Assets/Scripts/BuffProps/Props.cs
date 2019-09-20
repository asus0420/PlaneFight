using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PropType
{
    HpProp,
    MpProp,
}
public class Props : MonoBehaviour
{
    public PropType typeOfProp = PropType.HpProp;
    public float moveSpeed;
    public float zOverShot =5.33f;

    public void Awake()
    {

    }

    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        if ( transform.position.z <= -zOverShot )
        {
            PropManager.Instance.CollectionDestroy( this );
        }
        transform.Translate( -Vector3.forward * Time.deltaTime * moveSpeed );
    }
}
