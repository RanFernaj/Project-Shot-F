using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCController : MonoBehaviour
{
    float hDirection;
    float vDirection;
    [SerializeField] float speed;
    Rigidbody2D rb;


    // Animation Variables 
    Animator anim;
    

    [Header("Knockback Settings")]
    [SerializeField] float knockBackForce;
    [SerializeField] float knockBackTime;
    float knockBackCounter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        NPCAnimationControls();

    }

    private void FixedUpdate()
    {
        if (knockBackCounter <= 0)
        {
            rb.velocity = new Vector2(hDirection * speed, vDirection * speed);
        }else
        {
            knockBackCounter -= Time.deltaTime;
        }
    }

    public void ControlNPC(float hInput, float vInput)
    {
        hDirection = hInput;
        vDirection = vInput;
    }


    public void KnockBack(Vector2 direction)
    {
        knockBackCounter = knockBackTime;
        rb.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
    }

    void NPCAnimationControls()
    {
        if(!Manager.gamePaused)
        {
            if(hDirection != 0f || vDirection != 0f)
            {
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }
        }
    }
}


