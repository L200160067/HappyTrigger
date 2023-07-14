using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    PlayerController playerController;
    PlayerStats playerStats;
    Enemy enemy;
    Vector2 initialPosition;
    [NonSerialized]
    public bool flipRight;
    public float speed = 40f;
    bool destroyed;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerStats = FindObjectOfType<PlayerStats>();
        initialPosition = transform.position;
        flipRight = playerController.transform.localScale.x > 0 ? true : false;
        transform.localScale = new Vector2(flipRight ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);

    }
    private void Update()
    {
        transform.Translate((flipRight ? Vector3.right : Vector3.left) * Time.deltaTime * speed);
        if (Vector2.Distance(initialPosition, transform.position) >= 30) //set skill range
        {
            destroyed = true;
        }

        if (destroyed)
        {
            StartCoroutine(Destroyed());
        }
    }

    IEnumerator Destroyed()
    {
        yield return null /*new WaitUntil(() => destroy animation == false)*/;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!destroyed)
        {
            if (other.gameObject.CompareTag("Ground"))
                destroyed = true;

            // damage the enemy
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(playerStats.attackPower * 2, playerStats.knockbackPower);
                destroyed = true;
                Debug.Log("Enemy get damage by skill");
            }
        }
        Debug.Log("Trigger by" + other.gameObject.tag);
    }
}