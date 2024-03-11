using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    Vector2 movement;
    Vector2 mousePos;
    bool fireWeapon;
    bool reload;
    bool first, second, thrid;
    bool melee;
    bool dashButton;
    bool grenadeButton;

    [Header("Knockback Settings")]
    [SerializeField] float knockBackForce;
    [SerializeField] float knockBackTime;
    float knockBackCounter;

    [Header("Melee Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float meleeSwingTime;
    [SerializeField] private float meleeCooldown;
    

    // All dash attributes
    private bool canDash = true;
    private bool isDasing;
    [Header("Dash Attributes")]
    [SerializeField] float dashingPower;
    [SerializeField] float dashingTime;
    [SerializeField]  float dashingCooldown;
    private bool dashUpgrade;

    float timer;

    public List<GameObject> guns = new List<GameObject>();
    [SerializeField] GameObject currentGun;
    [SerializeField] GameObject previousGun;


    [Header("References")]
    [SerializeField] Camera cam;
    Rigidbody2D rb;
    [SerializeField] GameObject fist;
    SpriteRenderer currentGunSr;
    MeleeAttack mAttack;
    SpellCooldown cooldown;

    // Animations Variables
    Animator anim;
    Animator fistAnim;
    bool attacking;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        mAttack = GetComponent<MeleeAttack>();
        anim = GetComponent<Animator>();
        fistAnim = fist.GetComponent<Animator>();
        currentGun = guns[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isDasing)
        {
            return;
        }
       
        CheckInptus();
        WeaponsSwithing();
        StartCoroutine(MeleeAttackSwing());
        AnimationControls();



    }

    private void FixedUpdate()
    {
        if (isDasing)
        {
            return;
        }
        if (knockBackCounter <= 0)
        {

            rb.velocity = new Vector2(movement.x, movement.y) * moveSpeed;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg /* - 90f */; // Used to calc the angle for the player to look at 
                                                                                    // the -90 is used as an offest to position the player accurately to the mouse   
            rb.rotation = angle;
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }


    }

    public void CheckInptus()
    {
        if (!Manager.gamePaused)
        {
            
            movement.y = Input.GetAxisRaw("Vertical");
            movement.x = Input.GetAxisRaw("Horizontal");

           
            
            

            fireWeapon = Input.GetMouseButton(0);

            reload = Input.GetKeyDown(KeyCode.R);
            first = Input.GetKeyDown(KeyCode.Alpha1);
            second = Input.GetKeyDown(KeyCode.Alpha2);
            thrid = Input.GetKeyDown(KeyCode.Alpha3);

            melee = Input.GetMouseButton(1);

            dashButton = Input.GetButtonDown("Jump");

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


            // This is done to so that player can shoot and melee at the same time depending on which weapon you have
            if (fireWeapon && currentGun == guns[1] || fireWeapon && currentGun == guns[2])
            {
                melee = false;
            }
            else
            {
                melee = Input.GetMouseButton(1);
            }

            if (dashUpgrade)
            {
                Dashing();
            }
            



        }

    }

    void AnimationControls()
    {
        if (!Manager.gamePaused)
        {
            if (movement.x != 0f || movement.y != 0f)
            {
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }

            fistAnim.SetBool("Attacking", attacking);


        }
    }
    void Dashing()
    {
        if (movement.y < 0f && dashButton && canDash)
        {
            StartCoroutine(Dash("UP"));
        }
        if (movement.y > 0f && dashButton && canDash)
        {
            StartCoroutine(Dash("DOWN"));
        }
        if (movement.x < 0f && dashButton && canDash)
        {
            StartCoroutine(Dash("LEFT"));
        }
        if (movement.x > 0f && dashButton && canDash)
        {
            StartCoroutine(Dash("RIGHT"));
        }

    }

    IEnumerator Dash(string direction)
    {
        canDash = false;
        isDasing = true;
        if (direction == "UP")
        {
            rb.velocity = new Vector2(0f, transform.localScale.y * -dashingPower); //Up
        }
        if (direction == "DOWN")
        {
            rb.velocity = new Vector2(0f, transform.localScale.y * dashingPower); //Down
        }
        if (direction == "RIGHT")
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f); //Right
        }
        if (direction == "LEFT")
        {
            rb.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f); //Left
        }
        yield return new WaitForSeconds(dashingTime);
        isDasing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void WeaponsSwithing()
    {
        if (!Manager.gamePaused)
        {
            if (first)
            {
                previousGun = currentGun;
                currentGun = guns[0];
                previousGun.SetActive(false);
                currentGun.SetActive(true);

            }
            if (second)
            {
                previousGun = currentGun;
                currentGun = guns[1];
                previousGun.SetActive(false);
                currentGun.SetActive(true);

            }
            if (thrid)
            {
                previousGun = currentGun;
                currentGun = guns[2];
                previousGun.SetActive(false);
                currentGun.SetActive(true);

            }
            
        }

    }

   

    IEnumerator MeleeAttackSwing()
    {
        currentGunSr = currentGun.GetComponent<SpriteRenderer>(); // Gets the Current guns sprites 
        if (!Manager.gamePaused)
        {
            timer += Time.deltaTime;
            if (melee && timer >= meleeCooldown)
            {
                currentGunSr.enabled = false; // Diables it 
                fist.SetActive(true);
                attacking = true;
                yield return new WaitForSeconds(meleeSwingTime);
                fist.SetActive(false);
                attacking = false;
                currentGunSr.enabled = true; // Re Enables it 
                timer = 0f;
            }
        }
    }
    public void KnockBack(Vector2 direction)
    {
        knockBackCounter = knockBackTime;
        rb.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
    }


    public bool GetFireWeapon()
    {
        return fireWeapon;
    }

    public bool GetReload()
    {
        return reload;
    }
    public bool GetFirst()
    {
        return first;
    }
    public bool GetSecond()
    {
        return second;
    }
    public bool GetThrid()
    {
        return thrid;
    }

    public void SetDash(bool dash)
    {
        dashUpgrade = dash;
    }


    public bool GetCanDash()
    {
        return canDash;
    }
    public bool GetDashDupgrade()
    {
        return dashUpgrade;
    }

    public float GetDashingCooldown()
    {
        return dashingCooldown;
    }

    public bool GetIsDashing()
    {
        return isDasing;
    }

    public bool GetGrenadeButton()
    {
        return grenadeButton;
    }
}
