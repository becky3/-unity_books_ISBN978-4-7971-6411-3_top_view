using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static int doorNumber = 0;

    public static void ChangeScene(string sceneName, int doorNumber)
    {
        RoomManager.doorNumber = doorNumber;

        var nowScene = PlayerPrefs.GetString("LastScene");
        if (nowScene != "")
        {
            SaveDataManager.SaveArrangeData(nowScene);
        }
        PlayerPrefs.SetString("LastScene", sceneName);
        PlayerPrefs.SetInt("LastDoor", doorNumber);
        ItemKeeper.SaveItem();
        
        SceneManager.LoadScene(sceneName);

        

    }
    // Start is called before the first frame update
    private void Start()
    {
        var enters = GameObject.FindGameObjectsWithTag("Exit");
        foreach (var doorObj in enters)
        {
            var exit = doorObj.GetComponent<Exit>();
            if (doorNumber != exit.doorNumber)
            {
                continue;
            }

            var x = doorObj.transform.position.x;
            var y = doorObj.transform.position.y;

            switch (exit.direction) 
            {
                case Exit.Direction.right:
                    x += 1;
                    break;
                case Exit.Direction.left:
                    x -= 1;
                    break;
                case Exit.Direction.down:
                    y -= 1;
                    break;
                case Exit.Direction.up:
                    y += 1;
                    break;
            }

            var player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = new Vector3(x, y);
            break;
        }
        
        var sceneName = PlayerPrefs.GetString("LastScene");

        var bgmType = SoundManager.BGMType.InGame;
        if (sceneName == "BossStage")
        {
            bgmType = SoundManager.BGMType.InBoss;
        }
        SoundManager.soundManager.PlayBgm(bgmType);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
