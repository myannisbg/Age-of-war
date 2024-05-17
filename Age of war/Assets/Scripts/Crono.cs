using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Crono : MonoBehaviour
{
    public Button button;
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    public Sprite[] images;
    private float tempsPassee = 0f;
    private float spetialTime = 0f;
    public float tempEntreChaqueCharge = 10f;
    public SpecialSpawner spawner; // Référence au spawner de projectiles

    public void updateButton()
    {
        tempsPassee = elapsedTime - spetialTime;
        if (tempsPassee >= tempEntreChaqueCharge * 4)
        {
            button.image.sprite = images[4];
        }
        else if (tempsPassee >= tempEntreChaqueCharge * 3)
        {
            button.image.sprite = images[3];
        }
        else if (tempsPassee >= tempEntreChaqueCharge * 2)
        {
            button.image.sprite = images[2];
        }
        else if (tempsPassee >= tempEntreChaqueCharge)
        {
            button.image.sprite = images[1];
        }
    }

    public void buttonSpetialPress()
    {
        if (button.image.sprite == images[4])
        {
            Debug.Log("Button pressed with full gauge"); // Ajoutez un message de débogage
            spetialTime = elapsedTime;
            button.image.sprite = images[0];
            tempsPassee = 0f;

            // Lancer l'attaque spéciale
            if (spawner != null)
            {
                Debug.Log("Calling StartSpawning on spawner"); // Ajoutez un message de débogage
                spawner.StartSpawning();
            }
            else
            {
                Debug.LogError("Spawner is not assigned"); // Ajoutez un message d'erreur
            }
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        updateButton();
    }
}
