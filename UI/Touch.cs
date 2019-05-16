using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Touch : MonoBehaviour
{
    private int pages;
    private GameObject howtoplayImage;
    private GameObject detailInformationImage;
    private GameObject description;
    private SE se;
    // Start is called before the first frame update
    void Start()
    {
        pages = 1;
        //Find objects
        howtoplayImage = GameObject.Find("HowToPlay");
        detailInformationImage = GameObject.Find("DetailInformation");
        description = GameObject.Find("Description");
        detailInformationImage.SetActive(false);
        //cast to SE
        se = GameObject.Find("SE").GetComponent<SE>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            //Play SE
            se.PlaySE();
            pages++;
            if (pages == 1)
            {
                detailInformationImage.SetActive(false);
                howtoplayImage.SetActive(true);
                description.SetActive(true);


            }
            if (pages == 2)
            {
                howtoplayImage.SetActive(false);
                detailInformationImage.SetActive(true);
                description.SetActive(false);
            }
            if (pages == 3)
            {
                SceneManager.LoadScene(0);
            }
            
        }
    }


}
