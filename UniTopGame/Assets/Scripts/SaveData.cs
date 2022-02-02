using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SaveData
{
    public int arrangeId = 0;
    public string objTag = "";
}

[System.Serializable]
public class SaveDataList
{
    public SaveData[] saveDatas;
}
