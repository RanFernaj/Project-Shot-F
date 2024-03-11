using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] GameObject thePlayer;
    [SerializeField] Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(thePlayer.transform.position.x + offset.x, thePlayer.transform.position.y + offset.y, transform.position.z);
    }
}
