using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public Sprite[] bombSprite;
    public Sprite left1, left2, right1, right2, normal;
    public Vector2 move;
    public string type;

    bool dead;
    float deadTimer;
    SpriteRenderer sr;
    private float[] marginX, marginY;
    GlobalVariable gv;

    GameDirector gameDirector;

    AudioSource s;    

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        deadTimer = 0;
        sr = gameObject.GetComponent<SpriteRenderer>();
        gv = GameObject.FindGameObjectsWithTag("GlobalVariable")[0].GetComponent<GlobalVariable>();       
        move = new Vector2(5, -1);

        marginX = new float[] { -8.4f, 3.4f };
        marginY = new float[] { -6.5f, 4.5f };

        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();

        s = gameDirector.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameDirector.GameOver)
        {
            if (!dead)
            {
                if (type == "A")
                {
                    move = new Vector2(0, -5);
                }
                else if (type == "B")
                {
                    if (transform.position.x <= marginX[0]) move = new Vector2(4, -1.5f);
                    else if (transform.position.x >= marginX[1]) move = new Vector2(-4, -1.5f);
                }
                //à⁄ìÆèàóù            
                transform.Translate(move * Time.deltaTime);

                if (transform.position.y < marginY[0]) Destroy(gameObject);

                //ìÆâÊèàóù
                if (move.x > 0)
                {
                    sr.sprite = right1;
                    if (move.x > 2.5) sr.sprite = right2;
                }
                else if (move.x < 0)
                {
                    sr.sprite = left1;
                    if (move.x < -2.5) sr.sprite = left2;
                }
                else
                {
                    sr.sprite = normal;
                }
            }
            else
            {
                deadTimer += Time.deltaTime;
                sr.sprite = bombSprite[(int)(deadTimer / 0.2f) % (bombSprite.Length - 1)];
                if (deadTimer >= 0.2f * (bombSprite.Length - 1)) Destroy(gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.CompareTag("Shot") && !this.dead)
        {            
            s.Play();
            this.dead = true;         
            Destroy(other.gameObject);
            gv.plusScore(1);            
            this.GetComponent<CapsuleCollider2D>().enabled = false;
        }  
        else if (other.gameObject.CompareTag("Player") && !this.dead)
        {
            s.Play();
            this.dead = true;
            this.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }   
}
