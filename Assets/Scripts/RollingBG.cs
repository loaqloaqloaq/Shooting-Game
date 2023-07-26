using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBG : MonoBehaviour
{
    public GameObject b;
    public float rollSpeed;

    const int n=6;
    GameObject[] bg = new GameObject[n];

    private float height = 3.2f;
   
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < n; i++)
        {
            float y = -6.4f + (height * i);
            bg[i] = Instantiate(b);
            bg[i].transform.parent = this.transform;
            bg[i].transform.position = new Vector3(-2.5f, y, 10);            
        }
        height = bg[0].GetComponent<Renderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0;i< n; i++) {
            bg[i].transform.Translate(0, -rollSpeed*Time.deltaTime, 0);
        }
        if (bg[2].transform.position.y <= -height) {
            for (int i = 0; i < n; ++i)
            {
                float y = -6.4f + (height * i);
                bg[i].transform.position = new Vector3(bg[i].transform.position.x,y, bg[i].transform.position.z);
            }
        }
    }
}
