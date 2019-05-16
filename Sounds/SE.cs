using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SE : MonoBehaviour
{
    public AudioClip SESoundButton;
    public AudioClip SEPunch;
    public AudioClip SECrack;
    public AudioClip SETimeUp;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
       
      
    }

    // Update is called once per frame
    void Update()
    {
        //check volume music
        float Volume = PlayerPrefs.GetFloat("MusicVolume");
        gameObject.GetComponent<AudioSource>().volume = Volume;
    }

    //Play SE Button
    public void PlaySE()
    {
        audioSource.clip = SESoundButton;
        audioSource.PlayOneShot(audioSource.clip);
    }


    //Play SE Punch
    public void PlaySEPunch()
    {
        audioSource.clip = SEPunch;
        audioSource.PlayOneShot(audioSource.clip);
    }

    //Play SE Crack
    public void PlaySECrack()
    {
        audioSource.clip = SECrack;
        audioSource.PlayOneShot(audioSource.clip);
    }


    //Play SE TimeUp
    public void PlaySETimeUp()
    {
        audioSource.clip = SETimeUp;
        audioSource.PlayOneShot(audioSource.clip);
    }

}
