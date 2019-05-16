using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private enum GameStatus
    {
        Begin, //before game start
        PinataSpawned, //normal game flow when only Pinata spawned
        CandySpawned, //normal game flow when Pinata was broken and candies spawned
        Finished //after game finished
    };

    private GameStatus status = GameStatus.Begin;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject pinata;
    [SerializeField] private GameObject pinataSpawnPos;
    public int score = 0;
    private int highScore;
    public float timeLimit = 30f;
    private GameManager gamemanager;
    private SE se;
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //cast to SE
        se = GameObject.Find("SE").GetComponent<SE>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case GameStatus.Begin:
                SpawnPinata();
                status = GameStatus.PinataSpawned;
                break;
            case GameStatus.PinataSpawned:
                if (IsPinataBroken())
                {
                    status = GameStatus.CandySpawned;
                }
                TimeCountdown();
                break;
            case GameStatus.CandySpawned:
                CheckClick();
                if (AllCandiesReceived())
                {
                    ClearAllCandies();
                    SpawnPinata();
                    status = GameStatus.PinataSpawned;
                }
                TimeCountdown();
                break;
            case GameStatus.Finished:
            default:
                break;
        }
        
        ShowScore();
    }

    private void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            int candyLayer = LayerMask.NameToLayer("Candy");
            int layerMask = 1 << candyLayer;
            // Does the ray intersect candy
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
            {
                score += hit.collider.gameObject.GetComponent<CandyStatus>().score;
                highScore = PlayerPrefs.GetInt("HighScore");
                if (score > highScore)
                {
                    PlayerPrefs.SetInt("HighScore", score);
                    PlayerPrefs.Save();
                }
                if (score < 0)
                {
                    score = 0;
                }
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private void ShowScore()
    {
        scoreText.text = score.ToString();
    }

    private void TimeCountdown()
    {
        timeLimit -= Time.deltaTime;
        timeText.text = ((int)timeLimit+1).ToString();
        if(timeLimit <= 0f)
        {
            timeText.text = "0";
            status = GameStatus.Finished;
            gamemanager.setDisplay(true);
            //play SE time up
            se.PlaySETimeUp();
        }
    }

    private bool IsPinataBroken()
    {
        if (GameObject.FindGameObjectsWithTag("Pinata").Length == 0)
        {
            //Play SE crack
            se.PlaySECrack();
            //Pinata was broken.
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool AllCandiesReceived()
    {
        if(GameObject.FindGameObjectsWithTag("GoodCandy").Length == 0)
        {
            //get all scored candies.
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ClearAllCandies()
    {
        GameObject[] remainingCandies = GameObject.FindGameObjectsWithTag("BadCandy");
        for(int i = 0; i < remainingCandies.Length; i++)
        {
            Destroy(remainingCandies[i]);
        }
    }

    private void SpawnPinata()
    {
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, -123, -90);

        GameObject.Instantiate(pinata, pinataSpawnPos.transform.position, rotation);
    }

    public bool IsGameNormalStatus()
    {
        if(status == GameStatus.PinataSpawned || status == GameStatus.CandySpawned)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
