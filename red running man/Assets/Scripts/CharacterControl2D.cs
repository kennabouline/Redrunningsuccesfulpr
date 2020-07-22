using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterControl2D : MonoBehaviour
{
    bool grounded;
    public Collider2D groundCheck;
    public LayerMask groundLayer;
    public float speed;
    public float smooth;
    public float jumpForce;
    Rigidbody2D rb2D;
    private Animator animator;
    private float x;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
         x = Input.GetAxisRaw("Horizontal");
        Vector2 targetVelocity = new Vector2(x * speed, rb2D.velocity.y);
        rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, targetVelocity,
            ref targetVelocity, Time.deltaTime * smooth);

        
        if (x > 0 && !facingRight)
        {
           
            Flip();
        }
        
        else if (x < 0 && facingRight)
        {
         
            Flip();
        }

    }
    bool facingRight = true;
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale; 
        scale.x*= -1;
        transform.localScale = scale;

    }

    void Update()
    {
       
        grounded = groundCheck.IsTouchingLayers(groundLayer);

        animator.SetBool("Jumping", !grounded);
        animator.SetBool("Running", x != 0);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rb2D.AddForce(new Vector2(0f, jumpForce));
    }



    
    
       
       
    



























}