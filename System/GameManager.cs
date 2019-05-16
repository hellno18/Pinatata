using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject borderGameOver;
    private bool display;
    private SE se;
    // Start is called before the first frame update
    void Start()
    {
        //cast to SE
        se = GameObject.Find("SE").GetComponent<SE>();
        borderGameOver = GameObject.Find("BorderGameOver");
        borderGameOver.SetActive(false);
        display = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (display == true)
        {
            borderGameOver.SetActive(true);
        }

    }


    public void ResumeButton()
    {
        //Play SE Button
        se.PlaySE();
        SceneManager.LoadScene(1);
    }


    public void QuitButton()
    {
        //Play SE Button
        se.PlaySE();
        SceneManager.LoadScene(0);
    }

    public bool getDisplay()
    {
        return display;
    }
    public void setDisplay(bool x)
    {
        display = x;
    }

}
