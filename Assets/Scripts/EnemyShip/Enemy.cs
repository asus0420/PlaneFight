using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHp;
    public int inittalHp;
    public float minX;
    public float maxX;
    public GameObject explosition;
    public float moveSpeed;
    public float zOverShot;
    protected MeshRenderer mesh;
    protected Color currentColor;
    protected Color changeColor;
    public float score;
    public void Awake()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        currentColor = mesh.material.color;
    }
    void Start()
    {

    }
    void Update()
    {

    }

    public virtual void Move()
    {
        if ( transform.position.z <= -zOverShot )
        {
            Destroy( gameObject );
            mesh.material.color = currentColor;
        }
        transform.Translate( -Vector3.forward * Time.deltaTime * moveSpeed );
    }

    public void Die()
    {
        Instantiate( explosition , transform.position , Quaternion.identity );
        Destroy( gameObject );
        UICanvas.Instance.scoreTotal += score;
        mesh.material.color = currentColor;
    }

    public void GetHurt()
    {
        if ( enemyHp > 0 )
        {
            --enemyHp;
            if ( enemyHp <= 0 )
            {
                Die();
            }
            else
            {
                AttackFeedBack();
            }
        }
    }

    public void AttackFeedBack()
    {
        changeColor = new Color( Random.Range( 0.5f , 1f ) , Random.Range( 0f , 1f ) , Random.Range( 0f , 1f ) , 1 );
        mesh.material.color = changeColor;
    }

    public void RestoreColor()
    {
        mesh.material.color = Color.Lerp( mesh.material.color , currentColor , Time.deltaTime * 5f );
    }


}
