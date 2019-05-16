using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    private GameObject borderSetting;
    private GameObject borderHighScore;
    private ParticleSystem particleSystem;
    private SE se;
    private float Volume;
    // Start is called before the first frame update
    void Start()
    {
        //find BorderSettings
        borderSetting = GameObject.Find("BorderSettings");
        borderHighScore= GameObject.Find("BorderHighScore");
        particleSystem= GameObject.Find("Particleshot").GetComponent<ParticleSystem>();
        borderSetting.SetActive(false);
        borderHighScore.SetActive(false);
        //cast to SE
        se = GameObject.Find("SE").GetComponent<SE>();
        //set volume audiosource
        PlayerPrefs.SetFloat("MusicVolume", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void StartButton()
    {
        StartCoroutine(PlayDelay());

    }
    public void SettingsButton()
    {
        //Play SE
        se.PlaySE();
        //show border setting
        borderSetting.SetActive(true);
        //disable border highscore
        borderHighScore.SetActive(false);
        particleSystem.Stop();
    }
    public void InformationButton()
    {
        StartCoroutine(InformationDelay()); 
    }

    public void HighScoreButton()
    {
        //Play SE
        se.PlaySE();
        //show border
        borderHighScore.SetActive(true);
        //disable border setting
        borderSetting.SetActive(false);

        particleSystem.Stop();

    }



    // give delay when play button 
    IEnumerator PlayDelay()
    {
        //Play SE
        se.PlaySE();
        yield return new WaitForSeconds(0.2f);
        //load scene main game
        SceneManager.LoadScene(1);
    }

    // give delay when information button
    IEnumerator InformationDelay()
    {
        //Play SE
        se.PlaySE();
        yield return new WaitForSeconds(0.2f);
        //load scene main game
        SceneManager.LoadScene(2);
    }


}
