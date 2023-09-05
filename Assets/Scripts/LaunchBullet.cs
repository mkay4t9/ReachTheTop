using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBullet : MonoBehaviour
{
    private float xRange = 10, speed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.isFlipped)
            transform.Translate(Vector3.up * speed * Time.deltaTime);

        else
            transform.Translate(Vector3.up * -speed * Time.deltaTime);

        

        if(transform.position.x > xRange || transform.position.x < -xRange)
        {
            Destroy(gameObject);
        }
    } 
}
