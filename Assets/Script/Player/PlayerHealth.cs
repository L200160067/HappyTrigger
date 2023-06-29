using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerController player;
    Animator anim;
    public bool isAlive = true;
    public int health = 100;
    public float fallingDMGThreshold = 20f; //Kecepatan minimal untuk player terkena damage jatuh

    private void Start()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
            isAlive = false;
        }
    }

    public void Die()
    {
        anim.SetTrigger("die");
        Debug.Log("Player is died!");
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    /* Fall Damage */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Memberikan damage ketika jatuh di kecepatan tertentu */
        float velocityY = Mathf.Abs(collision.relativeVelocity.y);
        int fallingDamage = Mathf.RoundToInt(velocityY - fallingDMGThreshold) / 2; //Perhitungan damage ketika jatuh
        if (collision.relativeVelocity.y > fallingDMGThreshold && isAlive)
        {
            TakeDamage(fallingDamage);
        }
    }

    public void TakeDamage(int damage, float knockback = 0)
    {
        health -= damage;
        anim.SetTrigger("hurt");
        player.rb2D.AddForce(new Vector2(knockback, 5f), ForceMode2D.Impulse);

    }
}
