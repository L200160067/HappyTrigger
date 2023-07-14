using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    PlayerController player;
    Animator anim;
    Image healthBar, manaBar;
    public bool isAlive = true;
    public int health, mana;
    public int maxHealth = 100, maxMana = 100, attackPower = 10, defense;
    public float fallingDMGThreshold = 20f, //Kecepatan minimal untuk player terkena damage jatuh
                knockbackPower = 5f, skillCD = 5f;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        mana = maxMana;
        healthBar = FindObjectOfType<UI>().healthBar;
        manaBar = FindObjectOfType<UI>().manaBar;
    }

    private void Update()
    {
        healthBar.fillAmount = (float)health / (float)maxHealth;
        manaBar.fillAmount = (float)mana / (float)maxMana;

        if (health > maxHealth)
            health = maxHealth;
        if (mana > maxMana)
            mana = maxMana;

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
            // TakeDamage(fallingDamage);
        }
    }

    public void TakeDamage(int damage, float knockback = 0)
    {
        health -= damage;
        anim.SetTrigger("hurt");
        player.rb2D.AddForce(new Vector2(knockback, 5f), ForceMode2D.Impulse);
    }

}
