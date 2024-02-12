using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    public Animator animator;


    void FixedUpdate()
    {
        float horizontalMovement= Input.GetAxis("Horizontal") * moveSpeed* Time.deltaTime;

        MovePlayers(horizontalMovement);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed",characterVelocity);
    }
    void MovePlayers(float _horizontalMovement)

 


    {
        
    
        Vector3 targetVelocity = new Vector2 (_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity,ref velocity, .05f);
    }
}
