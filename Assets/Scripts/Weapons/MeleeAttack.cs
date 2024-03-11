using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] int damage = 999;
    EnemyHealthSystem health;


    //public GameObject hitEffect; // This is for hit animation
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f); This is for the effect when bullet hits

        // Code for damage goes before the destory code
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthSystem>().ReceiveHit(damage);
        }
        

        
    }

    
}
