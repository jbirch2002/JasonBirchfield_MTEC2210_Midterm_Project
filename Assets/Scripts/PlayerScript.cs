using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Score score;
    public AudioClip collectSound;
    public AudioClip deathSound;
    new AudioSource audio;
    public AudioSource introSound;
    public AudioSource backgroundMusic;
    public float speed = 5f;
    float movement;
    Rigidbody2D rb;
    Vector3 localScale;

    void Start()
    {
        introSound.Play();
        StartCoroutine(PlayBackgroundMusic(3f));

        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }
    
    void ProcessInputs()
    {
        movement = Input.GetAxisRaw("Horizontal");

        // Stops player from moving when pressing both movement keys.
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            movement = 0f;
        }

        // Flips player when moving left/right.
        if (movement > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z);
        }
        else if (movement < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(localScale.x), localScale.y, localScale.z);
        }
    }

    IEnumerator PlayBackgroundMusic(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
        else
        {
            Debug.LogError("Background music AudioSource missing...");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            introSound.Stop();
            backgroundMusic.Stop();
            audio.PlayOneShot(deathSound, .7f);
            
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;

            StartCoroutine(DestroyAfterSound(deathSound.length));
        }

        if(other.CompareTag("Collectable"))
        {
            score.ScoreIncrease();
            audio.PlayOneShot(collectSound, .7f);
            Destroy(other.gameObject);
        }
    }

    IEnumerator DestroyAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }       
}
