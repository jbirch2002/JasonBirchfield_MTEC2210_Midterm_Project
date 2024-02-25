using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    private float movement;
    private Rigidbody2D rb;
    private Vector3 localScale;

    private void Start()
    {
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Collectable"))
        {
            collectSound.Play();
            // score++
        }
        else if (collision.gameObject.CompareTag("ThemeChange"))
        {
            // Change theme...
        }

    }

}
