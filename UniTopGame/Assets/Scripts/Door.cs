using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (ItemKeeper.hasKeys > 0)
            {
                ItemKeeper.hasKeys--;
                Destroy(gameObject);
                
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
            }    
        }
    }
}
