using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    public GameObject fond1avant;
    public GameObject fond2avant;
    public GameObject fond3avant;
    public GameObject fond4avant;
    public GameObject fond5avant;
    public GameObject fond6avant;
    public GameObject fond7avant;

    public GameObject fond1arriere;
    public GameObject fond2arriere;
    public GameObject fond3arriere;
    public GameObject fond4arriere;
    public GameObject fond5arriere;
    public GameObject fond6arriere;
    public GameObject fond7arriere;

    public GlobalAge ageValue;
    // Update is called once per frame

    void Start(){
        updateBackground();
    }
    public void updateBackground(){

        fond1avant.SetActive(false);
        fond2avant.SetActive(false);
        fond3avant.SetActive(false);
        fond4avant.SetActive(false);
        fond5avant.SetActive(false);
        fond6avant.SetActive(false);
        fond7avant.SetActive(false);

        fond1arriere.SetActive(false);
        fond2arriere.SetActive(false);
        fond3arriere.SetActive(false);
        fond4arriere.SetActive(false);
        fond5arriere.SetActive(false);
        fond6arriere.SetActive(false);
        fond7arriere.SetActive(false);

        if (ageValue.getAge() == 0){
            fond1avant.SetActive(true);
            fond1arriere.SetActive(true);
        }
        else if (ageValue.getAge() == 1){
            fond2avant.SetActive(true);
            fond2arriere.SetActive(true);
        }
        else if (ageValue.getAge() == 2){
            fond3avant.SetActive(true);
            fond3arriere.SetActive(true);
        }
        else if (ageValue.getAge() == 3){
            fond4avant.SetActive(true);
            fond4arriere.SetActive(true);
        }
        else if (ageValue.getAge() == 4){
            fond5avant.SetActive(true);
            fond5arriere.SetActive(true);
        }
        else if (ageValue.getAge() == 5){
            fond6avant.SetActive(true);
            fond6arriere.SetActive(true);
        }
        else if (ageValue.getAge() == 6){
            fond7avant.SetActive(true);
            fond7arriere.SetActive(true);
        }

    }
}
