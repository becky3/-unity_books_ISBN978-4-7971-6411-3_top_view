using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum GameState
    {
        playing,
        gameOver,
        gameClear,
    }

    public float speed = 3f;

    public string upAnime = "PlayerUp";
    public string downAnime = "PlayerDown";
    public string rightAnime = "PlayerRight";
    public string leftAnime = "PlayerLeft";
    public string deadAnime = "PlayerDead";

    string nowAnimation = "";
    string oldAnimation = "";

    float axisH;
    float axisV;
    public float angleZ = -90f;

    Rigidbody2D rbody;
    bool isMoving = false;

    public static int hp = 3;
    public static GameState gameState;
    bool inDamage = false;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        oldAnimation = downAnime;
        gameState = GameState.playing;

        hp = PlayerPrefs.GetInt("PlayerHP");

        if (hp <= 0)
        {
            hp = 3;
            PlayerPrefs.SetInt("PlayerHP", hp);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != GameState.playing || inDamage)
        {
            return;
        }

        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        var fromPt = transform.position;
        var toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleZ = GetAngle(fromPt, toPt);

        if (angleZ >= -45 && angleZ <= 45)
        {
            nowAnimation = rightAnime;
        }
        else if (angleZ >= 45 && angleZ <= 135)
        {
            nowAnimation = upAnime;
        }
        else if (angleZ >= -135 && angleZ <= -45)
        {
            nowAnimation = downAnime;
        }
        else
        {
            nowAnimation = leftAnime;
        }

        if (nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }
    }

    private void FixedUpdate()
    {
        if (gameState != GameState.playing)
        {
            return;
        }

        if (inDamage)
        {
            var val = Mathf.Sin(Time.time * 50);
            Debug.Log(val);
            gameObject.GetComponent<SpriteRenderer>().enabled = val > 0;
            return;
        }

        rbody.velocity = new Vector2(axisH, axisV) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetDamage(collision.gameObject);
        }

    }

    private void GetDamage(GameObject enemy)
    {
        if (gameState != GameState.playing)
        {
            return;
        }

        hp--;
        
        PlayerPrefs.SetInt("PlayerHP", hp);
        
        Debug.Log("Player HP" + hp);
        if (hp > 0)
        {
            rbody.velocity = new Vector2(0, 0);
            var v = (transform.position - enemy.transform.position).normalized;
            rbody.AddForce(new Vector2(v.x * 4, v.y * 4), ForceMode2D.Impulse);
            inDamage = true;
            Invoke(nameof(DamageEnd), 0.25f);
        }
        else
        {
            GameOver();
        }
    }

    private void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void GameOver()
    {
        Debug.Log("ゲームオーバー！！");
        gameState = GameState.gameOver;

        GetComponent<CircleCollider2D>().enabled = false;
        rbody.velocity = Vector2.zero;
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        GetComponent<Animator>().Play(deadAnime);
        Destroy(gameObject, 1f);
        
        SoundManager.soundManager.StopBgm();
        SoundManager.soundManager.SEPlay(SoundManager.SeType.GameOver);
    }

    public void SetAxis(float h, float v)
    {
        axisH = h;
        axisV = v;
        isMoving = !(axisH == 0 && axisV == 0);
    }

    private float GetAngle(Vector3 p1, Vector2 p2)
    {
        float angle;
        if (axisH != 0 || axisV != 0)
        {
            var dx = p2.x - p1.x;
            var dy = p2.y - p1.y;
            var rad = Mathf.Atan2(dy, dx);
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            angle = angleZ;
        }

        return angle;
    }
}
