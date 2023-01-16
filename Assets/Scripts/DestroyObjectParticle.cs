using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectParticle : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyParticleEnemy", 5f);
    }

    // Update is called once per frame
    void DestroyParticleEnemy()
    {
        Destroy(gameObject);
    }
}
