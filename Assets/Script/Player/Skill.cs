using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    PlayerController player;
    Enemy enemy;
    [NonSerialized]
    public bool flipRight;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        flipRight = player.transform.localScale.x > 0 ? true : false;
        transform.localScale = new Vector2(flipRight ? transform.localScale.x > 0 ? transform.localScale.x : -transform.localScale.x
        : transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);

    }
    private void Update()
    {
        transform.Translate(flipRight ? Vector3.right : Vector3.left /** Time.deltaTime * player.skillSpeed*/);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            Destroy(gameObject);

        // damage the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(player.attackPower, player.knockbackPower);
            Destroy(gameObject);
            Debug.Log("Enemy get damage by skill");
        }
        Debug.Log("Trigger by" + other.gameObject.tag);
    }
}