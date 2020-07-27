using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;


public class CharacterControl2D : MonoBehaviour
{
    bool grounded;
    public Collider2D groundCheck;
    public LayerMask groundLayer;
    public float speed;
    public float maxSpeed;
    public float currentSpeed;
    public float smooth;
    public float jumpForce;
    Rigidbody2D rb2D;
    private Animator animator;
    private float x;

    public int extraJumps;
    public int maxExtraJumps;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = maxExtraJumps;

        maxSpeed = speed * 2;
        currentSpeed = speed;

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
         x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = maxSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed =speed;
        }

        Vector2 targetVelocity = new Vector2(x * currentSpeed, rb2D.velocity.y);
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

        if (grounded)
        {
            extraJumps = maxExtraJumps;
        }

        animator.SetBool("Jumping", !grounded);
        animator.SetBool("Running", x != 0);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps >0)
        {
            rb2D.AddForce(new Vector2(0f, jumpForce));
            extraJumps--;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "star")
        {
            Destroy(collision.gameObject);
            gameObject.tag = "PowerUp";
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            StartCoroutine("reset");
        }
    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(5f);
        gameObject.tag = "Player";
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }






























}