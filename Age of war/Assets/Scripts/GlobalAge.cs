using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAge : MonoBehaviour
{
    public int age = 0;

    public int getAge (){
            return age;
        }

    public void incAge(){
        age += 1;
    }
}
