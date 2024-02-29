using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    public AudioSource deathSound;
    public float minFallingSpeed = -10f; 
    public float maxFallingSpeed = -5f;
    private float fallingSpeed;
    private Rigidbody2D rb;

    void Awake()
    {
        if (deathSound == null)
        {
            deathSound = GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallingSpeed = Random.Range(minFallingSpeed, maxFallingSpeed);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0, fallingSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            deathSound.Play();
            Destroy(gameObject, deathSound.clip.length);
        }

        if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}