using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapons
{
    SHOTGUN, ASSULTRIFLE, PISTOL
}
public class Weapon : MonoBehaviour
{

    [Header("Bullet Attributes")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] List<Transform> firePoints = new List<Transform>();

    [Header("Gun Settings")]
    public float bulletForce = 20f;
    public int currentAmmo;
    public Weapons weapon;
    [SerializeField] float reloadTime;
    [SerializeField] float fireRate;
    [SerializeField] int maxAmmo;
    [SerializeField] int magSize; // how many bullets in one mag
    [SerializeField] int magAmount; // How many mags player has
    bool isReloading = false;
    float nextTimeToFire = 0f;



    [Header("References")]
    PlayerController pc;
    [SerializeField] AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {

        pc = FindObjectOfType<PlayerController>();
        currentAmmo = magSize;
        


    }

    // Used so weapons arent in a reloading loop when switching to different guns
    private void OnEnable()
    {
        isReloading = false;

    }

    // Update is called once per frame
    void Update()
    {

        pc.CheckInptus();

        WeaponControls();
        maxAmmo = magSize * magAmount;


    }

    // This is for weapons 
    void WeaponControls()
    {
        if (isReloading)
        {
            return;
            
        }

        if (currentAmmo <= 0 && magAmount > 0 && maxAmmo > 0) // Checks if the player has any ammo 
        {
            StartCoroutine(Reload());
            return;
        }

        // Makes sure the max ammo doesn't go into negatives
        if (maxAmmo <= 0)
        {
            maxAmmo = 0;
        }

        if (maxAmmo >= 200)
        {
            maxAmmo = 200;
        }

        if (pc.GetReload())
        {
            StartCoroutine(Reload());
            return;
        }

        if (pc.GetFireWeapon() && Time.time >= nextTimeToFire && currentAmmo > 0)// Also makes sure that current ammo doesnt go into negatives
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            FireWeapon();
        }
        
        




    }

    // Fires weapons
    public void FireWeapon()
    {
        currentAmmo--;

        // Checks what gun the player has 
        if (weapon == Weapons.ASSULTRIFLE || weapon == Weapons.PISTOL )
        {
            GameObject shot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            audioManager.AudioGunShot();
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);


        }
        

        if (weapon == Weapons.SHOTGUN)
        {
            audioManager.AudioShotgunShot();
            // Fires at multiple fire points
            GameObject shot = Instantiate(bulletPrefab, firePoints[0].position, firePoint.rotation);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoints[0].up * bulletForce, ForceMode2D.Impulse);

            GameObject shot1 = Instantiate(bulletPrefab, firePoints[1].position, firePoint.rotation);
            Rigidbody2D rb1 = shot1.GetComponent<Rigidbody2D>();
            rb1.AddForce(firePoints[1].up * bulletForce, ForceMode2D.Impulse);

            GameObject shot2 = Instantiate(bulletPrefab, firePoints[2].position, firePoint.rotation);
            Rigidbody2D rb2 = shot2.GetComponent<Rigidbody2D>();
            rb2.AddForce(firePoints[2].up * bulletForce, ForceMode2D.Impulse);


        }




    }
    IEnumerator Reload()
    {
        isReloading = true;
        if (weapon == Weapons.SHOTGUN)
        {
            audioManager.AudioShotgunReload();

        }
        if (weapon == Weapons.ASSULTRIFLE)
        {
            audioManager.AudioARReload();

        }
        if (weapon == Weapons.PISTOL)
        {
            audioManager.AudioPistolReload();

        }
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magSize;
        maxAmmo = maxAmmo - magSize;
        magAmount--;
        isReloading = false;
        Debug.Log("Reloading Done!");
    }

    public void SetReloadTime(int decreaseTime)
    {
        reloadTime -= decreaseTime;
    }

    public float GetReloadTIme()
    {
        return reloadTime;
    }
    public void SetMagSize(int moreMags)
    {
        magSize += moreMags;
    }
    public int GetMagSize()
    {
        return magSize;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
    public void SetMagAmount(int mag)
    {
        magAmount += mag;
    }

    
}
