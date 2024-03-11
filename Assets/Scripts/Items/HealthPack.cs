using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Pickups
{
    [SerializeField] private int healAmount;
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
        print("This is a health pack");
        audioManager.AudioHealthPickUp();
        Manager.AddHealth(healAmount);
        // Add sounds here 
    }

}
