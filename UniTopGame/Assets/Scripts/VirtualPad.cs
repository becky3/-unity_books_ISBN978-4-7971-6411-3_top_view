using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class VirtualPad : MonoBehaviour
{
    public float maxLength = 60;
    public bool is4DPad = false;

    [CanBeNull]
    PlayerController player =>
        GameObject
        .FindGameObjectWithTag("Player")?
        .GetComponent<PlayerController>();

    Vector2 defPos;
    Vector2 downPos;

    RectTransform rectTransform => GetComponent<RectTransform>();

    // Start is called before the first frame update
    void Start()
    {
        defPos = rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PadDown()
    {
        downPos = Input.mousePosition;
    }

    public void PadDrag()
    {

        var mousePosition = (Vector2)Input.mousePosition;
        var newTabPos = mousePosition - downPos;
        if (is4DPad == false)
        {
            newTabPos.y = 0;
        }

        var axis = newTabPos.normalized;
        var len = Vector2.Distance(defPos, newTabPos);
        if (len > maxLength)
        {
            newTabPos.x = axis.x * maxLength;
            newTabPos.y = axis.y * maxLength;
        }

        rectTransform.localPosition = newTabPos;
        player?.SetAxis(axis.x, axis.y);


    }

    public void PadUp()
    {
        rectTransform.localPosition = defPos;
        player?.SetAxis(0, 0);
    }

    public void Attack()
    {
        var shoot = player?.GetComponent<ArrowShoot>();
        shoot?.Attack();
    }
}
