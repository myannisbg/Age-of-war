using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebloquageUnitee : MonoBehaviour
{
    public Money argent;
    public GlobalAge ageValue;
    public GameObject ButtonBlocage;
    public TMP_Text buttonText;

    public void cliqueButtonBlocage()
    {
        float cost = 500f * (ageValue.getAge() + 1);
        if (argent.canBuy(cost))
        {
            argent.addGold(-cost);
            ButtonBlocage.SetActive(false);
            cost = 500f * (ageValue.getAge() + 2);
            buttonText.text = "debloquer l'uniter anti armure pour " + cost.ToString() + " pièces";
        }
    }

    public void bloqueUnite4()
    {
        ButtonBlocage.SetActive(true);
    }
}
