using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FrontendUI : MonoBehaviour
{
    CameraStates camStates;
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public GameObject musicToggle;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
       
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        if (PlayerPrefs.GetInt("MutedMusic") == 0)
        {
            musicToggle.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            musicToggle.GetComponent<Toggle>().isOn = false;
        }

        LevelManager.Instance.PlayClip(0, LevelManager.Instance.musicSource);
       
    }
    public void QuitGame()
    {
        Application.Quit();
        print("Quit");
    }

    public void HowToPlayButton()
    {
        camStates = CameraStates.instructions;
    }
    public void StartGameButton()
    {
        camStates = CameraStates.levelSelect;
    }
    public void OptionsButton()
    {
        camStates = CameraStates.options;
    }

    #region audio settings
    public void ToggleMusic(bool value)
    {
        print(value);
        LevelManager.Instance.musicSource.mute = value;
        PlayerPrefs.SetInt("MusicMuted", value ? 1 : 0);
        
    }
    

    void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    #endregion


}
