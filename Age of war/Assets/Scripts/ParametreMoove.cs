using System.Collections;
using UnityEngine;


public class ParametreMoove : MonoBehaviour
{
    audio audioManager; 
    public GameObject panelDark;
    public GameObject objectToMove; // Référence vers l'objet à déplacer
    public float targetHeight = 5f; // Hauteur cible à laquelle déplacer l'objet
    public float moveSpeed = 2f; // Vitesse de déplacement

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audio>();
    }
    public void MoveDown()
    {
        audioManager.PlaySFX(audioManager.menuWoosh); //woosh song
        panelDark.SetActive(false);
        StartCoroutine(MoveToPosition(new Vector3(objectToMove.transform.position.x, -targetHeight, objectToMove.transform.position.z)));
    }

    // Fonction appelée lorsque le bouton est cliqué
    public void MoveUp()
    {
        audioManager.PlaySFX(audioManager.menuWoosh); //woosh song
        panelDark.SetActive(true);
        // Lance la coroutine de déplacement
        StartCoroutine(MoveToPosition(new Vector3(objectToMove.transform.position.x, targetHeight, objectToMove.transform.position.z)));
    }

    // Coroutine pour déplacer progressivement l'objet vers la position cible
    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        // Tant que l'objet n'a pas atteint la position cible
        while (objectToMove.transform.position != targetPosition)
        {
            // Déplace progressivement l'objet vers la position cible
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Attend la prochaine frame
            yield return null;
        }
    }
}
