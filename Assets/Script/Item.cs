using UnityEngine;

public class Item : MonoBehaviour
{
    public int scoreValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.IncreaseScore(scoreValue);
        }
        Destroy(gameObject);
    }
}
