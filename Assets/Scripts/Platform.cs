﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Simple platform script handling <see cref="Player"/> and <see cref="Bullet"/> bouncing
/// </summary>
public class Platform : MonoBehaviour
{
    [SerializeField] AudioSource bulletbounce = null;
    public float jumpForce = 10f;
    public bool destroy;
    private Transform camera;

    private void Start()
    {
        camera = Camera.main.transform;
        bulletbounce = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(destroy && camera.position.y - 10 > transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb;
            Player player = collision.collider.GetComponent<Player>();
            if (player != null)
            {
                if (!player.IsJetpacking)
                {
                    rb = collision.collider.GetComponentInParent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 velocity = rb.velocity;
                        velocity.y = jumpForce * player.jumpModifier;
                        rb.velocity = velocity;
                        
                        SpriteRenderer sr = GetComponent<SpriteRenderer>();
                        if ( sr.flipX )
                        {
                            Destroy(gameObject);
                        }
                    }
                    return;
                }
            }
            rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce ;
                rb.velocity = velocity;
            }

            Bullet bullet = collision.collider.GetComponent<Bullet>();
            Rigidbody2D bulletrb = collision.collider.GetComponent<Rigidbody2D>();
            if(bullet != null)
            {
                bullet.LevelUpBullet();
                bulletbounce.pitch = 1 * bulletrb.velocity.y;
                bulletbounce.Play();
            }
        }
    }
}
