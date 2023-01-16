using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject vC;
    public void DisableButton(GameObject button)
    {
        button.gameObject.SetActive(false);

        StartCoroutine(VideoBaslat());
        
        
    }

    IEnumerator VideoBaslat()
    {
        yield return new WaitForSeconds(1f);
        vC.SetActive(true);
        StartCoroutine(OyunBaslat());
    }

    IEnumerator OyunBaslat()
    {
        yield return new WaitForSeconds(12f);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}
