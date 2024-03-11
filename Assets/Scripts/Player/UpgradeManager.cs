using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Upgrades variables")]
    [SerializeField] int healthIncrease = 10;
    [SerializeField] int extraDamage = 5;
    [SerializeField] int reloadReduce = 5;
    [SerializeField] int increaseMagSize = 5;

    // References
    Bullet bullet;
    PlayerController pc;

    Weapon weapon;

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] bullets;

    public void GainDash()
    {
        print("You got a dash ability");
        pc = player.GetComponent<PlayerController>();
        pc.SetDash(true);
    }


    public void MoreAmmo()
    {
        // Take the weapon script and change the max ammo
        print("more Ammor gained, Your weapons magazine is bigger");
        pc = player.GetComponent<PlayerController>();
        foreach (GameObject g in pc.guns)
        {
            Weapon gun = g.GetComponent<Weapon>();
            gun.SetMagSize(increaseMagSize);
        }

    }

    public void MoreBulletDamage()
    {
        // Take the damage from the bullet script and increase it
        print(" MORE DAMAGE!!!!!!");
        foreach (GameObject g in bullets)
        {
            bullet = g.GetComponent<Bullet>();
            bullet.SetBulletDamage(extraDamage);
        }

    }
    public void MoreHealth()
    {
        // Take the health script from the player and increase the max health
        print("More health");
        Manager.SetMaxHealth(healthIncrease);
    }
    public void ReloadTimeReduced()
    {
        // Take the reload times for the weapon script and reduce it (For all guns)
        print("Less waiting more shooting!");
        pc = player.GetComponent<PlayerController>();
        foreach (GameObject g in pc.guns)
        {
            Weapon gun = g.GetComponent<Weapon>();
            gun.SetReloadTime(reloadReduce);
        }

    }
   



}
