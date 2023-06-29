using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 20, damage = 1, knockbackPower, speed = 4;
    public float attackCD = 2f;
    [NonSerialized]
    public bool playerDetected, isAttacking;
    bool isCD = false;
    float Cooldown = 0;
    Transform target;
    Animator anim;


    private void Start()
    {
        Cooldown = attackCD;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        Move();
    }
    void Attack()
    {

        isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("attack");
        isCD = attackCD > 0;
        if (!isCD)
        {
            attackCD = Cooldown;
            anim.SetTrigger("attack");
        }
        if (!isAttacking)
        {
            if (attackCD >= 0)
            {
                setCooldown();
            }
        }
        void setCooldown()
        {
            attackCD = attackCD - Time.deltaTime;
        }
    }

    void Move()
    {
        float playerDistanceX = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(target.position.x, 0));
        float playerDistanceY = Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, target.position.y));
        if (FindObjectOfType<PlayerHealth>().isAlive)
        {
            if (playerDetected && playerDistanceX > 3)
            {
                anim.SetBool("isMoving", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
                if (target.position.x > transform.position.x)
                {
                    transform.localScale = new Vector2(transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);
                }
                else if (target.position.x < transform.position.x)
                {
                    transform.localScale = new Vector2(transform.localScale.x > 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);
                }
            }
            else if (playerDistanceX < 3.5f && playerDistanceY < 3f)
            {
                Attack();
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            if (Vector2.Distance(transform.position, target.position) > 100 && playerDetected)
            {
                playerDetected = false;
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage, float knockback = 0)
    {
        health -= damage;
        // anim.SetTrigger("hurt");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback, 4f), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerHealth>().isAlive)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, knockbackPower);
        }
    }

}
