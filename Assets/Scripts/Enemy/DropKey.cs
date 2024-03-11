using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKey : MonoBehaviour
{
    [SerializeField] GameObject Key;
    [SerializeField] Transform areaToSpawnKey;
    // public List<GameObject> enemiesInArea = new List<GameObject>();
    public int numberOfEnemies;
    // Add all the enemies in the level into list 

    // everytime an enemy dies it calls this variable and remove it from the list 
    // when list count = 0 spawn in key




    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfEnemies == 0)
        {
           if(Key != null)
           {
                Key.SetActive(true);
           }
            
        }
        




    }
}
