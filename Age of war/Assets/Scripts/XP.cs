using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Xp : MonoBehaviour
{
    public int xp = 0;
    public Slider xpBarre; // La référence à l'élément de texte UI
    public int maxXP = 100;
    public GameObject AmeliorationButton;


    void Start()
    {
        if (xpBarre == null)
        {
                Debug.LogError("Références non définies.dans XP.cs.");
            return;
        }

        UpdateXpBarre(); // Met à jour le texte au démarrage
    }
    
    public int getXp(){
        return xp;
    }

    public int getMax(){
        return maxXP;
    }

    public void addXp(int amount){
        xp += amount;
        UpdateXpBarre(); // Mettre à jour le texte avec le nouveau montant
    }

    public void setMaxValue(int amount){
        maxXP = amount;
    }

    public bool canBuy(int amount){

        if (xp - amount < 0) {
            return false;
        }
        else {
            return true;
        }
    }


    // Met à jour le texte avec le montant d'or
    public void UpdateXpBarre()
    {
        xpBarre.maxValue = maxXP; 
        xpBarre.value = xp;

        if (xp >= maxXP){
            AmeliorationButton.SetActive(true);
        }
        else {
            AmeliorationButton.SetActive(false);
        }
    }
}

