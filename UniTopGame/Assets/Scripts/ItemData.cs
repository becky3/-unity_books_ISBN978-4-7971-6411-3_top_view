using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public enum Type
    {
        Arrow,
        Key,
        Life,
    }

    public Type type;
    public int count = 1;
    public int arrangeId;
    
    
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
        if (!col.gameObject.CompareTag("Player")) return;
        switch (type)
        {
            case Type.Arrow:
                var shoot = col.gameObject.GetComponent<ArrowShoot>();
                ItemKeeper.hasArrows += count;
                break;
            case Type.Key:
                ItemKeeper.hasKeys += 1;
                break;
            case Type.Life:
                if (PlayerController.hp < 3)
                {
                    PlayerController.hp++;
                    PlayerPrefs.SetInt("PlayerHP", PlayerController.hp);
                }
                break;
        }

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        var itemBody = GetComponent<Rigidbody2D>();
        itemBody.gravityScale = 2.5f;
        itemBody.AddForce(new Vector2(0,6), ForceMode2D.Impulse);
        Destroy(gameObject, 0.5f);
        
        SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);

    }
}
