using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    //55 ，25   20
    LinkedList<Planet> planetLifePool;
    LinkedList<Planet> planetDeathPool;
    private static PlanetManager instance;
    public static PlanetManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        planetLifePool = new LinkedList<Planet>();
        planetDeathPool = new LinkedList<Planet>();
        instance = this;
    }
    /// <summary>
    /// 在死亡池找到行星
    /// </summary>
    /// <param name="type"> 行星种类</param>
    /// <returns></returns>
    public Planet FindPlanet(PlanetType type)
    {
        Planet planet = null;
        foreach ( Planet node in planetDeathPool )
        {
            if ( node.typeOfPlanet == type )
            {
                planet = node;
                planetDeathPool.Remove( planet );
                planet.gameObject.SetActive( true );
                break;
            }
        }
        return planet;
    }
    /// <summary>
    /// 添加到生存池中
    /// </summary>
    /// <param name="planet"> 行星</param>
    public void AddToLifePool( Planet planet )
    {
        planetLifePool.AddLast( planet );
    }
    /// <summary>
    /// 回收作废的行星
    /// </summary>
    /// <param name="planet"> 行星 </param>
    public void CollectionPlanet( Planet planet )
    {
        planetLifePool.Remove( planet );
        planetDeathPool.AddLast(planet);
        planet.gameObject.SetActive( false );  
    }

}
