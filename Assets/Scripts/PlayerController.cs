using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight;
    float velX, velY;
    Rigidbody2D rb;

    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public float maxExper;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }


        FlipCharacter();
        Attack();

    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }
    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.saltoHeroe);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;
        rb.velocity = new Vector2(velX * speed, velY);


        if(rb.velocity.x != 0)
        {
            anim.SetBool("Walk", true);
            
        }
        else
        {
            anim.SetBool("Walk", false);
            AudioManager.instance.PlayAudio(AudioManager.instance.run);
        }

        if(maxExper >= 10)
        {
            
        }
    }

    public void Attack()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Attack", true);
            AudioManager.instance.PlayAudio(AudioManager.instance.hit);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    public void FlipCharacter()
    {

        if (rb.velocity.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (rb.velocity.x < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
}
