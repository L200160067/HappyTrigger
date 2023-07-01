using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [NonSerialized]
    public Rigidbody2D rb2D;
    public GameObject container;
    Animator anim;
    PlayerHealth playerHealth;
    public bool isJumping, isAttacking, isGrounded;
    [NonSerialized] float moveHorizontal;
    public float jumpForce = 20f, moveSpeed = 5f, skillSpeed = 5f, knockbackPower = 5f;
    public int attackPower = 10, defense, score;

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
        moveHorizontal = isAttacking ? 0 : Input.GetAxis("Horizontal"); // prevent player to flip while attacking
        rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, rb2D.velocity.y);

        anim.SetBool("isRun", moveHorizontal != 0 && !isJumping && isGrounded ? true : false);
        if (moveHorizontal > 0) // flip right
        {
            transform.localScale = new Vector2(transform.localScale.x > 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);
            knockbackPower = knockbackPower < 0 ? -knockbackPower : knockbackPower;
        }
        else if (moveHorizontal < 0) // flip left
        {
            transform.localScale = new Vector2(transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);
            knockbackPower = knockbackPower > 0 ? -knockbackPower : knockbackPower;
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
        isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill");
        if (!isAttacking && isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack"); // normal attack
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetTrigger("skill"); // launch skill
            }
        }

        rb2D.constraints = isAttacking ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation; // prevent player can move while attacking
    }
    void Skill(GameObject magicSkill) // method for Animation Event
    {
        if (!magicSkill.GetComponent<Skill>())
        {
            magicSkill.AddComponent<Skill>();
        }
        if (transform.localScale.x > 0)
        {
            Instantiate(magicSkill, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), Quaternion.identity, container != null ? container.transform : null);
        }
        else if (transform.localScale.x < 0)
        {
            Instantiate(magicSkill, new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z), Quaternion.identity, container != null ? container.transform : null);
        }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // damage the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(attackPower, knockbackPower);
            Debug.Log("Enemy get damage");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
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
