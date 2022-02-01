using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataList arrangeDataList;

    public static void SetArrangeId(int arrangeId, string objTag)
    {
        if (arrangeId == 0 || objTag == "")
        {
            return;
        }

        var newSaveDatas = new SaveData[arrangeDataList.saveDatas.Length + 1];
        for (var i = 0; i < arrangeDataList.saveDatas.Length; ++i)
        {
            newSaveDatas[i] = arrangeDataList.saveDatas[i];
        }

        var saveData = new SaveData();
        saveData.arrangeId = arrangeId;
        saveData.objTag = objTag;

        newSaveDatas[arrangeDataList.saveDatas.Length] = saveData;
        arrangeDataList.saveDatas = newSaveDatas;
    }

    public static void SaveArrangeData(string stageName)
    {
        if (arrangeDataList.saveDatas == null || stageName == "")
        {
            return;
        }

        var saveJson = JsonUtility.ToJson(arrangeDataList);
        PlayerPrefs.SetString(stageName, saveJson);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        arrangeDataList = new SaveDataList();
        arrangeDataList.saveDatas = new SaveData[]{};

        var stageName = PlayerPrefs.GetString("LastScene");
        var data = PlayerPrefs.GetString(stageName);

        if (data == "")
        {
            return;
        }

        arrangeDataList = JsonUtility.FromJson<SaveDataList>(data);

        foreach (var saveData in arrangeDataList.saveDatas)
        {
            var objTag = saveData.objTag;
            var objects = GameObject.FindGameObjectsWithTag(objTag);

            foreach (var obj in objects)
            {
                switch (objTag)
                {
                    case "Door":
                        var door = obj.GetComponent<ItemBox>();
                        if (door.arrangeId == saveData.arrangeId)
                        {
                            Destroy(obj);   
                        }
                        break;
                    case "ItemBox":
                        var box = obj.GetComponent<ItemBox>();
                        if (box.arrangeId == saveData.arrangeId)
                        {
                            box.isClosed = false;
                            box.GetComponent<SpriteRenderer>().sprite = box.openImage;
                        }
                        break;
                    case "Item":
                        var item = obj.GetComponent<ItemData>();
                        if (item.arrangeId == saveData.arrangeId)
                        {
                            Destroy(obj);
                        }
                        break;
                    case "Enemy":
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
