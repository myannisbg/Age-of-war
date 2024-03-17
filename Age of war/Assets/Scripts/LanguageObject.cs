using UnityEngine;

public class LanguageObject : MonoBehaviour
{
    public GameObject[] languageButtons; // Tableau contenant les boutons de langue
    private GameObject selectedButton; // Bouton de langue sélectionné
    private bool isUnfold = false;
    private float buttonSpacing = 50f;
    void Start()
    {
        // Au début, tous les boutons sont cachés sauf le premier
        for (int i = 1; i < languageButtons.Length; i++)
        {
            languageButtons[i].SetActive(false);
        }
        selectedButton = languageButtons[0]; // Le premier bouton est sélectionné par défaut
    }

    public void SelectLanguageButton(GameObject button)
    {
        if (isUnfold == false){
            for (int i = 0; i < languageButtons.Length; i++)
            {
                languageButtons[i].SetActive(true);
                languageButtons[i].transform.position = transform.position + Vector3.up * i * buttonSpacing;
            }
            isUnfold = true;

        }
        else{
            selectedButton = button;
            for (int i = 0; i < languageButtons.Length; i++)
            {
                languageButtons[i].SetActive(false);
                languageButtons[i].transform.position = transform.position;
            }
            selectedButton.SetActive(true);
            // selectedButton.transform.position = transform.position + Vector3.up * (languageButtons.Length - 1) * buttonSpacing;
            isUnfold = false;
        }
    }
}
