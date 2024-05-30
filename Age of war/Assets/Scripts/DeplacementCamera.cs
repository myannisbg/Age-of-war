using UnityEngine;
using UnityEngine.EventSystems;

public class DeplacementCamera : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject mainCamera; // Référence au canvas HUD
    public float moveAmount = 10f; // Quantité de déplacement
    public float bloquageMoin = -100f;
    public float bloquagePlus = 100f;

    private bool isMoving = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMoving = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            float x = cameraPosition.x;
            if (moveAmount > 0){
                if (x < bloquagePlus){
                    mainCamera.transform.position += new Vector3(moveAmount * Time.deltaTime, 0f, 0f);
                }
                else{
                    mainCamera.transform.position = new Vector3(20, -0.04f, -10f);
                }

            }
            if (moveAmount < 0){
                if (x > bloquageMoin){
                    mainCamera.transform.position += new Vector3(moveAmount * Time.deltaTime, 0f, 0f);
                }
                else{
                    mainCamera.transform.position = new Vector3(-4, -0.04f, -10f);
                }
            }
        }
    }
}