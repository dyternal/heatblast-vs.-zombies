using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zombie : MonoBehaviour
{
    private Karakter karakter;
    private GameManager gameManager;


    public float speed;
    private bool faceR = true;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public float health = 100.0f;
    private bool kontroledebilir = true;
    public int damage = 0;
    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("Player").GetComponent<Karakter>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        if (kontroledebilir == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, karakter.transform.position, speed * Time.deltaTime);
            Vector2 direction = karakter.transform.position - transform.position;

            if (faceR && direction.x < 0)
            {
                Flip();
            }
            if (!faceR && direction.x > 0)
            {
                Flip();
            }
            anim.SetBool("zRunning", true);
            if (health <= 0.0f)
            {
                bc.isTrigger = true;
                gameManager.oldurmeSayisi++;
                gameManager.zombieText.text = gameManager.oldurmeSayisi.ToString();
                gameManager.exp += gameManager.killEXP;
                anim.SetTrigger("death");
                anim.SetBool("zAttack", false);
                anim.SetBool("zRunning", false);
                kontroledebilir = false;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                bc.isTrigger = true;
                StartCoroutine(Olum());
            }
        }
    }

    public void OyuncuyaSaldir(bool leave=false)
    {
        if (kontroledebilir)
        {
            if (leave == true)
            {
                anim.SetBool("zAttack", false);
            }
            else
            {
                anim.SetBool("zAttack", true);
                karakter.health -= 2;
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

    private IEnumerator Olum()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);



    }
}
