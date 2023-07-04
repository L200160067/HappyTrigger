using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    PlayerController player;
    Enemy enemy;
    Vector2 initialPosition;
    [NonSerialized]
    public bool flipRight;
    bool destroyed;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        initialPosition = transform.position;
        flipRight = player.transform.localScale.x > 0 ? true : false;
        transform.localScale = new Vector2(flipRight ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);

    }
    private void Update()
    {
        transform.Translate(flipRight ? Vector3.right : Vector3.left * Time.deltaTime * 30);
        if (Vector2.Distance(initialPosition, transform.position) >= 50) //set skill range
        {
            destroyed = true;
        }

        if (destroyed)
        {
            StartCoroutine(Destroyed());
            Debug.Log("Skill Destroyed");
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
                other.gameObject.GetComponent<Enemy>().TakeDamage(player.attackPower, player.knockbackPower);
                destroyed = true;
                Debug.Log("Enemy get damage by skill");
            }
        }
        Debug.Log("Trigger by" + other.gameObject.tag);
    }
}