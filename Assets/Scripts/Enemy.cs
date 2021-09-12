using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 10;

    [SerializeField]
    private float attackDamage = 3;

    float playerDamage;
    float maxDistanceToFollowPlayer = 25;
    float distanceToPlayer;
    SpriteRenderer spr;
    private Vector2 dir;

    public Transform firePoint;
    public GameObject bulletPreFab;
    [SerializeField]
    private float bulletWaitTime = 0.4f;
    private float timer = 0.0f;
    public float bulletForce = 35f;
    private void Awake()
    {
        health = maxHealth;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerDamage = playerController.getAttackDamage();
        spr = GetComponent<SpriteRenderer>();
    }

    public float getDamageVal()
    {
        return attackDamage;
    }

    public void TakeDamage()
    {
        health = health - playerDamage;

        if(health <= 0f)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

    }

    public void FixedUpdate()
    {
        getDistance();
        checkDistance();
        
    }

    public void getDistance()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position);
        //print(distanceToPlayer);
    }

    private void checkDistance()
    {
        if(gameObject.transform.parent.GetComponent<AIPath>().enabled == true && distanceToPlayer >= maxDistanceToFollowPlayer)
        {
            gameObject.transform.parent.GetComponent<AIPath>().enabled = false;
        }else if(gameObject.transform.parent.GetComponent<AIPath>().enabled == false && distanceToPlayer < maxDistanceToFollowPlayer)
        {
            gameObject.transform.parent.GetComponent<AIPath>().enabled = true;
        }

        if(distanceToPlayer < 20)
        {
            trackPlayer();
            attackPlayer();
        }
    }

    private void trackPlayer()
    {

        Transform weaponPivot = transform.Find("WeaponPivot").GetComponent<Transform>();



        dir = GameObject.Find("Player").transform.position - weaponPivot.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weaponPivot.rotation = rotation;

        if (dir.x >= 0.0)
        {
            if (spr.flipX == true)
            {
                spr.flipX = false;
            }
        }
        else if (dir.x < 0f)
        {
            if (spr.flipX == false)
            {
                spr.flipX = true;
            }
        }

    }

    private void attackPlayer()
    {


        int layerMask = LayerMask.GetMask("Player");
        //Get the first object hit by the ray
        RaycastHit2D hit = Physics2D.Raycast( firePoint.position, dir, layerMask);
        
        //If the collider of the object hit is not NUll
        if (hit.collider.tag == "Player")
        {
            Shoot();
        }
        
        
    }



    private void Shoot()
    {
        
        timer += Time.deltaTime;
        if (timer > bulletWaitTime)
        {
            
            timer = 0.0f;
            GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
            bullet.transform.parent = gameObject.transform;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            
        }
    }

 

}
