using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class UpgradeUnit : MonoBehaviour
{
    public float percentageIncrease = 10f; // Pourcentage d'augmentation des dégâts ou des points de vie

    public List<GameObject> unitPrefabsToUpgrade; // Liste des préfabriqués d'unités à améliorer

    public int buttonNumber; 

    private int damageBoostsRemainingBtn1 = 3; // Nombre d'utilisations restantes pour augmenter les dégâts avec le bouton 1
    private int damageBoostsRemainingBtn2 = 3; // Nombre d'utilisations restantes pour augmenter les dégâts avec le bouton 2
    private int damageBoostsRemainingBtn3 = 3; // Nombre d'utilisations restantes pour augmenter les dégâts avec le bouton 3
    private int damageBoostsRemainingBtn4 = 3; // Nombre d'utilisations restantes pour augmenter les dégâts avec le bouton 4

    private int healthBoostsRemainingBtn5 = 3; // Nombre d'utilisations restantes pour augmenter les points de vie avec le bouton 5
    private int healthBoostsRemainingBtn6 = 3; // Nombre d'utilisations restantes pour augmenter les points de vie avec le bouton 6
    private int healthBoostsRemainingBtn7 = 3; // Nombre d'utilisations restantes pour augmenter les points de vie avec le bouton 7

    void Start()
{
    Button button = GetComponent<Button>();
    if (button != null)
    {
        // Ajouter un écouteur d'événement pour le clic sur le bouton
        button.onClick.AddListener(() => UpgradeUnits(buttonNumber));
    }
}


    // Fonction appelée lorsque l'utilisateur appuie sur un bouton pour améliorer les unités
    public void UpgradeUnits(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 1:
                BoostDamageForAllies(ref damageBoostsRemainingBtn1);
                break;
            case 2:
                BoostDamageForAllies(ref damageBoostsRemainingBtn2);
                break;
            case 3:
                BoostDamageForAllies(ref damageBoostsRemainingBtn3);
                break;
            case 4:
                BoostDamageForAllies(ref damageBoostsRemainingBtn4);
                break;
            case 5:
                BoostHealthForAllies(ref healthBoostsRemainingBtn5);
                break;
            case 6:
                BoostHealthForAllies(ref healthBoostsRemainingBtn6);
                break;
            case 7:
                BoostHealthForAllies(ref healthBoostsRemainingBtn7);
                break;
            default:
                Debug.LogWarning("Numéro de bouton non reconnu !");
                break;
        }
    }

    // Fonction pour augmenter les dégâts des unités alliées
    private void BoostDamageForAllies(ref int damageBoostsRemaining)
    {
        // Vérifier s'il reste des utilisations de la fonction
        if (damageBoostsRemaining > 0)
        {
            // Parcourir la liste des préfabriqués d'unités à améliorer
            foreach (var prefab in unitPrefabsToUpgrade)
            {
                // Vérifier si le préfabriqué a le bon tag, sinon le mettre à jour
                if (!prefab.CompareTag("Ally"))
                {
                    Debug.Log("Le préfabriqué " + prefab.name + " n'a pas le bon tag. Mise à jour en cours...");
                    prefab.tag = "Ally";
                }
                
                // Vérifier si le préfabriqué a un composant Unit
                var unitComponent = prefab.GetComponent<Unit>();
                if (unitComponent != null)
                {
                    // Augmenter les dégâts de l'unité
                    unitComponent.damageDealt *= (1f + percentageIncrease / 100f);
                }
                else
                {
                    Debug.LogWarning("Le préfabriqué " + prefab.name + " n'a pas de composant Unit. Veuillez ajouter un composant Unit à cette unité pour la mise à niveau des statistiques.");
                }
            }

            // Décrémenter le nombre d'utilisations restantes
            damageBoostsRemaining--;
        }
        else
        {
            Debug.LogWarning("Vous avez utilisé toutes les améliorations disponibles pour les dégâts !");
        }
    }

    // Fonction pour augmenter les points de vie des unités alliées
    private void BoostHealthForAllies(ref int healthBoostsRemaining)
    {
        // Vérifier s'il reste des utilisations de la fonction
        if (healthBoostsRemaining > 0)
        {
            // Parcourir la liste des préfabriqués d'unités à améliorer
            foreach (var prefab in unitPrefabsToUpgrade)
            {
                // Vérifier si le préfabriqué a le bon tag, sinon le mettre à jour
                if (!prefab.CompareTag("Ally"))
                {
                    Debug.Log("Le préfabriqué " + prefab.name + " n'a pas le bon tag. Mise à jour en cours...");
                    prefab.tag = "Ally";
                }
                
                // Vérifier si le préfabriqué a un composant Unit
                var unitComponent = prefab.GetComponent<Unit>();
                if (unitComponent != null)
                {
                    // Augmenter les points de vie de l'unité
                    unitComponent.maxHealth *= (1f + percentageIncrease / 100f);
                }
                else
                {
                    Debug.LogWarning("Le préfabriqué " + prefab.name + " n'a pas de composant Unit. Veuillez ajouter un composant Unit à cette unité pour la mise à niveau des statistiques.");
                }
            }

            // Décrémenter le nombre d'utilisations restantes
            healthBoostsRemaining--;
        }
        else
        {
            Debug.LogWarning("Vous avez utilisé toutes les améliorations disponibles pour les points de vie !");
        }
    }
}