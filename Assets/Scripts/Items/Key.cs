using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickups
{
    [SerializeField] GameObject personCollecting;
    PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = personCollecting.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Use()
    {
        audioManager.AudioKeyCollect();
        print("Player got key");
        // playerInventory.SetKey(true);
        playerInventory.itemList.Add("Key");
    }


    

}
