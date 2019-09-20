using UnityEngine;

public class BackGround : MonoBehaviour
{
    private MeshRenderer mesh;
    public float moveSpeed = 0.1f;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //通过偏移量使得主材质移动
        mesh.material.SetTextureOffset( "_MainTex" , new Vector2( 0 , Time.time * moveSpeed ) );
    }
}
