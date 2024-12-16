using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FrontendUI : MonoBehaviour
{
    CameraStates camStates;
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicToggle;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
       
    }

    private void Start()
    {        

        

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        print(musicSlider.value);


        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            musicToggle.isOn = true;
            LevelManager.Instance.musicSource.mute = false;
        }
        else
        {
            musicToggle.isOn = false;
            LevelManager.Instance.musicSource.mute = true;

        }

        LevelManager.Instance.PlayClip(0, LevelManager.Instance.musicSource);


       
    }
    #region Title buttons
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
    #endregion

    public void ChooseLevel(string level)
    {
        LevelManager.Instance.LoadScene(level);
    }


    #region audio settings
    public void ToggleMusic(bool value)
    {
        LevelManager.Instance.musicSource.mute = !value;

        if (value)
        {
            PlayerPrefs.SetInt("MusicOn", value ? 1 : 0);
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", value ? 1 : 0);

        }
        print(PlayerPrefs.GetInt("MusicOn"));

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
