using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControlller : MonoBehaviour
{
    private PlayerControls playerControls;
    public Transform firePoint;
    public GameObject bulletPreFab;
    private float bulletWaitTime = 0.2f;
    private float timer = 0.0f;


    public float bulletForce = 20f;

    private void Awake()
    {
        playerControls = new PlayerControls();
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
        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
