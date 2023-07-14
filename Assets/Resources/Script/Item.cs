using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isScoreItem = true;
    public int mana = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            if (isScoreItem)
            {
                FindFirstObjectByType<UI>().IncreaseScore(gameObject.tag);
            }
            else
            {
                playerStats.mana += mana;
            }
        }
        Destroy(gameObject);
    }
}
