using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : Upgrade
{

    [SerializeField] float speedModifier = 2;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().movementSpeed *= speedModifier;
            StartCoroutine(shutOffUpgrade(collision.gameObject.GetComponent<Player>()));
        }
    }

    private IEnumerator shutOffUpgrade(Player p)
    {
        yield return new WaitForSeconds(activeTime);
        p.movementSpeed /= speedModifier;
    }
}
