using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickups
{
    PlayerController pc;
    Weapon weapon;
    [SerializeField] private int magAmountToGive;
    [SerializeField] GameObject player;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Use()
    {
        audioManager.AudioPistolReload();
        pc = player.GetComponent<PlayerController>();
        foreach (GameObject g in pc.guns)
        {
            Weapon gun = g.GetComponent<Weapon>();
            gun.SetMagAmount(magAmountToGive); 
        }
    }
}
