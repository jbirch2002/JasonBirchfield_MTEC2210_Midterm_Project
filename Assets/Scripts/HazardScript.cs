using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    public AudioClip deathSound;
    new AudioSource audio;
    public float minFallingSpeed = -10f; 
    public float maxFallingSpeed = -5f;
    float fallingSpeed;
    Rigidbody2D rb;

    void Start()
    {
        audio = GetComponent<AudioSource>();
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
            audio.PlayOneShot(deathSound, .07f);
            Destroy(gameObject);
        }

        if (other.CompareTag("Floor")) Destroy(gameObject);
    }
}