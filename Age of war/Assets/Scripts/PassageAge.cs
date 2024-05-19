using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageAge : MonoBehaviour
{
    public BackgroundChange updateBG;
    public GlobalAge ageValue;
    public Bases basesHp;
    public Bot bot;
    public Xp xp;
    public DebloquageUnitee bouttonBloquageUnite4;


    public void ToggleButton(){
        ageValue.incAge();
        xp.UpdateXpBarre();
        updateBG.updateBackground();
        basesHp.AgeUp();
        bot.ResetSpawnTimer();
        bouttonBloquageUnite4.bloqueUnite4();
    }
}

