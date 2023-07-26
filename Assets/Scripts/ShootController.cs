using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    // Start is called before the first frame update
    //0F 1FR 2R 3BR 4B 5BL 6L 7FL
    public int direction = 0;
    public float speed;
    private float[] marginX, marginY;
    void Start()
    {
        marginX = new float[] { -8.9f, 3.9f };
        marginY = new float[] { -4.9f, 4.9f };
    }

    // Update is called once per frame
    void Update()
    {
        Vector2[] dirVec = new Vector2[] {
            //0 front
            new Vector2( 0,1),
            //1 front right
            new Vector2( 1,1),
            //2 right
            new Vector2( 1,0),
            //3 back right
            new Vector2( 1,-1),
            //4 back
            new Vector2( 0,-1),
            //5 back left
            new Vector2( -1,-1),
            //6 left
            new Vector2( -1,0),
            //7 front left
            new Vector2( -1,1),
        };

        transform.Translate(dirVec[direction] * speed * Time.deltaTime);

        if (transform.position.y > marginY[1] || transform.position.y < marginY[0] || transform.position.x > marginX[1] || transform.position.x < marginX[0]) {
            Destroy(gameObject);
        }
    }
}
