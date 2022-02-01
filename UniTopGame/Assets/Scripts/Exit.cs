using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public enum Direction
    {
        right,
        left,
        down,
        up,
    }

    public string sceneName = "";
    public int doorNumber = 0;
    public Direction direction = Direction.down;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            RoomManager.ChangeScene(sceneName, doorNumber);
        }
    }
}
