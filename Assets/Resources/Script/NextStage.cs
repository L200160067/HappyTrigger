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
            if (FindObjectOfType<UI>().taskComplete)
            {
                loadScene.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("Task doesn't complete");
            }
        }
    }
}
