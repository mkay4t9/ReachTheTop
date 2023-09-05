using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPos;
    private int lives = 3;
    private AudioSource enemySound;
    public AudioClip fireSound, hitSound;
    // Start is called before the first frame update
    void Start()
    {
        enemySound = GetComponent<AudioSource>(); 
        InvokeRepeating("SpawnBullet", 4.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBullet()
    {
        if(!PlayerController.gameOver && !PlayerController.gameCompleted)
        {
            enemySound.PlayOneShot(fireSound, 0.5f);
            Instantiate(bulletPrefab, spawnPos.position, bulletPrefab.transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Bullet"))
        {
            lives--;
            enemySound.PlayOneShot(hitSound, 0.5f);
            Destroy(other.gameObject);
            Debug.Log("Player hits Enemy");
            if(lives <= 0)
            {
                Destroy(gameObject);
                enemySound.PlayOneShot(hitSound, 0.5f);
            }
        }
    }
}
