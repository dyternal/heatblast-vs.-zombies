using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int oldurmeSayisi;
    public int alinanCoinSayisi;
    public int exp;
    public int maxExp;
    private int level;
    public int killEXP;
    public float coinOlusturmaSure;
    public int maxCoin;

    [SerializeField]
    private AudioClip levelUpSound;
    private AudioSource audio_Source;

    private EnemyManager enemyManager;
    private Karakter karakter;
    [SerializeField]
    private GameObject coinObje;
    [SerializeField]
    private Transform coinPos;

    public TMP_Text zombieText;
    public TMP_Text coinText;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private Slider explevelSlider;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private GameObject skillPanel;
    [SerializeField]
    private TMP_Text skillText;

    private Coroutine CoinOlusturCR = null;

    [SerializeField]
    private Slider maxhp_Slider;
    [SerializeField]
    private Slider fireball_speed_Slider;
    [SerializeField]
    private Slider fireball_reload_Slider;
    [SerializeField]
    private Slider movement_speed_Slider;
    [SerializeField]
    private Slider two_fireballs_Slider;
    [SerializeField]
    private Slider fireball_damage_Slider;

    public float fireball_Speed;
    public float fireball_Time;
    private float movement_Speed;
    public int twoBall;
    public float fireball_Damage;

    public int yetenekPuani;



    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("Player").GetComponent<Karakter>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        audio_Source = GetComponent<AudioSource>();

        fireball_Speed = 4;
        fireball_Time = 1;
        movement_Speed = 3;
        fireball_Damage = 30;

        coinText.text = alinanCoinSayisi.ToString();
        zombieText.text = oldurmeSayisi.ToString();
        exp = 0;
        level = 1;
        maxExp = level * 240;
        explevelSlider.maxValue = maxExp;
        explevelSlider.value = exp;
        levelText.text = "LEVEL: " + level.ToString() + " (" + exp.ToString() + "/" + maxExp.ToString() + ")";

        
    }

    // Update is called once per frame
    void Update()
    {
        skillText.text = "Skill Points: " + yetenekPuani.ToString();
        healthSlider.maxValue = karakter.maxHealth;
        if (yetenekPuani > 0 && skillPanel.activeInHierarchy == false)
        {
            skillPanel.SetActive(true);
            skillPanel.GetComponent<Animator>().SetTrigger("panelOpen");
        }
        else if(yetenekPuani <= 0)
        {
            yetenekPuani = 0;
            skillPanel.SetActive(false);
        }
        if (skillPanel.activeInHierarchy == true)
        {
            var input = Input.inputString;
            switch(input)
            {
                case "1":
                {
                    if (karakter.maxHealth < 200)
                    {
                        karakter.maxHealth += 20;
                        karakter.health += 20;
                        maxhp_Slider.value++;
                        
                        yetenekPuani--;
                    }
                    else skillPanel.GetComponent<Animator>().SetTrigger("panelShake");
                    break;
                }
                case "2":
                {
                    if(fireball_Speed < 8)
                    {
                        fireball_Speed++;
                        fireball_speed_Slider.value++;
                        yetenekPuani--;
                    }
                    else skillPanel.GetComponent<Animator>().SetTrigger("panelShake");
                    break;
                }
                case "3":
                {
                    if (fireball_Time > 0.6)
                    {
                        fireball_Time -= 0.1f;
                        fireball_reload_Slider.value++;
                        yetenekPuani--;
                    }
                    else skillPanel.GetComponent<Animator>().SetTrigger("panelShake");
                    break;
                }
                case "4":
                {
                    if (movement_Speed < 4.5)
                    {
                        movement_Speed += 0.5f;
                        karakter.speed = movement_Speed;
                        movement_speed_Slider.value++;
                        yetenekPuani--;
                    }
                    else skillPanel.GetComponent<Animator>().SetTrigger("panelShake");
                    break;
                }
                case "5":
                {
                    if (twoBall < 7)
                    {
                        twoBall++;
                        two_fireballs_Slider.value++;
                        yetenekPuani--;
                    }
                    else skillPanel.GetComponent<Animator>().SetTrigger("panelShake");
                    break;
                }
                case "6":
                {
                    if (fireball_Damage < 50)
                    {
                        fireball_Damage += 5;
                        fireball_damage_Slider.value++;
                        yetenekPuani--;
                    }
                    else skillPanel.GetComponent<Animator>().SetTrigger("panelShake");
                    break;
                }
            }
        }
        if(exp >= maxExp)
        {
            audio_Source.PlayOneShot(levelUpSound);
            exp = 0;
            level++;
            maxExp = level * 240;
            yetenekPuani++;

            if (enemyManager.time > 2)
            {
                if (level % 2 == 0)
                {
                    enemyManager.time -= 0.5f;

                }
            }
        }
        explevelSlider.maxValue = maxExp;
        explevelSlider.value = exp;
        levelText.text = "LEVEL: " + level.ToString() + " (" + exp.ToString() + "/" + maxExp.ToString() + ")";

        if (CoinOlusturCR == null)
        {
            CoinOlusturCR = StartCoroutine(CoinOlustur(coinOlusturmaSure));
        }
        healthSlider.value = karakter.health;
        healthText.text = karakter.health.ToString() + "/" + karakter.maxHealth.ToString();
    }

    private IEnumerator CoinOlustur(float coinOlusturmaSure)
    {
        if (GameObject.FindGameObjectsWithTag("Coin").Length < maxCoin)
        {
            yield return new WaitForSeconds(coinOlusturmaSure);
            float sansX = Random.Range(-8.023f, 8.023f);
            float sansY = Random.Range(-4.181f, 4.181f);
            coinPos.position = new Vector2(sansX, sansY);
            GameObject olusan = Instantiate(coinObje, coinPos.position, Quaternion.identity);
            CoinOlusturCR = null;
            StartCoroutine(Sil(olusan));
        }
    }

    private IEnumerator Sil(GameObject olusan)
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(olusan);
    }
}
