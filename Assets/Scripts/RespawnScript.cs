using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public float timeToRespawn;

    public GameObject enemyToRespawn;
    public bool isRespawning;

    // Update is called once per frame

    private void Start()
    {
        enemyToRespawn = transform.GetChild(0).gameObject;
    }


    void Update()
    {
        
    }

    public IEnumerator RespawnEnemy()
    {
        enemyToRespawn.SetActive(false);

        yield return new WaitForSeconds(timeToRespawn);
        enemyToRespawn.SetActive(true);

        enemyToRespawn.GetComponent<Enemy>().healthPoints =
            enemyToRespawn.GetComponent<EnemyHealth>().originalHealth;

        enemyToRespawn.GetComponent<SpriteRenderer>().material =
            enemyToRespawn.GetComponent<Blink> ().original;
        enemyToRespawn.GetComponent<EnemyHealth>().isDamaged = false;

        yield return RespawnAmin();
    }

    IEnumerator RespawnAmin()
    {
        isRespawning = true;
        enemyToRespawn.GetComponent<Animator>().SetBool("isRespawning", true);
        yield return new WaitForSeconds(0.4f);
        enemyToRespawn.GetComponent<Animator>().SetBool("isRespawning", false);
        isRespawning = false;
    }
}
