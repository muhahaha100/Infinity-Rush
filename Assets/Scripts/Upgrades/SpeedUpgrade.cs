using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : Upgrade
{

    [SerializeField] float speedModifier = 2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().movementSpeed *= speedModifier;
            StartCoroutine(shutOffUpgrade(collision.gameObject.GetComponent<Player>()));
            active = false;
            this.enabled = false;
            spriteRenderer.enabled = false;
            audioSource.Play();
        }
    }

    private IEnumerator shutOffUpgrade(Player p)
    {
        yield return new WaitForSeconds(activeTime);
        Debug.Log("here");
        p.movementSpeed /= speedModifier;
        Destroy(gameObject);

        yield return null;
    }
}
