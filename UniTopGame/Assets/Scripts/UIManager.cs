using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int hasKeys = 0;
    int hasArrows = 0;
    int hp = 0;

    public GameObject arrowText;

    public GameObject keyText;

    public GameObject hpImage;

    public Sprite life3Image;
    public Sprite life2Image;
    public Sprite life1Image;
    public Sprite life0Image;

    public GameObject mainImage;

    public GameObject resetButton;

    public Sprite gameOverSpr;
    public Sprite gameClearSpr;

    public GameObject inputPanel;

    public string retrySceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        UpdateItemCount();
        UpdateHP();
        Invoke("InactiveImage", 1f);
        resetButton.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        UpdateItemCount();
        UpdateHP();
    }
    
    private void UpdateItemCount()
    {
        if (hasArrows != ItemKeeper.hasArrows)
        {
            arrowText.GetComponent<TMP_Text>().text = ItemKeeper.hasArrows.ToString();
            hasArrows = ItemKeeper.hasArrows;
        }

        if (hasKeys != ItemKeeper.hasKeys)
        {
            keyText.GetComponent<TMP_Text>().text = ItemKeeper.hasKeys.ToString();
            hasKeys = ItemKeeper.hasKeys;
        }
    }

    private void UpdateHP()
    {
        // if (PlayerController.gameState == PlayerController.GameState.gameOver)
        // {
        //     return;
        // }

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }

        if (PlayerController.hp == hp)
        {
            return;
        }

        hp = PlayerController.hp;
        Debug.Log("HP:" + hp);
        switch (hp)
        {
            case <= 0:
                hpImage.GetComponent<Image>().sprite = life0Image;
            
                resetButton.SetActive(true);
                mainImage.SetActive(true);

                mainImage.GetComponent<Image>().sprite = gameOverSpr;
                inputPanel.SetActive(false);
                PlayerController.gameState = PlayerController.GameState.gameOver;
                break;
            
            case 1:
                hpImage.GetComponent<Image>().sprite = life1Image;    
                break;
            case 2:
                hpImage.GetComponent<Image>().sprite = life2Image;    
                break;
            default:
                hpImage.GetComponent<Image>().sprite = life3Image;    
                break;

        }

    }

    public void Retry()
    {
        PlayerPrefs.SetInt("PlayerHP", 3);
        SceneManager.LoadScene(retrySceneName);
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}

    