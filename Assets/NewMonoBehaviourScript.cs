using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 2f;
    public Rigidbody2D rb;
    public Vector2 movement;

    void awake()
    {
        
    }
    void Start()
    {
        Debug.Log("start");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");
       //rb.linearVelocity = movement*speed;
       rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

}
