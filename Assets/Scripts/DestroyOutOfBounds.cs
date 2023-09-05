using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private int lives=3;
    private AudioSource obstacleSound;
    public AudioClip hitSound;
    // Start is called before the first frame update
    void Start()
    {
        obstacleSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -0.7f)
        {
            obstacleSound.PlayOneShot(hitSound, 1);
            Destroy(gameObject);
            Debug.Log("Obstacles landed on ground");
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Bullet"))
        {
            lives--;
            obstacleSound.PlayOneShot(hitSound, 0.5f);
            Destroy(other.gameObject);
            if(lives <= 0)
            {
                obstacleSound.PlayOneShot(hitSound, 1);
                Destroy(gameObject);
            }   
            Debug.Log("Player hits Obstacle");
        }
    }
}
