using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSpeedUpgrade : Upgrade
{
    [SerializeField] float shotSpeedModifier = 1.5F;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<PlayerShoot>().cooldown /= shotSpeedModifier;
            StartCoroutine(shutOffUpgrade(collision.gameObject.GetComponentInChildren<PlayerShoot>()));
            active = false;
            this.enabled = false;
            spriteRenderer.enabled = false;
            audioSource.Play();
        }
    }

    private IEnumerator shutOffUpgrade(PlayerShoot p)
    {
        yield return new WaitForSeconds(activeTime);
        Debug.Log("here");
        p.cooldown *= shotSpeedModifier;
        Destroy(gameObject);
    }
}
