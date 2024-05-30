using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassageAge : MonoBehaviour
{
    public BackgroundChange updateBG;
    public GlobalAge ageValue;
    public Bases basesHp;
    public Bot bot;
    public Xp xp;
    public DebloquageUnitee bouttonBloquageUnite4;
    public GameObject xpBarre;
    public GameObject AmeliorationButton;
    public unite5 BouttonUnite5;


    public void ToggleButton(){
        ageValue.incAge();
        xp.UpdateXpBarre();
        updateBG.updateBackground();
        basesHp.AgeUp();
        bot.ResetSpawnTimer();
        bouttonBloquageUnite4.bloqueUnite4();
        BouttonUnite5.tryUnlock();

        if (ageValue.getAge() == 6){
            AmeliorationButton.SetActive(false);
            xpBarre.SetActive(false);
        }
    }
}

