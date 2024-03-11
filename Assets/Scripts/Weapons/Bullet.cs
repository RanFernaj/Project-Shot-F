using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int baseDamage;
    int damage;
    NPCController npcController;



    //public GameObject hitEffect; // This is for hit animation

    void Start() 
    {
        damage = baseDamage;
        


    }

    //Checks for collision
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f); This is for the effect when bullet hits

        //Checks if it hits an enemy and deals damage
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthSystem>().ReceiveHit(damage);
            npcController = collision.gameObject.GetComponent<NPCController>();
            Vector2 hitDirection = collision.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            npcController.KnockBack(hitDirection);
            Destroy(gameObject);

        }

        Destroy(gameObject);
        
    }

    
    public void SetBulletDamage(int moreDamage)
    {
        damage += moreDamage;
    }
    
    
}
