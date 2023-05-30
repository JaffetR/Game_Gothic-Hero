using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthImg;
    bool isInmune;
    public float inmunityTime;
    Blink material;
    SpriteRenderer sprite;
    public float knockbackForceX;
    public float knockbackForceY;
    Rigidbody2D rb;
    public bool isDead;

    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject gameOverImg;

    // Start is called before the first frame update
    void Start()
    {
        gameOverImg.SetActive(false);
        rb=GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        health = maxHealth;
        if (!isDead)
        {
            gameOverImg.GetComponent<CanvasGroup>().alpha = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsDead();
        healthImg.fillAmount = health / maxHealth;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            health -= collision.GetComponent<Enemy>().damageToGive;
            StartCoroutine(Inmunity());
            AudioManager.instance.PlayAudio(AudioManager.instance.daHeroe);

            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }

            if(health <= 0)
            {
                //Game Over
                isDead = true;
                AudioManager.instance.PlayAudio(AudioManager.instance.gameOver);

            }
        }
    }

    public void IsDead()
    {
        if (isDead)
        {
            Time.timeScale = 0;
            gameOverImg.SetActive(true);
            AudioManager.instance.backgroundMusic.Stop();

            if(gameOverImg.GetComponent<CanvasGroup>().alpha < 1f)
            {
                gameOverImg.GetComponent<CanvasGroup>().alpha += 0.005f;
            }
        }
    }

    IEnumerator Inmunity()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        sprite.material = material.original;
        isInmune = false;
    }
}
