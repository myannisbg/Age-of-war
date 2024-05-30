using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unite5 : MonoBehaviour
{
    public GlobalAge ageValue;
    public GameObject boutton5;
    public void tryUnlock(){
        if(ageValue.getAge()==6){
            boutton5.SetActive(true);
        }
        else{
            boutton5.SetActive(false);
        }
    }
}
