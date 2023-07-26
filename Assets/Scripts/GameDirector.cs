using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{

    GlobalVariable gv;
    Text scoreText;
    public bool GameOver;

    float time;

    public GameObject score, image, GameOverText, GameOverScore, GameOverStart;

    bool addAlp;
    float speed;
    float alp;
    Image i;

    // Start is called before the first frame update
    void Start()
    {
        gv= GameObject.FindGameObjectWithTag("GlobalVariable").GetComponent<GlobalVariable>();
        gv.setScore(0);
        scoreText=GameObject.FindGameObjectWithTag("score").GetComponent<Text>();

        GameOver = false;

        i = GameOverStart.GetComponent<Image>();
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

        if (scoreText)
        scoreText.text=gv.getScore().ToString().PadLeft(5, '0');

        //ゲームオーバー処理
        if (GameOver) {
            score.SetActive(false);
            image.SetActive(false);            

            GameOverScore.GetComponent<Text>().text= gv.getScore().ToString().PadLeft(5, '0')+"点！";

            time += Time.deltaTime;
            if(time >1) GameOverText.SetActive(true);            
            if (time > 3) GameOverStart.SetActive(true);

            if (time > 2) {
                GameOverScore.SetActive(true);
                
                if (addAlp)
                {
                    alp += speed * Time.deltaTime;
                    if (alp >= 1.0f) addAlp = false;
                }
                else
                {
                    alp -= speed * Time.deltaTime;
                    if (alp <= 0) addAlp = true;
                }
                i.color = new Color(i.color.r, i.color.g, i.color.b, alp);

                if (Input.GetKeyDown(KeyCode.S))
                {
                    SceneManager.LoadScene("title");
                }
            }
        }
    }
}
