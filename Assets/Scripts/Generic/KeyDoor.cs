using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{

    bool canOpen;
    GameObject personOpening;
    PlayerInventory playerInventory;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OpenDoor()
    {
        print("Open Door");
        if (canOpen)
        {
            Destroy(gameObject);
            playerInventory.itemList.Remove("Key");
        }
    }

  

    private void OnCollisionEnter2D(Collision2D other) 
    {
        personOpening = other.gameObject;
        if (personOpening.tag == "Player")
        {
            playerInventory = personOpening.GetComponent<PlayerInventory>();
            canOpen = true;
            

            if(playerInventory.GetHasKey())
            {
                OpenDoor(); 
            }
            
        }

        
    }


}
