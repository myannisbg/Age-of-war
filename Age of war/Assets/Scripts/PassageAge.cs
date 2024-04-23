using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageAge : MonoBehaviour
{
    public GlobalAge ageValue;
    public void ToggleButton(){
        ageValue.incAge();
    }
}

