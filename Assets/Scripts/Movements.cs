using UnityEngine;

public class Movements : MonoBehaviour
{
    [Tooltip("Kecepatan gerak player")]
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input WASD / Panah
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Gerakan Fisika
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    
    // Kita hapus OnTriggerEnter Game Over dari sini karena sudah diurus PlayerHealth
}