using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeChangeScript : MonoBehaviour
{
    // public AudioSource themeBoxCollectSound;
    public float minFallingSpeed = -10f;
    public float maxFallingSpeed = -5f;
    float fallingSpeed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallingSpeed = Random.Range(minFallingSpeed, maxFallingSpeed);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0, fallingSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //themeBoxCollectSound.Play();
            Debug.Log("Theme Box collected...");
            Destroy(gameObject);
        }

        if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
