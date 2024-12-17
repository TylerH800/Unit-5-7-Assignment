using TMPro;
using UnityEngine;

public class DummyGame : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI diffDisplay;
    public TextMeshProUGUI musicStatus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadUI();
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
        LevelManager.Instance.LoadScene("Frontend");
    }
}
