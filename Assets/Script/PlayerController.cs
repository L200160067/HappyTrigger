using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField]
    private bool isJumping, isGrounded;
    [SerializeField]
    private float jumpForce, moveSpeed;
    [SerializeField]
    private int score, health;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        /*Change the gravity scale*/
        rb2D.gravityScale = 3f;

        jumpForce = 15f;
        moveSpeed = 5f;
        score = 0;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, rb2D.velocity.y);

        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector2(2, 2);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector2(-2, 2);
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            // isJumping = true;
            // isGrounded = false;
        }

        isJumping = rb2D.velocity.y > 0 && !isGrounded;
        // if (rb2D.velocity.y > 0)
        // {
        //     isJumping = true;
        // }
        // else
        // {
        //     isJumping = false;
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.CompareTag("Ground"))
        // {
        //     isJumping = false;
        //     isGrounded = true;
        // }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }

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
        isGrounded = !other.gameObject.CompareTag("Ground");
    }



    private void CollectItem(GameObject item)
    {
        IncreaseScore(1);

        Destroy(item);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
    }

    public void IncreaseScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }


}
