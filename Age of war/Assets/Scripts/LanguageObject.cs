using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LanguageObject : MonoBehaviour
{
    public GameObject[] languageButtons; // Tableau contenant les boutons de langue
    private GameObject selectedButton; // Bouton de langue sélectionné
    private bool isUnfold = false;
    private float buttonSpacing = 50f;
    private bool active = false;
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
            string buttonName = selectedButton.gameObject.name;

            for (int i = 0; i < languageButtons.Length; i++)
            {
                languageButtons[i].SetActive(false);
                languageButtons[i].transform.position = transform.position;
            }
            selectedButton.SetActive(true);
            if (buttonName == "france"){
                ChangeLocale(1);
            }
            else if(buttonName == "anglais"){
                ChangeLocale(0);
            }
            else if(buttonName == "espagne"){
                ChangeLocale(4);
            }
            else if(buttonName == "italien"){
                ChangeLocale(3);
            }
            else if(buttonName == "allemand"){
                ChangeLocale(2);
            }
            print(selectedButton.gameObject.name);
            // selectedButton.transform.position = transform.position + Vector3.up * (languageButtons.Length - 1) * buttonSpacing;
            isUnfold = false;
        }
    }

    public void ChangeLocale(int localeID){
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }
    IEnumerator SetLocale(int _localeID){
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
    }
}
