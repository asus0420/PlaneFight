using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawn : MonoBehaviour
{
    //生成行星的几率
    static int  PLANET_1_ODDS = 55;
    static int  PLANET_2_ODDS = 80;
    static int  PLANET_3_ODDS = 100;

    public GameObject[] planets = new GameObject[4];
  
    void Start()
    {
        InvokeRepeating( "CreatePlanet" , 1f , 2f );
    }

   
    void Update()
    {
        
    }
    /// <summary>
    /// 产生行星
    /// </summary>
    void CreatePlanet()
    {
        Planet planet = null;
        int num = GetPlanetNum();
        planet = PlanetManager.Instance.FindPlanet( (PlanetType)num );
        if ( null == planet )
        {
            GameObject go =   Instantiate( planets[num] );
            go.transform.SetParent( transform );
            planet = go.GetComponent<Planet>();
        }
        planet.transform.position = transform.position;
        planet.RandomPosition();
        PlanetManager.Instance.AddToLifePool( planet );
    }
    /// <summary>
    /// 随机产生下标
    /// </summary>
    /// <returns></returns>
    int GetPlanetNum()
    {
        int num = Random.Range( 0 , 100 );
        if ( num<  PLANET_1_ODDS)//0-54
        {
            return 0;
        }
        else if ( num < PLANET_2_ODDS )//55-79
        {
            return 1;
        }
        else //80-99
        {
            return 2;
        }
    }
}
