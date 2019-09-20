using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawn : MonoBehaviour
{
    public GameObject[] propsPrefabs = new GameObject[2];
    void Start()
    {
        Invoke( "RandomInitProps",0.2f ); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateProps()
    {
        Props prop = null;
        int num = Random.Range(0,2);
        prop = PropManager.Instance.FindInDeathPool((PropType)num);
        if ( null == prop )
        {
            GameObject go = Instantiate( propsPrefabs[num]);
            go.transform.SetParent( GameObject.FindGameObjectWithTag( "PropManager" ).transform );
            prop = go.GetComponent<Props>();
        }
        prop.transform.position = transform.position;
        PropManager.Instance.AddToLifePool( prop );
    }

    public void RandomInitProps()
    {
        int flag = Random.Range( 0 , 10 );
        if ( flag < 5 )
        {
            CreateProps();
        }
    }
}
