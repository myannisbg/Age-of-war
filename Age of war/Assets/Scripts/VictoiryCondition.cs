using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoiryCondition : MonoBehaviour
{
    public GameObject DarkPannel;
    public GameObject VictoryScreen;
    public GameObject LooseScreen;

    public void Start(){
        DarkPannel.SetActive(false);
        VictoryScreen.SetActive(false);
        LooseScreen.SetActive(false);
    }

    public void victory(){
        DarkPannel.SetActive(true);
        VictoryScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void loose(){
        DarkPannel.SetActive(true);
        LooseScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
