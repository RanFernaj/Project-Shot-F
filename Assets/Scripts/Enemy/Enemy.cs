using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum AIStates { IDLE, ATTACKING, CHASING };
public class Enemy : MonoBehaviour
{
    // Variables displayed on inspector
    public int giveEXP;
    [Header("Player elements")]
    [SerializeField] GameObject playerGO;
    [SerializeField] float enemyRange;
    [SerializeField] AudioManager audioManager;



    AIStates aiState;

    // Variables not shown on inspector
    private float distance;
    NPCController npcController;
    Rigidbody2D rb;
    PlayerController thePlayer;



    // This is for dealing damage 

    // Check for player then does damage and can kill player also does some knockback
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Manager.AddHealth(-1);
            audioManager.AudioPlayerHurt();
            Vector2 hitDirection = collision.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            thePlayer.KnockBack(hitDirection);

        }


    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        npcController = GetComponent<NPCController>();
        thePlayer = playerGO.GetComponent<PlayerController>();


    }
    // Update is called once per frame
    void Update()
    {
        AI();
    }


    // Chases player when in range
    void AI()
    {
        distance = Vector2.Distance(transform.position, playerGO.transform.position);
        Vector2 direction = playerGO.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < enemyRange)
        {
            // move towards player aka chasing
            npcController.ControlNPC(direction.x, direction.y);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            npcController.ControlNPC(0f, 0f); // Enemies stop following player when not in range
        }
    }

    // Add an attack method when close to enemy 






}
