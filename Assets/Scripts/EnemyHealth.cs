using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;
    public GameObject deathEffect;
    public bool isDamaged;
    SpriteRenderer sprite;
    Blink material;
    Rigidbody2D rb;

    public float originalHealth;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        material = GetComponent<Blink>();
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon") && !isDamaged)
        {

            enemy.healthPoints -= 2f;
            AudioManager.instance.PlayAudio(AudioManager.instance.daZom);

            if (collision.transform.position.x< transform.position.x)
            {
                rb.AddForce(new Vector2(enemy.knockbackForceX, enemy.knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(-enemy.knockbackForceX, enemy.knockbackForceY), ForceMode2D.Force);
            }
            
            StartCoroutine(Damager());

            if(enemy.healthPoints< 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                AudioManager.instance.PlayAudio(AudioManager.instance.enemydead);

                ExperencieScript.instance.expModifier(GetComponent<Enemy>().experienceToGive);

                //respawn
                if(enemy.shouldRespawn)
                {
                    transform.GetComponentInParent<RespawnScript>().StartCoroutine
                        (GetComponentInParent<RespawnScript>().RespawnEnemy() );
                    
                }
                else
                {
                    Destroy(gameObject);
                }

                
            }
        }
    }

    IEnumerator Damager()
    {
        isDamaged = true;
       sprite.material = material.blink;
        yield return new WaitForSeconds(0.3f);
        isDamaged = false;
        sprite.material = material.original;
    }

}
