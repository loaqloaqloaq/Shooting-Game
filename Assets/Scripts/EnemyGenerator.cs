using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject e;

    float time;

    [System.Serializable]
    public class EnemyData
    {
        public string type;
        public float timimng;
        public float x;
        public float y;
        public bool spawned;
    }
    
    public List<EnemyData> enemyData = new List<EnemyData>();

    public int stage = 1;

    GameDirector gameDirector;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        var textAsset = Resources.Load("Stage0"+stage.ToString()) as TextAsset;
        string[] text = textAsset.text.Split("\n");       

        foreach (string s in text)
        {
            string[] fileData = s.Split(" ");
            EnemyData tmp = new EnemyData();
            if (fileData.Length == 3)
            {
                tmp.type = fileData[0];
                tmp.timimng = float.Parse(fileData[1]);
                tmp.x = float.Parse(fileData[2]) - 8;
                tmp.y = float.Parse(fileData[3]) + 5.57f;
                tmp.spawned = false;
                enemyData.Add(tmp);
            }            
        }
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameDirector.GameOver) { 
            this.time += Time.deltaTime;

            bool allSpawned = true;
            foreach (var ed in enemyData)
            {
                allSpawned = allSpawned && ed.spawned;
                if (this.time >= ed.timimng && !ed.spawned) {
                    GameObject enemy = Instantiate(e);
                    enemy.transform.position = new Vector2(ed.x, ed.y);
                    enemy.GetComponent<EnemyController>().type = ed.type;
                    ed.spawned = true;
                }
            }
            if (allSpawned) {
                foreach (var ed in enemyData)
                {
                    ed.spawned = false;
                }
                this.time = -1.0f;
            }
        }
    }
}
