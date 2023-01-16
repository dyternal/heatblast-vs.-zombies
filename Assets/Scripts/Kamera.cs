using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Animator anim;

    public void CamShake()
    {
        anim.SetTrigger("shake");
    }
}
