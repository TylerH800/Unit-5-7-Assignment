using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Audio")]
    public float defaultVolume = 0.5f;
    public AudioClip[] clips;
    public AudioSource musicSource, sfxSource;

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
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MutedMusic"))
        {
            PlayerPrefs.GetInt("MusicMuted");
        }
        else
        {
            PlayerPrefs.SetInt("MusicMuted", 0);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", defaultVolume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SFXVolume", defaultVolume);
        }

    }

    public void PlayClip(int clipNumber, AudioSource source)
    {
        source.PlayOneShot(clips[clipNumber]);
    }

    public void StopClip()
    {
        musicSource.Stop();
        sfxSource.Stop();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
