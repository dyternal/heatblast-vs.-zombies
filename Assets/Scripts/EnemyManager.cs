using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Karakter karakter;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject fenemyPrefab;
    public Transform[] spawnNoktalari;
    [SerializeField]
    private int maxEnemy;
    public float time;
    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("Player").GetComponent<Karakter>();
        StartCoroutine(EnemySpawner(time));
    }

    private IEnumerator EnemySpawner(float time)
    {
        while (!karakter.isDead)
        {
            yield return new WaitForSeconds(time);

            int sans = Random.Range(0, 14);
            int sans2 = Random.Range(0, 2);
            GameObject secilenPrefab;
            if (sans2 == 0) secilenPrefab = enemyPrefab;
            else secilenPrefab = fenemyPrefab;
            
            Instantiate(secilenPrefab, spawnNoktalari[sans]);
        }
    }
}
