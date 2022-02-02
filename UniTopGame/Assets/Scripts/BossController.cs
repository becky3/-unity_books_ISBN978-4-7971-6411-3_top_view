using System;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int hp = 10;
    public float reactionDistance = 7f;

    public GameObject bulletPrefab;
    public float shootSpeed = 5f;

    private bool inAttack = false;

    private void Update()
    {
        if (hp <= 0)
        {
            return;
        }

        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            var plpos = player.transform.position;
            var dist = Vector2.Distance(transform.position, plpos);
            if (dist <= reactionDistance && inAttack == false)
            {
                inAttack = true;
                GetComponent<Animator>().Play("BossAttack");
            } else if (dist > reactionDistance && inAttack)
            {
                inAttack = false;
                GetComponent<Animator>().Play("BossIdle");
            }
        }
        else
        {
            inAttack = false;
            GetComponent<Animator>().Play("BossIdle");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Arrow"))
        {
            return;
        }

        hp--;
        if (hp > 0)
        {
            return;
        }

        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Animator>().Play("BossDead");
        Destroy(gameObject, 1);
    }

    void Attack()
    {
        var tr = transform.Find("gate");
        var gate = tr.gameObject;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }

        var dx = player.transform.position.x - gate.transform.position.x;
        var dy = player.transform.position.y - gate.transform.position.y;

        var rad = Mathf.Atan2(dy, dx);
        var angle = rad * Mathf.Rad2Deg;

        var r = Quaternion.Euler(0, 0, angle);
        var bullet = Instantiate(bulletPrefab, gate.transform.position, r);
        var x = Mathf.Cos(rad);
        var y = Mathf.Sin(rad);
        var v = new Vector3(x, y) * shootSpeed;
        var rbody = bullet.GetComponent<Rigidbody2D>();
        rbody.AddForce(v, ForceMode2D.Impulse);

    }
}
