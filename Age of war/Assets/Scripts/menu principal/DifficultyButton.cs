using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;


    public void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        SceneManager.LoadScene("ça marche");
    }
}