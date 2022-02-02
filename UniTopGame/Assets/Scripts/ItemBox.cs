using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImage;

    public GameObject itemPrefab;

    public bool isClosed = true;

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
        if (!isClosed || !col.gameObject.CompareTag("Player"))
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = openImage;
        isClosed = false;
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        
        SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
    }
}
