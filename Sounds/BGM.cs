using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance = null;
    public AudioClip BGMSound;
    private bool isSound;
    private AudioSource audioSource;

    public static BGM Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        //while gameobject actived, BGM still play
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        //Play BGM
        isSound = true;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = BGMSound;
        audioSource.Play();
    }
    void Update()
    {
        float Volume = PlayerPrefs.GetFloat("MusicVolume");
        gameObject.GetComponent<AudioSource>().volume = Volume;
    }

    public bool getSoundBool()
    {
        return isSound;
    }
    public void setSoundBool(bool x)
    {
        isSound = x;
    }
    public AudioSource getAudioScource()
    {
        return audioSource;
    }
}
