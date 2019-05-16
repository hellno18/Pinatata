using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateCandies : MonoBehaviour
{
    [SerializeField] private GameObject[] candies;
    [SerializeField] private GameObject[] bonusCandies;
    [SerializeField] private GameObject candySpawnPos;
    [SerializeField] private Material crackedTexture;
    [SerializeField] private GameObject brokenPinata;
    [SerializeField] private int maxCandies = 10;
    [SerializeField] private int bonusCandyRate = 20;
    [SerializeField] private int bonusCakeRate = 10;
    [SerializeField] private int hp = 20;
    private ScoreManager scoreManager;
    private SE se;
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        //cast to SE
        se = GameObject.Find("SE").GetComponent<SE>();
    }

    private void Update()
    {
        //click mouse, touch
        if (Input.GetMouseButtonDown(0) && scoreManager.IsGameNormalStatus())
        {
            //play SE punch
            se.PlaySEPunch();
            
            RaycastHit hit;
            // Does the ray intersect any objects
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                //check if ray hit this object
                if(hit.collider.gameObject == this.gameObject.transform.GetChild(0).gameObject)
                {
                    if(hp > 0)
                    {
                        //reduces HP;
                        hp--;
                        if(hp == 5)
                        {
                            ChangeToCrackedTexture();
                        }
                    }
                    else
                    {
                        //Spawn candies when HP reached 0
                        SpawnCandies();
                        //destroy this Pinata
                        BreakPinata();
                    }
                }
            }
        }
    }

    private void SpawnCandies()
    {
        //instantiate candies
        for (int i = 0; i < maxCandies; i++)
        {
            //create random position for each candies
            Vector3 randomPosition = candySpawnPos.transform.position + (Random.insideUnitSphere * 1.0f);
            randomPosition.y = candySpawnPos.transform.position.y;

            //create random rotation for each candies
            float randomX = Random.Range(0, 360);
            float randomY = Random.Range(0, 360);
            float randomZ = Random.Range(0, 360);
            Quaternion randomQuaternion = new Quaternion();
            randomQuaternion.eulerAngles = new Vector3(randomX, randomY, randomZ);

            //random candy type
            int candyType = Random.Range(0, candies.Length);

            //got candy.
            GameObject spawnCandy;
            if (candyType == 0)
            {
                if (Random.Range(0, 100) < bonusCandyRate)
                {
                    spawnCandy = bonusCandies[0];
                }
                else
                {
                    spawnCandy = candies[candyType];
                }
            }
            else if (candyType == 1)
            {
                if (Random.Range(0, 100) < bonusCakeRate)
                {
                    spawnCandy = bonusCandies[1];
                }
                else
                {
                    spawnCandy = candies[candyType];
                }
            }
            else
            {
                spawnCandy = candies[candyType];
            }
            
            Object.Instantiate(spawnCandy, randomPosition, randomQuaternion);
        }
    }

    private void ChangeToCrackedTexture()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = crackedTexture;
    }

    private void BreakPinata()
    {
        Vector3 brokenPinataSpawnPos = new Vector3();
        brokenPinataSpawnPos.x = this.gameObject.transform.position.x;
        brokenPinataSpawnPos.y = this.gameObject.transform.GetChild(0).transform.position.y;
        brokenPinataSpawnPos.z = this.gameObject.transform.position.z;
        var go = GameObject.Instantiate(brokenPinata, brokenPinataSpawnPos, this.gameObject.transform.rotation);
        Destroy(go, 0.3f);
        Destroy(this.gameObject);
    }
}
