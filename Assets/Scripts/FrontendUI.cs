using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;



public class FrontendUI : MonoBehaviour
{
    CameraStates camStates;

    public float defaultVolume = 0.5f;

    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicToggle;

    public TMP_Dropdown difficultyDrop;
    public TMP_InputField playerNameInput;

    public Animator crossfade;
    public float transitionTime = 1;

    private void Awake()
    {
        LoadSettings();
    }

    private void Start()
    {        
        SetUIValues();
   
        StartCoroutine(TitleMusic());
    }

    #region playing sound
    //underscore so at the top of function list
    public void _ButtonPressSFX()
    {
        LevelManager.Instance.PlayClip(4, LevelManager.Instance.sfxSource);
      
    }
    public void _PageChangeSFX()
    {
        LevelManager.Instance.PlayClip(1, LevelManager.Instance.sfxSource);
        
    }
    public void _LoadLevelSFX()
    {
        LevelManager.Instance.PlayClip(2, LevelManager.Instance.sfxSource);
        

    }

    IEnumerator TitleMusic()
    {
        float length = LevelManager.Instance.clips[0].length - 2f;
        while (true)
        {
            LevelManager.Instance.PlayClip(0, LevelManager.Instance.musicSource);
            yield return new WaitForSeconds(length);
        }
    }

    #endregion

    #region load and set ui/settings
    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", "Player");
        }
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            PlayerPrefs.GetInt("Difficulty");
        }
        else
        {
            PlayerPrefs.SetInt("Difficulty", 0);
        }

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

        if (PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.GetInt("MusicOn");
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }
    }

    
    void SetUIValues()
    {
        StartCoroutine(PreventStartSFX());
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        difficultyDrop.value = PlayerPrefs.GetInt("Difficulty");
        playerNameInput.text = PlayerPrefs.GetString("PlayerName");


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
    }

    IEnumerator PreventStartSFX() //prevents sfx from playing if SetUIValues changes a value like a toggle
    {
        LevelManager.Instance.sfxSource.mute = true;
        yield return new WaitForSeconds(0.5f);
        LevelManager.Instance.sfxSource.mute = false;
    }
    #endregion

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
    
    public void ChooseLevel(string level)
    {
        StartCoroutine(Crossfade(level));
    }


    private IEnumerator Crossfade(string level)
    {
        crossfade.SetTrigger("Crossfade");
        yield return new WaitForSeconds(transitionTime);

        LevelManager.Instance.StopMusic();
        SceneManager.LoadScene(level);
    }
    #endregion

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
    

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    #endregion

    #region other settings

    public void ChangeDifficulty(int value)
    {        
        PlayerPrefs.SetInt("Difficulty", value);        
        
    }

    public void ChangePlayerName(string input)
    {
        PlayerPrefs.SetString("PlayerName", input);
        print(name);
    }
    #endregion


}
