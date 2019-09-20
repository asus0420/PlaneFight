using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float delayTime;
    public float zOverShot = 5.33f;
    public float minX;
    public float maxX;
    private float scoreSpwan = 0;
    private int scoreBorder = 500;
    public void Awake()
    {
        
    }

    void Start()
    {
        InvokeRepeating( "CreateEnemy" , 5 , delayTime );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEnemy()
    {
         scoreSpwan =  UICanvas.Instance.scoreTotal;
         if ( scoreSpwan >= scoreBorder )
         {
             GameObject boss = Instantiate( bossPrefab , new Vector3( 0f , 0f , 8f ) , Quaternion.identity );
             boss.transform.SetParent( GameObject.FindGameObjectWithTag( "EnemyManager" ).transform );
             scoreBorder += 700;
         }
         else
         {
             GameObject go = Instantiate( enemyPrefab , RandomPosiotion() , Quaternion.identity );
             go.transform.SetParent( GameObject.FindGameObjectWithTag( "EnemyManager" ).transform );
         }
    }

    public Vector3 RandomPosiotion()
    {
        float x = Random.Range( minX , maxX );
        Vector3 pos = new Vector3( x , transform.position.y , transform.position.z );
        return pos;
    }
}
