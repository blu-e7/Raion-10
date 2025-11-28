using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Tooltip("Kecepatan gerak background (misal 0.1 atau 0.5)")]
    public float speed = 0.1f;
    
    private Renderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Geser tekstur berdasarkan waktu (Time.time)
        // Vector2(0, y) -> Geser sumbu Y (Vertikal)
        // Kalau mau horizontal, ganti jadi Vector2(x, 0)
        
        float offset = Time.time * speed;
        Vector2 textureOffset = new Vector2(offset, 0);

        meshRenderer.material.mainTextureOffset = textureOffset;
    }
}