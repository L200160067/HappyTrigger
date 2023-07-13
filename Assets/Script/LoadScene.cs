using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int changeSceneIndex;
    public Animator transition;
    public GameObject loadingScreen;
    public Slider loadingSlider;

    public void ChangeScene(int sceneIndex = -1)
    {
        StartCoroutine(LoadLevel(sceneIndex != -1 ? sceneIndex : changeSceneIndex));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger("transitionIn");
        yield return new WaitForSeconds(1);
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            yield return null;
        }

    }

}
