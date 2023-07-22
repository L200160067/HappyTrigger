using System.Threading.Tasks;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Setting setting;
    public BGM[] BGM;
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (BGM sound in BGM)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.outputAudioMixerGroup = sound.mixer;
            sound.source.loop = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (FindAnyObjectByType<Setting>(FindObjectsInactive.Include) != null)
        {
            setting = FindAnyObjectByType<Setting>(FindObjectsInactive.Include);
        }
        PlayBGM("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadScene.Instance.isTransitioning)
        {
            PauseBGM("BGM");
        }
        else
        {
            UnPauseBGM("BGM");
        }
    }

    public void PlayBGM(string name)
    {
        BGM bgm = Array.Find(BGM, sound => sound.name == name);
        bgm.source.Play();
    }
    public void StopBGM(string name)
    {
        BGM bgm = Array.Find(BGM, sound => sound.name == name);
        bgm.source.Stop();
    }
    async public void PauseBGM(string name)
    {
        BGM bgm = Array.Find(BGM, sound => sound.name == name);
        float elapsedTime = 0;
        while (elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            bgm.mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(0, -40, elapsedTime / 2));
        }
        await Task.Delay(1000);
        bgm.source.Pause();
    }
    public void UnPauseBGM(string name)
    {
        BGM bgm = Array.Find(BGM, sound => sound.name == name);
        float elapsedTime = 0;
        bgm.source.UnPause();
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            bgm.mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-40, 0, elapsedTime / 1));
        }
    }
}
