using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyController : MonoBehaviour
{
    public int hp = 3;
    public float speed = 0.5f;
    public float reactionDistance = 4f;
    public string idleAnime = "EnemyIdle";
    public string upAnime = "EnemyUp";
    public string downAnime = "EnemyDown";
    public string rightAnime = "EnemyRight";
    public string leftAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";
    private string nowAnimation = "";
    private string oldAnimation = "";
    private float axisH;
    private float axisV;
    private Rigidbody2D rbody;
    private bool isActive = false;
    public int arrangeId = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive)
            {
                var dx = player.transform.position.x - transform.position.x;
                var dy = player.transform.position.y - transform.position.y;
                var rad = Mathf.Atan2(dy, dx);
                var angle = rad * Mathf.Rad2Deg;

                nowAnimation = GetAnime(angle);

                axisH = Mathf.Cos(rad);
                axisV = Mathf.Sin(rad);
            }
            else
            {
                var dist = Vector2.Distance(
                    transform.position,
                    player.transform.position
                );
                if (dist < reactionDistance)
                {
                    isActive = true;
                }
            }
        } 
        else if (isActive)
        {
            isActive = false;
            rbody.velocity = Vector2.zero;
        }
        
    }

    private void FixedUpdate()
    {
        if (!isActive || hp <= 0)
        {
            return;
        }

        rbody.velocity = new Vector2(axisH, axisV);
        if (nowAnimation == oldAnimation)
        {
            return;
        }

        oldAnimation = nowAnimation;
        var animator = GetComponent<Animator>();
        animator.Play(nowAnimation);
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
        rbody.velocity = Vector2.zero;
        var animator = GetComponent<Animator>();
        animator.Play(deadAnime);
        Destroy(gameObject, 0.5f);
    }

    private string GetAnime(float angle)
    {
        return angle switch
        {
            > -45.0f and < 45f => rightAnime,
            > 45f and <= 135f => upAnime,
            >= -135f and <= -45f => downAnime,
            _ => leftAnime
        };
    }
}
