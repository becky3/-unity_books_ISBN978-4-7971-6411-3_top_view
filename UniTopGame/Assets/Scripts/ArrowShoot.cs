using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float shootSpeed = 12f;
    public float shootDelay = 0.25f;
    public GameObject bowPrefab;
    public GameObject arrowPrefab;

    public bool inAttack = false;
    GameObject bowObj;

    // Start is called before the first frame update
    void Start()
    {
        var pos = transform.position;
        bowObj = Instantiate(bowPrefab, pos, Quaternion.identity);
        bowObj.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Attack();
        }

        var bowZ = -1;
        var plmv = GetComponent<PlayerController>();
        if (plmv.angleZ > 30 && plmv.angleZ < 150)
        {
            bowZ = 1;
        }

        bowObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ);
        bowObj.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            bowZ
        );
    }

    public void Attack()
    {
        if (ItemKeeper.hasArrows <= 0 || inAttack == true)
        {
            return;
        }

        ItemKeeper.hasArrows -= 1;
        inAttack = true;

        var playerCnt = GetComponent<PlayerController>();
        var angleZ = playerCnt.angleZ;
        var r = Quaternion.Euler(0, 0, angleZ);
        var arrowObj = Instantiate(arrowPrefab, transform.position, r);
        var x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
        var y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
        var v = new Vector3(x, y) * shootSpeed;
        var body = arrowObj.GetComponent<Rigidbody2D>();
        body.AddForce(v, ForceMode2D.Impulse);
        Invoke("StopAttack", shootDelay);
        
        SoundManager.soundManager.SEPlay(SoundManager.SeType.Shoot);
    }

    public void StopAttack()
    {
        inAttack = false;
    }
}
