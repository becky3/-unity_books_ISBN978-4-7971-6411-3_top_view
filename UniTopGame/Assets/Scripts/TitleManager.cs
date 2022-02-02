using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject continueButton;
    public string firstSceneName;

    // Start is called before the first frame update
    void Start()
    {
        var sceneName = PlayerPrefs.GetString("LastScene");
        continueButton
            .GetComponent<Button>()
            .interactable = sceneName != "";
        
        SoundManager.soundManager.PlayBgm(SoundManager.BGMType.Title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("PlayerHP", 3);
        PlayerPrefs.SetString("LastScene", firstSceneName);

        RoomManager.doorNumber = 0;
        SceneManager.LoadScene(firstSceneName);
    }

    public void ContinueButtonClicked()
    {
        var sceneName = PlayerPrefs.GetString("LastScene");

        RoomManager.doorNumber = PlayerPrefs.GetInt("LastDoor");
        SceneManager.LoadScene(sceneName);
    }
}
