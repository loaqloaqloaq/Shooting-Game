using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    private static bool created = false;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;            
        }
    }

    public static int score;

    public void setScore(int s) {
        score = s;
    }
    public void plusScore(int s)
    {
        score += s;
    }
    public int getScore() {
        return score;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
