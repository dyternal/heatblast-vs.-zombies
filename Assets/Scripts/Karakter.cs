using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Karakter : MonoBehaviour
{
    public float speed;

    public bool isDead;
    private bool faceR = true;
    public GameObject fireball;
    public Transform firepoint;

    public float maxHealth;
    public float health;

    private Vector2 moveVelocity;

    public Texture2D cursorTexture;

    private Animator anim;
    private Rigidbody2D rb2d;
    private GameManager gameManager;

    private Coroutine OyuncuyaSaldir = null;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        maxHealth = 100f;
        health = maxHealth;

    }
    void Update()
    {
        float moveInput1 = Input.GetAxisRaw("Horizontal");
        float moveInput2 = Input.GetAxisRaw("Vertical");
        Vector2 moveInput3 = new Vector2(moveInput1, moveInput2);
        moveVelocity = moveInput3 * speed;
        if (moveInput1 == 0 && moveInput2 == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (faceR && moveInput1 < 0)
        {
            Flip();
        }
        if (!faceR && moveInput1 > 0)
        {
            Flip();
        }

    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.deltaTime);

        float xPosition = Mathf.Clamp(transform.position.x, -8.023f, Mathf.Abs(-8.023f));
        float yPosition = Mathf.Clamp(transform.position.y, -4.181f, Mathf.Abs(-4.181f));
        transform.position = new Vector2(xPosition, yPosition);

        if(health <= 0.0f)
        {
            isDead = true;
            health = 0.0f;;
            Variables.coin = gameManager.alinanCoinSayisi;
            Variables.kill = gameManager.oldurmeSayisi;
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            gameManager.alinanCoinSayisi++;
            gameManager.coinText.text = gameManager.alinanCoinSayisi.ToString();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("AttackCircle"))
        {
            if (!isDead && OyuncuyaSaldir == null)
            {
                Zombie zombie_SC = collision.gameObject.GetComponentInParent<Zombie>();
                zombie_SC.OyuncuyaSaldir();
            
                OyuncuyaSaldir = StartCoroutine(OyuncuyaSaldirC(zombie_SC));
            }
        }
    }

    private IEnumerator OyuncuyaSaldirC(Zombie zombie_SC)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.43f);
            
            zombie_SC.OyuncuyaSaldir();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackCircle"))
        {
            Zombie zombie_SC = collision.gameObject.GetComponentInParent<Zombie>();
            zombie_SC.OyuncuyaSaldir(true);
            if (OyuncuyaSaldir != null)
            {
                StopCoroutine(OyuncuyaSaldir);
                OyuncuyaSaldir = null;
            }
        }
    }
    private void Flip()
    {
        faceR = !faceR;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
