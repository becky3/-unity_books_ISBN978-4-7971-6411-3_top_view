#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum BGMType
    {
        None,
        Title,
        InGame,
        InBoss,
    }
    
    public enum SeType
    {
        GameClear,
        GameOver,
        Shoot,
    }

    public static SoundManager soundManager;
    public static BGMType playingBGM = BGMType.None; 
    
    public AudioClip bgmInTitle;
    public AudioClip bgmInGame;
    public AudioClip bgmInBoss;
    public AudioClip meGameClear;
    public AudioClip meGameOver;
    public AudioClip seShoot;
    
    

    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBgm(BGMType type)
    {
        if (type == playingBGM)
        {
            return;
        }

        playingBGM = type;
        var audio = GetComponent<AudioSource>();

        switch (type)
        {
            case BGMType.Title:
                audio.clip = bgmInTitle;
                break;
            case BGMType.InGame:
                audio.clip = bgmInGame;
                break;
            case BGMType.InBoss:
                audio.clip = bgmInBoss;
                break;
        }
        audio.Play();
    }

    public void StopBgm()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    public void SEPlay(SeType type)
    {
        AudioClip? audioClip = null;
        switch (type)
        {
            case SeType.GameClear:
                audioClip = meGameClear;
                break;
            case SeType.GameOver:
                audioClip = meGameOver;
                break;
            case SeType.Shoot:
                audioClip = seShoot;
                break;
        }

        if (audioClip != null)
        {
            GetComponent<AudioSource>().PlayOneShot(audioClip);
        }

    }
}
