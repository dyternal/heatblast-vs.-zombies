using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameObject.Find("yourkills").GetComponent<TMP_Text>().text = "YOUR KILL SCORE: " + Variables.kill.ToString();
            GameObject.Find("yourcoins").GetComponent<TMP_Text>().text = "YOUR COINS: " + Variables.coin.ToString();
            GameObject.Find("YOUAREDEAD").GetComponent<Animator>().SetTrigger("fade");

            return;
        }
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}
