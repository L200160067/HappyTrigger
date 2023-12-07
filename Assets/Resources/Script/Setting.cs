using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public Slider BGMSlider, SFXSlider;
    public AudioMixer myMixer;
    public static Setting Instance;
    float BGMVol, SfxVol;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        myMixer.GetFloat("BGMVolume", out BGMVol);
        BGMSlider.value = BGMVol;
        myMixer.GetFloat("SFXVolume", out SfxVol);
        SFXSlider.value = SfxVol;
    }
    private void Start()
    {
        gameObject.SetActive(false);

    }

    public void SetBGMVolume(float volume)
    {
        volume = BGMSlider.value;
        myMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        volume = SFXSlider.value;
        myMixer.SetFloat("SFXVolume", volume);
    }

    public void BackToMenu()
    {
        FindObjectOfType<LoadScene>().ChangeScene(0);
        Time.timeScale = 1;
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        gameObject.SetActive(true);
        // Time.timeScale = 0;
    }
}
