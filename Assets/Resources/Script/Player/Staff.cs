using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] AudioSource bonkSfx;
    PlayerStats player;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // damage the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            bonkSfx.Play();
            other.gameObject.GetComponent<Enemy>().TakeDamage(player.attackPower, player.knockbackPower);
            Debug.Log("Enemy get damage");
        }
    }
}
