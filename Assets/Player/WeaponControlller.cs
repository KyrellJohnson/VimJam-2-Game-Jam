using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControlller : MonoBehaviour
{
    private PlayerControls playerControls;
    public Transform firePoint;
    public Transform firePointShotgun1;
    public Transform firePointShotgun2;
    public Transform firePointShotgun3;
    public GameObject bulletPreFab;
    [SerializeReference]
    private float bulletWaitTime = 0.3f;
    private float timer = 0.0f;
    public GameObject weapon_GO;
    SpriteRenderer weaponSprite;
    public GameObject[] weapons;

    public float bulletForce = 20f;

    public bool hasShotgun;
    public bool pistolEquipped; 

    private void Awake()
    {
        playerControls = new PlayerControls();
        hasShotgun = false;
        pistolEquipped = true;
        weaponSprite = weapon_GO.GetComponent<SpriteRenderer>();
        
    }

    public void Start()
    {
        playerControls.Player.SwitchWeapons.performed += _ => switchWeapons();

    }

    public void switchWeapons()
    {
        
        
        if(pistolEquipped == true)
        {
            pistolEquipped = false;
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
        }
        else
        {
            pistolEquipped = true;
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
        }
    }

    public void setHasShotgun(bool hasShotgunWeapon)
    {
        hasShotgun = hasShotgunWeapon;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void Update()
    {
        float mouse = playerControls.Player.Shooting.ReadValue<float>();
        timer += Time.deltaTime;
        if(mouse == 1 && timer > bulletWaitTime)
        {
            
            Shoot();
            timer = 0.0f;
            
        }
        //Debug.Log("Timer: " + timer + "|Visual Time: " + visualTime + "|bulletWaitTime: " + bulletWaitTime);


        
    }


    void Shoot()
    {
        if(pistolEquipped == true)
        {
            GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
            bullet.transform.parent = gameObject.transform;
            bullet.transform.Rotate(Vector3.forward * 90);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
        else if(pistolEquipped == false)
        {
            GameObject bullet_shotgun_1 = Instantiate(bulletPreFab, firePointShotgun1.position, firePointShotgun1.rotation);
            bullet_shotgun_1.transform.parent = gameObject.transform;
            bullet_shotgun_1.transform.Rotate(Vector3.forward * 90);

            Rigidbody2D rb_shotgun_1 = bullet_shotgun_1.GetComponent<Rigidbody2D>();
            rb_shotgun_1.AddForce(firePointShotgun1.up * bulletForce, ForceMode2D.Impulse);

            GameObject bullet_shotgun_2 = Instantiate(bulletPreFab, firePointShotgun2.position, firePointShotgun2.rotation);
            bullet_shotgun_2.transform.parent = gameObject.transform;
            bullet_shotgun_2.transform.Rotate(Vector3.forward * 90);

            Rigidbody2D rb_shotgun_2 = bullet_shotgun_2.GetComponent<Rigidbody2D>();
            rb_shotgun_2.AddForce(firePointShotgun2.up * bulletForce, ForceMode2D.Impulse);

            GameObject bullet_shotgun_3 = Instantiate(bulletPreFab, firePointShotgun3.position, firePointShotgun3.rotation);
            bullet_shotgun_3.transform.parent = gameObject.transform;
            bullet_shotgun_3.transform.Rotate(Vector3.forward * 90);

            Rigidbody2D rb_shotgun_3 = bullet_shotgun_3.GetComponent<Rigidbody2D>();
            rb_shotgun_3.AddForce(firePointShotgun3.up * bulletForce, ForceMode2D.Impulse);
        }
        
    }
}
