using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public AudioSource introSound;
    public AudioSource backgroundMusic;
    public float speed = 5f;
    private float movement;
    private Rigidbody2D rb;
    private Vector3 localScale;

    private void Start()
    {
        introSound.Play();
        StartCoroutine(PlayBackgroundMusic(3f));

        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }
    
    private void ProcessInputs()
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
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the background music if it's available
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
        else
        {
            Debug.LogError("Background music AudioSource missing...");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            introSound.Play();
            backgroundMusic.Stop();
            Destroy(gameObject);
        }
    }
}
