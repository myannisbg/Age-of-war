using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class UpgradeUnit : MonoBehaviour
{
    public int damageBoostsRemaining = 3; // Nombre d'utilisations restantes de la fonction
    public float percentageIncrease = 10f; // Pourcentage d'augmentation des dégâts

    public List<GameObject> unitPrefabsToUpgrade; // Liste des préfabriqués d'unités à améliorer

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // Ajouter un écouteur d'événement pour le clic sur le bouton
            button.onClick.AddListener(BoostDamageForAllies);
        }
    }

    // Fonction pour augmenter les dégâts des unités alliées
    public void BoostDamageForAllies()
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
                    Debug.Log("La préfabriqué " + prefab.name + " n'a pas le bon tag. Mise à jour en cours...");
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
                    Debug.LogWarning("La préfabriqué " + prefab.name + " n'a pas de composant Unit. Veuillez ajouter un composant Unit à cette unité pour la mise à niveau des dégâts.");
                }
            }

            // Décrémenter le nombre d'utilisations restantes
            damageBoostsRemaining--;
        }
        else
        {
            Debug.LogWarning("Vous avez utilisé toutes les améliorations de dégâts disponibles !");
        }
    }
}
