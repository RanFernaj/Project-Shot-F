using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] bool hasKey;

    //List used to for inventory
    public List<string> itemList = new List <string>(); 

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckInventory();
        
        // Checks if the player has the key in inventory 
        
    }

    void CheckInventory()
    {
        if (itemList.Contains("Key"))
        {
            hasKey = true;
        }
        else
        {
            hasKey = false;
        }
        
    }

    public void SetKey(bool keyBool)
    {
        hasKey = keyBool;
    }

    public bool GetHasKey(){
        return hasKey;
    }
}
