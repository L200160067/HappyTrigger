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
    public bool isTransitioning;
    public static LoadScene Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ChangeScene(int sceneIndex = -1)
    {
        StartCoroutine(LoadLevel(sceneIndex != -1 ? sceneIndex : changeSceneIndex));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        isTransitioning = true;
        transition.SetTrigger("transitionIn");
        yield return new WaitForSeconds(1);
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            loadingSlider.value = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }

    }

}
