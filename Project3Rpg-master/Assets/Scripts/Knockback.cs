using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to handle the knockback behavior of the enemy
public class Knockback : MonoBehaviour
{

    // Variable for the amount of knockback
    public float forceOfKnockback;

    // Variable for damage player does
    public float damage;

    // Checks to see if we have a collision
    // "enemy" tag is set in Unity
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            // Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            // if(enemy != null)
            // {
            //     Vector2 difference = enemy.transform.position - transform.position;
            //     difference = difference.normalized * forceOfKnockback;
            //     enemy.AddForce(difference, ForceMode2D.Impulse);
            //     //StartCoroutine(KnockCoroutine(enemy));
            // }
        }
    }

    //private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    //{
    //    if(enemy != null)
    //    {
    //        yield return new WaitForSeconds(knockBackTime);
    //        enemy.velocity = Vector2.zero;
    //        enemy.isKinematic = true;

    //    }
    //}
}
