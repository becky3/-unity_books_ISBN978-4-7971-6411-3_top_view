using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectGenManager : MonoBehaviour
{
    private ObjectGenPoint[] objGens;
    
    // Start is called before the first frame update
    void Start()
    {
        objGens = GameObject.FindObjectsOfType<ObjectGenPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        var items = GameObject.FindObjectsOfType<ItemData>();
        foreach (var item in items)
        {
            if (item.type == ItemData.Type.Arrow)
            {
                return;
            }
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        if (ItemKeeper.hasArrows != 0 || player == null)
        {
            return;
        }

        var index = Random.Range(0, objGens.Length);
        var objGen = objGens[index];
        objGen.ObjectCrater();
    }
}
