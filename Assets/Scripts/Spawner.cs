using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Daftar Musuh")]
    public GameObject Enemy1; // Musuh Biasa
    public GameObject Enemy2; // Musuh Menengah (Shotgun)
    public GameObject Enemy3; // Musuh Kuat

    [Header("Pengaturan Spawn")]
    public float spawnInterval = 2f;
    public Vector2 spawnArea = new Vector2(5f, 3f); 

    [Header("Persentase Spawn (Total harus logic 100%)")]
    [Tooltip("Jika angka acak di bawah ini, spawn Enemy 1. Misal 60 berarti 60% peluang.")]
    public int chanceEnemy1 = 60; 
    
    [Tooltip("Batas atas untuk Enemy 2. Misal 90 berarti (90-60) = 30% peluang.")]
    public int chanceEnemy2 = 90; 

    private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // 1. Lempar dadu acak dari 0 sampai 100
        int randomPick = Random.Range(0, 100);
        
        GameObject enemyToSpawn = null;

        // 2. Cek Logika Persentase
        if (randomPick < chanceEnemy1) 
        {
            // Jika angka 0 sampai 59 (60% peluang)
            enemyToSpawn = Enemy1; 
        }
        else if (randomPick < chanceEnemy2) 
        {
            // Jika angka 60 sampai 89 (30% peluang)
            enemyToSpawn = Enemy2; 
        }
        else 
        {
            // Jika angka 90 sampai 99 (10% peluang)
            enemyToSpawn = Enemy3; 
        }

        // 3. Proses Spawn (Sama seperti sebelumnya)
        if (enemyToSpawn != null)
        {
            float randomX = Random.Range(-spawnArea.x, spawnArea.x);
            float randomY = Random.Range(-spawnArea.y, spawnArea.y);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0);

            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x * 2, spawnArea.y * 2, 0));
    }
}