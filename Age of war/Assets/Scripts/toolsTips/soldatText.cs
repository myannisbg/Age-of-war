using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class soldatText : MonoBehaviour
{
    public LanguageObject langue;
    public TMP_Text buttonText;
    public int unite;
    public SpawnButton price;

    void Start(){
        textUniteActualise();
    }

    public void textUniteActualise(){
        if (langue.getLangue() == "fr"){
            if( unite == 0 ){
                buttonText.text = "UNITE D'INFANTERIE très efficace contre les SUPPORTS " + price.getPrice().ToString() + " pièces";
            }
            else if( unite == 1 ){
                buttonText.text = "UNITE TANK très efficace contre les INFANTERIES " + price.getPrice().ToString() + " pièces";
            }
            else if( unite == 2 ){
                buttonText.text = "UNITE SUPPORT très efficace contre les ANTI-TANKS " + price.getPrice().ToString() + " pièces";
            }
            else{
                buttonText.text = "UNITE ANTI-TANK très efficace contre les TANKS " + price.getPrice().ToString() + " pièces";
            }
        }
        else if (langue.getLangue() == "en"){
            if( unite == 0 ){
                buttonText.text = "INFANTRY UNIT very effective against SUPPORTS " + price.getPrice().ToString() + " coins";
            }
            else if( unite == 1 ){
                buttonText.text = "TANK UNIT very effective against INFANTRY " + price.getPrice().ToString() + " coins";
            }
            else if( unite == 2 ){
                buttonText.text = "SUPPORT UNIT very effective against ANTI-TANKS " + price.getPrice().ToString() + " coins";
            }
            else{
                buttonText.text = "ANTI-TANK UNIT very effective against TANKS " + price.getPrice().ToString() + " coins";
            }
        }
        else if (langue.getLangue() == "es"){
            if( unite == 0 ){
                buttonText.text = "UNIDAD DE INFANTERÍA muy efectiva contra APOYOS " + price.getPrice().ToString() + " monedas";
            }
            else if( unite == 1 ){
                buttonText.text = "UNIDAD DE TANQUE muy efectiva contra INFANTERÍA " + price.getPrice().ToString() + " monedas";
            }
            else if( unite == 2 ){
                buttonText.text = "UNIDAD DE APOYO muy eficaz contra ANTITANQUES " + price.getPrice().ToString() + " monedas";
            }
            else{
                buttonText.text = "UNIDAD ANTITANQUE muy efectiva contra TANQUES " + price.getPrice().ToString() + " monedas";
            }
        }
        else if (langue.getLangue() == "it"){
            if( unite == 0 ){
                buttonText.text = "UNITÀ DI FANTERIA molto efficace contro i SUPPORTI " + price.getPrice().ToString() + " monete";
            }
            else if( unite == 1 ){
                buttonText.text = "UNITÀ CARRO ARMATO molto efficace contro la FANTERIA " + price.getPrice().ToString() + " monete";
            }
            else if( unite == 2 ){
                buttonText.text = "UNITÀ DI SUPPORTO molto efficace contro gli ANTI-TANK " + price.getPrice().ToString() + " monete";
            }
            else{
                buttonText.text = "UNITÀ ANTI-TANK molto efficace contro i TANK " + price.getPrice().ToString() + " monete";
            }
        }
        else if (langue.getLangue() == "al"){
            if( unite==0 ){
                buttonText.text = "INFANTERIEEINHEIT sehr effektiv gegen UNTERSTÜTZUNGEN " + price.getPrice().ToString() + " Münzen frei";
            }
            else if( unite == 1 ){
                buttonText.text = "Panzereinheit sehr effektiv gegen Infanterie " + price.getPrice().ToString() + " Münzen frei";
            }
            else if( unite == 2 ){
                buttonText.text = "UNTERSTÜTZUNGSEINHEIT, sehr effektiv gegen Panzerabwehreinheiten " + price.getPrice().ToString() + " Münzen frei";
            }
            else{
                buttonText.text = "ANTI-TANK-EINHEIT, sehr effektiv gegen TANKS " + price.getPrice().ToString() + " Münzen frei";
            }
        }
    }
}
