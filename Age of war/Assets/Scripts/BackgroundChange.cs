using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    public GameObject fond1;
    public GameObject fond2;
    public GameObject fond3;
    public GameObject fond4;
    public GameObject fond5;
    public GlobalAge ageValue;
    // Update is called once per frame

    void Start(){
        updateBackground();
    }
    public void updateBackground(){

        fond1.SetActive(false);
        fond2.SetActive(false);
        fond3.SetActive(false);
        fond4.SetActive(false);
        fond5.SetActive(false);

        if (ageValue.getAge() == 0){
            fond1.SetActive(true);
        }
        else if (ageValue.getAge() == 1){
            fond2.SetActive(true);
        }
        else if (ageValue.getAge() == 2){
            fond3.SetActive(true);
        }
        else if (ageValue.getAge() == 3){
            fond4.SetActive(true);
        }
        else if (ageValue.getAge() == 4){
            fond5.SetActive(true);
        }

    }
}
