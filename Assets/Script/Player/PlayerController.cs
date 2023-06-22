using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator anim;
    PlayerHealth playerHealth;
    [SerializeField]
    private bool isJumping, isAttacking, isGrounded;
    [SerializeField]
    private float jumpForce = 20f, moveSpeed = 5f;
    [SerializeField]
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        rb2D = GetComponent<Rigidbody2D>();
        /*Change the gravity scale*/
        rb2D.gravityScale = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isAlive)
        {
            Run();
            Jump();
            Attack();
        }
        if (transform.position.y <= -35f)
        {
            playerHealth.health = 0;
        }
    }

    void Run()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, rb2D.velocity.y);

        anim.SetBool("isRun", moveHorizontal != 0 && !isJumping && isGrounded ? true : false);
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector2(2, 2);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector2(-2, 2);
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        isJumping = rb2D.velocity.y > 0 && !isGrounded; // return boolean untuk isJumping
        anim.SetBool("isJump", isJumping);

    }

    void Attack()
    {
        isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && isGrounded)
        {
            anim.SetTrigger("attack");
        }

        rb2D.constraints = isAttacking ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation; // prevent player can move while attacking

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Item"))
        {
            CollectItem(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // if (collision.gameObject.CompareTag("Ground"))
        // {
        //     isGrounded = false;
        // }
    }

    /*Ground Check*/
    private void OnTriggerStay2D(Collider2D other)
    {
        isGrounded = other.gameObject.CompareTag("Ground");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") == true)
            isGrounded = false;
    }



    private void CollectItem(GameObject item)
    {
        IncreaseScore(1);

        Destroy(item);
    }


    public void IncreaseScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }


}
