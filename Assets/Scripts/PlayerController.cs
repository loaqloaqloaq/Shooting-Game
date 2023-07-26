using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Sprite left1,left2,right1,right2,normal;
    public GameObject shot;

    private float[] marginX, marginY;

    private float fireRate, coolDownCounter;

    private int[] shootingMode = { 1, 3, 5 };

    private bool dead;
    private float deadTimer;
    public Sprite[] bombSprite;
    AudioSource s;
    public AudioClip st, bm;

    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        marginX = new float[] { -8.4f, 3.4f };
        marginY = new float[] { -4.5f, 4.5f };
        
        fireRate = 0.1f;
        coolDownCounter = 0;

        dead = false;
        deadTimer = 0;

        s=this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            //à⁄ìÆèàóù
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);

            float tmpX = transform.position.x;
            float tmpY = transform.position.y;
            if (transform.position.y <= marginY[0]) tmpY = marginY[0];
            else if (transform.position.y >= marginY[1]) tmpY = marginY[1];

            if (transform.position.x <= marginX[0]) tmpX = marginX[0];
            else if (transform.position.x >= marginX[1]) tmpX = marginX[1];

            transform.position = new Vector3(tmpX, tmpY, transform.position.z);

            //éÀåÇèàóù
            if (Input.GetKeyDown("z") || Input.GetKeyDown("x") || Input.GetKeyDown("c")) coolDownCounter = 0;

            if (Input.GetKey("z"))
            {
                this.Shoot(shootingMode[0]);
            }

            else if (Input.GetKey("x"))
            {
                this.Shoot(shootingMode[1]);
            }


            else if (Input.GetKey("c"))
            {
                this.Shoot(shootingMode[2]);
            }

            else if (Input.GetKey("c"))
            {
                this.Shoot(shootingMode[2]);
            }

            //ìÆâÊèàóù
            if (horizontalInput > 0)
            {
                //right
                sr.sprite = right1;
                if (horizontalInput > 0.5f) sr.sprite = right2;
            }
            else if (horizontalInput < 0)
            {
                //left
                sr.sprite = left1;
                if (horizontalInput < -0.5f) sr.sprite = left2;
            }
            else
            {
                //normal
                sr.sprite = normal;
            }
            //DEBUG
            if (Input.GetKey("g"))
            {
                dead = true;
            }
        }
        //åÇÇΩÇÍÇΩèàóù
        else
        {
            deadTimer += Time.deltaTime;
            sr.sprite = bombSprite[(int)(deadTimer / 0.2f) % (bombSprite.Length - 1)];
            if (deadTimer >= 0.2f * (bombSprite.Length - 1))
            {
                Destroy(gameObject);
                GameObject.Find("GameDirector").GetComponent<GameDirector>().GameOver = true;
            }

        }
    }
    private void Shoot(int mode) {             
        if (coolDownCounter <= 0)
        {
            s.clip = st;
            s.Play();
            if (mode == 1)
            {
                genBullet(0);
            }
            else if (mode == 3)
            {
                genBullet(0);
                genBullet(7);
                genBullet(1);
            }
            else if (mode == 5) {
                genBullet(0);
                genBullet(7);
                genBullet(1);
                genBullet(3);
                genBullet(5);
            }
            coolDownCounter = fireRate;
        }
        else
        {
            coolDownCounter -= Time.deltaTime;
        }
    }
    private void genBullet(int dir) {
        Vector3[] offset = new Vector3[] {
            new Vector3(0, 0.32f, 0),
            new Vector3(0.32f, 0.32f, 0),
            new Vector3(0.32f, 0, 0),
            new Vector3(0.32f, -0.32f, 0),
            new Vector3(0, -0.32f, 0),
            new Vector3(-0.32f,-0.32f, 0),
            new Vector3(-0.32f, 0, 0),
            new Vector3(-0.32f, 0.32f, 0),
        };
        Vector3 pos = transform.position + offset[dir];
        GameObject s = Instantiate(shot, pos, Quaternion.identity);
        s.GetComponent<ShootController>().speed = 20;
        s.GetComponent<ShootController>().direction = dir;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            s.clip = bm;
            s.Play();
            this.dead = true;
        }       
    }
}



