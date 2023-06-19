using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    public int health = 100;
    public float fallingDMGThreshold = 20f; //Kecepatan minimal untuk player terkena damage jatuh

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            playerController.Die();
        }
    }

    /* Fall Damage */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Memberikan damage ketika jatuh di kecepatan tertentu */
        float velocityY = Mathf.Abs(collision.relativeVelocity.y);
        int fallingDamage = Mathf.RoundToInt(velocityY - fallingDMGThreshold) / 2; //Perhitungan damage ketika jatuh
        if (collision.relativeVelocity.y > fallingDMGThreshold)
        {
            TakeDamage(fallingDamage);
        }

        /* Memberikan damage ketika bertabrakan dengan enemy */
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
