using UnityEngine;

public class DirectionComponent : MonoBehaviour
{
    public bool moveRight = true;

public void SetSpriteDirection(bool moveRight)
{
    // Mettre à jour la direction du sprite en fonction de la direction du joueur
    float horizontalScale = moveRight ? 1f : -1f; // Inverser l'échelle horizontale seulement lorsque moveRight est false
    transform.localScale = new Vector3(horizontalScale, 1f, 1f);
}



}
