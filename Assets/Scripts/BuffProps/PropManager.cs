using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    private LinkedList<Props> lifePropPool;
    private LinkedList<Props> deathPropPool;

    //单例
    private static PropManager instance;
    public static PropManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        lifePropPool = new LinkedList<Props>();
        deathPropPool = new LinkedList<Props>();
        instance = this;
    }

    public Props FindInDeathPool(PropType type)
    {
        Props prop = null;
       
        foreach ( Props item in deathPropPool )
        {
            if ( item.typeOfProp == type )
            {
                prop = item;
                deathPropPool.Remove( prop );
                prop.gameObject.SetActive( true );
                break;
            }
        }
        return prop;
    }

    public void AddToLifePool( Props prop )
    {
        lifePropPool.AddLast( prop );
    }
    public void CollectionDestroy(Props prop)
    {
        lifePropPool.Remove( prop );
        deathPropPool.AddLast( prop );
        prop.gameObject.SetActive( false );
    }
}
