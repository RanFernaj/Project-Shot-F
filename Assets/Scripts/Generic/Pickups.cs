using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    [SerializeField] protected string itemName;
    [SerializeField] protected AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // WHen player collides with the player 
        if (collision.tag == "Player")
        {
            Use();
            Destroy(gameObject);
        }

    }
    protected abstract void Use();
    // {
    //     // This is for the effect for the item 
    //     // Also play the sound for the item
    //     print("This is an item");
    // }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
