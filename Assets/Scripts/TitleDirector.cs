using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleDirector : MonoBehaviour
{
    public GameObject title ,start;

    bool addAlp;
    float speed;
    float alp;
    Image i;
    // Start is called before the first frame update
    void Start()
    {
        i = start.GetComponent<Image>();
        addAlp = false;
        speed = 2.0f;
        alp = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("main");
        }

        if (addAlp)
        {
            alp += speed * Time.deltaTime;
            if(alp>=1.0f) addAlp = false;
        }
        else {
            alp -= speed * Time.deltaTime;
            if (alp <= 0) addAlp = true;
        }       
        i.color = new Color(i.color.r, i.color.g, i.color.b, alp);

        
    }
}
