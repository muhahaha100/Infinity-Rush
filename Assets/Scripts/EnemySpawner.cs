using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple enemy spawn manager.
/// Supports a single type of enemy and spawns it after a slightly variable amount of time.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    private bool side;
    private float cooldown;
    private Transform player;

    private int count = 0;

    public RectTransform rect;
    public GameObject boss = null;

    private float fire = 5;

    private float move = 2;

    private int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        side = false;
        cooldown = Time.time + 10f;
    }

    void Update()
    {
        if(GameManager.Playing() && Time.time > cooldown)
        {
            cooldown = Time.time + Random.Range(3, 10);
            count++;
            SpawnEnemy();
        }

        if ( count > 1 )
        {
            if ( boss != null )
            {
                GameObject bosss = Instantiate(boss);
                bosss.GetComponent<EnemyHealth>().maxHealth = health;
                bosss.GetComponent<EnemyHealth>().SetHealth(health);
                bosss.GetComponent<EnemyHealth>().enemySpawner = this;
                bosss.GetComponent<Boss>().moveRate = move;
                bosss.GetComponent<Boss>().shootRate = fire;
                bosss.transform.position = Camera.main.transform.position + Vector3.right * 20 + Vector3.forward * 10;
                bosss.GetComponent<Boss>().rect = rect;
                rect.parent.gameObject.SetActive(true);
                count = int.MinValue;
            }
        }
    }
    void SpawnEnemy()
    {
        Vector3 spawnPoint = player.position;
        spawnPoint.x = -10;
        if (side)
        {
            spawnPoint.x = 10;
        }
        spawnPoint.y += 8;
        side = !side;
        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }

    public void bossDied()
    {
        if ( move > 0.3 )
        {
            health *= 2;
            fire /= 2;
            move /= 2;
        }
        rect.parent.gameObject.SetActive(false);
        count = -1;
    }
}
