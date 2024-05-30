using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Xp : MonoBehaviour
{
    public int xp = 0;
    public Slider xpBarre; // La référence à l'élément de texte UI
    public int maxXP = 100;
    public float multipiclateur = 2;
    public GameObject AmeliorationButton;
    public GlobalAge age;




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


    public void UpdateXpBarre()
    {
        xpBarre.maxValue = maxXP*(age.getAge()+1)*multipiclateur; 
        xpBarre.value = xp;

        if (age.getAge() < 6){
            if (xp >= xpBarre.maxValue){
                AmeliorationButton.SetActive(true);
            }
            else {
                AmeliorationButton.SetActive(false);
            }
        }
    }
}

