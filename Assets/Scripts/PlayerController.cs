using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private float Force = 1;
    [SerializeField] private float jumpForce = 10.0f;
    private int jumpCount;
    [SerializeField] private Animator anim; 
    private string run_anim = "run";
    private string jump_anim = "Jump";
    private string party_anim = "Party";
    private float horizontalInput;
    private float plScale = 0.18f; 
    public GameObject bulletPrefab;
    public Transform spawnLocation;
    public static bool isFlipped = false;
    private int lives = 20;
    public static bool gameOver = false, bulletFired = false, bulletPowerup = false, gameCompleted = false;
    private bool shieldPowerUp = false; 
    private bool moveLeft, moveRight;
    public Slider HealthBar;
    private AudioSource playerAudio;
    public AudioClip jumpSound, fireSound, hitSound, powerUpSound;

    // Start is called before the first frame update
    private void Awake() 
    {
        playerRb = GetComponent<Rigidbody2D>(); 
        playerAudio = GetComponent<AudioSource>();  
    }

    
    private void Start() 
    {
        moveLeft = false;
        moveRight = false;
        gameOver = false;
        bulletFired = false;
        bulletPowerup = false;
        gameCompleted = false;
        shieldPowerUp = false;
        isFlipped = false;
        jumpCount = 0;
        HealthBar.value = lives;
        Time.timeScale = 1;
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }
    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }
    public void PointerUpRight()
    {
        moveRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        animatePlayer();
        checkPlScale();
    }

    void movePlayer()
    {
        if(!gameOver && !gameCompleted)
        {
            if (moveRight)
            {
                horizontalInput = 1;
            }
            else if(moveLeft)
            {
                horizontalInput = -1;
            }
            else
                horizontalInput = 0;
            
            transform.Translate(Vector3.right * Time.deltaTime * Force * horizontalInput);
        }
    }

    public void fireBullet()
    {
        if(!gameOver && !gameCompleted)
        {
            if(!bulletFired || bulletPowerup)
            {
                if(bulletPowerup)
                {
                    playerAudio.PlayOneShot(fireSound, 1.0f);
                    Instantiate(bulletPrefab, spawnLocation.position, bulletPrefab.transform.rotation);
                }
                else
                {
                    Instantiate(bulletPrefab, spawnLocation.position, bulletPrefab.transform.rotation);
                    playerAudio.PlayOneShot(fireSound, 1.0f);
                }
                bulletFired = true;
                StartCoroutine(bulletIsFired());
            }
        }
    }

    public void jumpPlayer()
    {
        if(jumpCount<2 && !gameOver && !gameCompleted)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            anim.SetBool(jump_anim, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            anim.SetBool(jump_anim, false);
        }

        if(other.gameObject.CompareTag("EnemyBullet"))
        { 
            if(!shieldPowerUp)  
                lives-=6;
            HealthBar.value = lives;
            Debug.Log("Enemy hits the player");
            playerAudio.PlayOneShot(hitSound, 1.0f);
            Destroy(other.gameObject);
        }
 
        if(other.gameObject.CompareTag("Obstacles"))
        {
            if(!shieldPowerUp)
                lives-=4;
            HealthBar.value = lives;
            Debug.Log("Obstacles hits the player");
            
            playerAudio.PlayOneShot(hitSound, 1.0f);
            Destroy(other.gameObject);
        }

        if(lives <= 0)  
        {
            Debug.Log("GameOver");
            gameOver = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Finish"))
        {
            playerAudio.PlayOneShot(powerUpSound, 1);
            Debug.Log("Game is over");
            gameCompleted = true;
        }

        if(other.CompareTag("rateOfFirePowerUp"))
        {
            playerAudio.PlayOneShot(powerUpSound, 1);
            Debug.Log("Rate of Fire PowerUp");
            bulletPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(disablePowerUp());
        }

        if(other.CompareTag("shieldPowerUp"))
        {
            playerAudio.PlayOneShot(powerUpSound, 1);
            Debug.Log("Shield PowerUp");
            shieldPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(disablePowerUp());
        }
    }

    void animatePlayer()
    {
        if (!gameOver)
        {
            if(horizontalInput > 0)
            {
                anim.SetBool(run_anim, true);
                transform.localScale = new Vector3(plScale, plScale, plScale);
            }
        
            else if(horizontalInput < 0)
            {
                anim.SetBool(run_anim, true);
                transform.localScale = new Vector3(-plScale, plScale, plScale);
            }

            else
                anim.SetBool(run_anim, false);   
        }

        else if (gameOver)
        {
            anim.SetBool(run_anim, false);
        }

        if(gameCompleted)
        {
            anim.SetBool(run_anim, false);
            anim.SetBool(jump_anim, false);
            anim.SetBool(party_anim, true);
        }
    }
    void checkPlScale()
    {
        if(transform.localScale.x > 0)
        {
            isFlipped=false;
        }
        else
        {
            isFlipped=true;
        }
    }

    IEnumerator disablePowerUp()
    {
        yield return new WaitForSeconds(4);
        bulletPowerup = false;
        shieldPowerUp = false;
        Debug.Log("Power Up disabled");
    }

    IEnumerator bulletIsFired()
    {
        yield return new WaitForSeconds(1);
        bulletFired = false;
    }
}
