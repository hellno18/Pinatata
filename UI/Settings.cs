using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private Animator anim;
    private GameObject soundOn;
    private GameObject soundOff;
    private BGM titlemenu;
    private SE se;
    private bool screen;
    // Start is called before the first frame update
    void Awake()
    {
        //declare animator
        anim = this.gameObject.GetComponent<Animator>();
        //find Object
        soundOn = GameObject.Find("SoundsOnButton");
        soundOff = GameObject.Find("SoundsOffButton");
        //cast to titlemenu 
        titlemenu = GameObject.Find("BGM").GetComponent<BGM>();
        //cast to SE
        se = GameObject.Find("SE").GetComponent<SE>();
        screen = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if border is active
        if (this.gameObject.activeInHierarchy == true)
        {
            anim.SetBool("isSettingButtonClicked", true);
            anim.Play("Border");
            if (titlemenu.getSoundBool() == true)
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
            }
            if (titlemenu.getSoundBool() == false)
            {
                soundOn.SetActive(false);
                soundOff.SetActive(true);
            }
        }
        //if border is false
        if (this.gameObject.activeInHierarchy == false)
        {
            anim.SetBool("isSettingButtonClicked", false);
            anim.Play("BorderIdle");
        }

       
        
    }
   
    //while close button clicked
    public void ClosedButton()
    {
        StartCoroutine(ClosedButtonDelay());
    }

    //while Sound on picture  --> off
    public void SoundOnButton()
    {
        //set volume
        PlayerPrefs.SetFloat("MusicVolume", 0f);
        //Mute SE
        se.PlaySE();
        titlemenu.setSoundBool(false);
        titlemenu.getAudioScource().mute=!titlemenu.getAudioScource().mute;
    }

    //while Sound off picture  --> on
    public void SoundOffButton()
    {
        //set volume
        PlayerPrefs.SetFloat("MusicVolume", 1.0f);
        //Play SE
        se.PlaySE();
        titlemenu.setSoundBool(true);
        titlemenu.getAudioScource().mute = !titlemenu.getAudioScource().mute;
    }

    public void WindowButton()
    {
        if (screen == true)
        {
            //Play SE
            se.PlaySE();
            screen = false;
            Screen.SetResolution(1920, 1080, false);
        }
        else
        {
            //Play SE
            se.PlaySE();
            screen = true;
            Screen.SetResolution(1920, 1080, true);
        }
       
    }
    //give delay 0.2 second
    IEnumerator ClosedButtonDelay()
    {
        //Play SE
        se.PlaySE();
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        GameObject.Find("Particleshot").GetComponent<ParticleSystem>().Play();
    }
}
