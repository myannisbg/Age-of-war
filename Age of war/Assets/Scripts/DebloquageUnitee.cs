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
    public LanguageObject langue;

    public void cliqueButtonBlocage()
    {
        float cost = 500f * (ageValue.getAge() + 1);
        if (argent.canBuy(cost))
        {
            argent.addGold(-cost);
            ButtonBlocage.SetActive(false);
            cost = 500f * (ageValue.getAge() + 2);
            if (langue.getLangue() == "fr"){
            buttonText.text = "debloquer l'uniter anti armure pour " + cost.ToString() + " coins";
            }
            else if (langue.getLangue() == "en"){
            buttonText.text = "unlock the anti armor unit for " + cost.ToString() + " pièces";
            }
            else if (langue.getLangue() == "es"){
            buttonText.text = "desbloquea la unidad antiarmadura por " + cost.ToString() + " monedas";
            }
            else if (langue.getLangue() == "it"){
            buttonText.text = "sblocca l'unità anti-armatura per " + cost.ToString() + " monete";
            }
            else if (langue.getLangue() == "al"){
            buttonText.text = "Schalte die Panzerabwehreinheit für " + cost.ToString() + " Münzen frei";
            }

        }
    }

    public void bloqueUnite4()
    {
        ButtonBlocage.SetActive(true);
        float cost = 500f * (ageValue.getAge() + 1);
        if (langue.getLangue() == "fr"){
            buttonText.text = "debloquer l'uniter anti armure pour " + cost.ToString() + " pièces";
        }
        else if (langue.getLangue() == "en"){
        buttonText.text = "unlock the anti armor unit for " + cost.ToString() + " coins";
        }
        else if (langue.getLangue() == "es"){
        buttonText.text = "desbloquea la unidad antiarmadura por " + cost.ToString() + " monedas";
        }
        else if (langue.getLangue() == "it"){
        buttonText.text = "sblocca l'unità anti-armatura per " + cost.ToString() + " monete";
        }
        else if (langue.getLangue() == "al"){
        buttonText.text = "Schalte die Panzerabwehreinheit für " + cost.ToString() + " Münzen frei";
        }
    }

    public void actualiseText(){
        float cost = 500f * (ageValue.getAge() + 1);
        if (langue.getLangue() == "fr"){
            buttonText.text = "debloquer l'uniter anti armure pour " + cost.ToString() + " pièces";
        }
        else if (langue.getLangue() == "en"){
        buttonText.text = "unlock the anti armor unit for " + cost.ToString() + " coins";
        }
        else if (langue.getLangue() == "es"){
        buttonText.text = "desbloquea la unidad antiarmadura por " + cost.ToString() + " monedas";
        }
        else if (langue.getLangue() == "it"){
        buttonText.text = "sblocca l'unità anti-armatura per " + cost.ToString() + " monete";
        }
        else if (langue.getLangue() == "al"){
        buttonText.text = "Schalte die Panzerabwehreinheit für " + cost.ToString() + " Münzen frei";
        }
    }
}
