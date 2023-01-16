using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed;
    private float time = 5f;
    private float damage;
    public float distance;

    public GameObject DestroyObje;

    private Zombie zombie;
    private Kamera camscript;
    private GameManager manager;

    public AudioClip zombiedamagetakenAC;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyFireball", time);
        camscript = Camera.main.GetComponent<Kamera>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = manager.fireball_Speed;
        damage = manager.fireball_Damage;
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,distance,LayerMask.GetMask("Enemy"));

        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Zombie"))
            {
                zombie = hit.collider.GetComponent<Zombie>();
                zombie.GetComponent<AudioSource>().PlayOneShot(zombiedamagetakenAC);
                Instantiate(DestroyObje, transform.position, Quaternion.identity);
                Destroy(gameObject);
                zombie.health -= damage;
                
                

                camscript.CamShake();
            }

        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
