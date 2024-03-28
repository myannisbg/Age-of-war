using UnityEngine;
using UnityEngine.EventSystems;

public class DeplacementCamera : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject mainCamera; // Référence au canvas HUD
    public float moveAmount = 10f; // Quantité de déplacement

    private bool isMoving = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("oui");
        isMoving = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("ouiiii");
        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            print("la");
            // Déplacez le canvas HUD par rapport au fond d'écran
            mainCamera.transform.position += new Vector3(moveAmount * Time.deltaTime, 0f, 0f);
        }
    }
}
