using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;

    public GameObject fireball;
    public Transform firepoint;
    private GameManager manager;

    private float timeShotTimer;
    public float startTimeShotTimer;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        startTimeShotTimer = manager.fireball_Time;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ + offset);

        if(timeShotTimer <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(manager.twoBall == 7) Instantiate(fireball, new Vector3(firepoint.position.x, firepoint.position.y + 0.5f, firepoint.position.z), transform.rotation);
                Instantiate(fireball, firepoint.position, transform.rotation);
                timeShotTimer = startTimeShotTimer;
            }
        }
        else
        {
            timeShotTimer -= Time.deltaTime;
        }


    }
}
