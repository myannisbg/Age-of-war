using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    public int gold = 0;
    public TextMeshProUGUI goldText; // La référence à l'élément de texte UI


    void Start()
    {
        if (goldText == null)
        {
                Debug.LogError("Références non définies. Assurez-vous que 'money' et 'goldText' sont attribués.");
            return;
        }

        UpdateGoldText(); // Met à jour le texte au démarrage
    }
    
    public int getGold(){
        return gold;
    }

    public void addGold(int amount){
        gold += amount;
        UpdateGoldText(); // Mettre à jour le texte avec le nouveau montant
    }

    public bool canBuy(int amount){

        if (gold - amount < 0) {
            return false;
        }
        else {
            return true;
        }
    }


    // Met à jour le texte avec le montant d'or
    public void UpdateGoldText()
    {
        goldText.text = "" + getGold();
    }
}
