﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for handling each enemy health values, handling his death, scoring the kill reward and spawning an explosion fx
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public GameObject explosionFx;
    public int maxHealth;
    public int scoreReward;

    public EnemySpawner enemySpawner;

    private int health;
    private ParticleSystem particles;

    private void Awake()
    {
        health = maxHealth;
    }
    void Start()
    {
        particles = GetComponent<ParticleSystem>();  
    }
    /// <summary>
    /// Returns the current enemy health
    /// </summary>
    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }
    /// <summary>
    /// Damages the enemy and handles his death
    /// </summary>
    public void DamageEnemy(int amount)
    {
        particles.Play();
        health -= amount;
        if (health <= 0)
        {
            KillEnemy();
        }
    }
    void KillEnemy()
    {
        if ( scoreReward > 999 )
        {
            enemySpawner.bossDied();
        }
        HighScoreSet.gameScore += scoreReward;
        Instantiate(explosionFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
