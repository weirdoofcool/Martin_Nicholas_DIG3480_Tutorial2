using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text WinText;

    public Text LiveText;

    private int scoreValue = 0;

    private int LiveTextValue = 3;

    public Text LoseText;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    Animator anim;

    private bool facingRight = true;

    private bool isOnGround;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        WinText.text = "";
        LiveText.text = LiveTextValue.ToString();
        LoseText.text = "";
        anim = GetComponent<Animator>();
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.A))
        { anim.SetInteger("State", 1); }

        if(Input.GetKeyUp(KeyCode.A))
        { anim.SetInteger("State", 0); }

        if(Input.GetKeyDown(KeyCode.D))
        { anim.SetInteger("State", 1); }

        if(Input.GetKeyUp(KeyCode.A))
        { anim.SetInteger("State", 0); }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue >= 8)
            {
                WinText.text = "You win! Game created by Nicholas Martin";
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.loop = false;
                musicSource.Play();
                    }
            
            if (scoreValue == 4)
            {
                transform.position = new Vector2(31.18f, -2.39f);
                LiveTextValue = 3;
            }
        }



        if (collision.collider.tag == "Enemy")
        {
            LiveTextValue -= 1;
            LiveText.text = LiveTextValue.ToString();
            Destroy(collision.collider.gameObject);
            if (LiveTextValue <= 0)
            {
                LoseText.text = "You Lose";
                Destroy(this);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
            }

        }
        {
            if (collision.collider.tag == "Ground" && isOnGround)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

}