using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DummyGame : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI diffDisplay;
    public TextMeshProUGUI musicStatus;

    public Animator crossfade;
    public float transitionTime = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadUI();
        StartCoroutine(BGMusic());
    }
    void LoadUI()
    {
        //player name text
        playerName.text = "Player name: " + PlayerPrefs.GetString("PlayerName");
        
        //difficulty text
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 0:
                diffDisplay.text = "Standard difficulty!";
                break;
            case 1:
                diffDisplay.text = "Challenging difficulty!";
                break;
            case 2:
                diffDisplay.text = "Super Buster difficulty!";
                break;
        }

        //music on off text
        {
            if (PlayerPrefs.GetInt("MusicOn") == 0)
            {
                musicStatus.text = "Music Off";
            }
            else
            {
                musicStatus.text = "Music On";
            }
        }

    }


    public void GoToTitle()
    {
        StartCoroutine(Crossfade());
    }

    private IEnumerator Crossfade()
    {
        crossfade.SetTrigger("Crossfade");
        yield return new WaitForSeconds(transitionTime);

        LevelManager.Instance.StopMusic();
        SceneManager.LoadScene("Frontend");
    }

    IEnumerator BGMusic()
    {
        float length = LevelManager.Instance.clips[3].length - 2f;
        while (true)
        {
            LevelManager.Instance.PlayClip(3, LevelManager.Instance.musicSource);
            yield return new WaitForSeconds(length);
        }
    }
}
