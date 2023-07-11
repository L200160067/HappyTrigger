using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    LoadScene loadScene;


    private void Start()
    {
        loadScene = FindObjectOfType<LoadScene>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            loadScene.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
