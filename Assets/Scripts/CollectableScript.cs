using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public AudioSource collectSound;
    public float minFallingSpeed = -10f;
    public float maxFallingSpeed = -5f;
    private float fallingSpeed;
    private Rigidbody2D rb;

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
            collectSound.Play();
            Destroy(gameObject, collectSound.clip.length);
        }
    }
}