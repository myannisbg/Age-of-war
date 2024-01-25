using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    [Range(-1f,1f)]
    public float scrollSpeed = 0.5f;
    private float offset;
    private Material mat;
    private bool direction = false;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {   
        Debug.Log(offset);
        if (offset > 0.5f ){
            direction = true;
        }
        else if (offset < 0f ){
            direction = false;
        }
        if (direction == false){
            offset += (Time.deltaTime * scrollSpeed) / 10f;
        }
        else{
            offset -= (Time.deltaTime * scrollSpeed) / 10f;
        }
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
