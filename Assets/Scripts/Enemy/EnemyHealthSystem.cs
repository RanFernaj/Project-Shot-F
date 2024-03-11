using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthSystem : MonoBehaviour
{
    [Header("Health Attributes")]

    [SerializeField] int maxHP;
    [SerializeField] int currentHP;
    Enemy enemy;
    DropKey key;
    [SerializeField] AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        key = FindObjectOfType<DropKey>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
        IsDead();
        
    }

    public void ReceiveHit(int damage)
    {
        currentHP -= damage;
        audioManager.AudioZombieHurt();


    }

   
    void IsDead()
    {
        if(currentHP <= 0)
        {
            audioManager.AudioZombieDied();
            Destroy(gameObject);
            key.numberOfEnemies--;

            Manager.AddXP(enemy.giveEXP);
        }
       
    }
}
