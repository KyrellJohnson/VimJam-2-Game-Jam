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
    private int attackDamage = 1;
    private Rigidbody2D rb;
    float playerDamage;

    [SerializeField]
    float maxDistanceCanAttack = 10;
    float distanceToPlayer;
    SpriteRenderer spr;
    private Vector2 dir;

    public Transform firePoint;
    public GameObject bulletPreFab;
    public GameObject weaponPivot;
    [SerializeField]
    private float bulletWaitTime = 0.4f;
    private float timer = 0.0f;
    public float bulletForce = 35f;

    Transform Tplayer;
    public float detectRange = 6;
    public bool inRange;
    public float moveSpeed = 2f;
    Rigidbody2D rbPLAYER;


    private void Awake()
    {
        health = maxHealth;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerDamage = playerController.getAttackDamage();
        spr = GetComponent<SpriteRenderer>();
        Tplayer = GameObject.Find("Player").GetComponent<Transform>();
        detectRange *= detectRange;
        rbPLAYER = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // a little cheaper than 'distance'.. deleted the code to create a position from the player values.
        float distsqr = (Tplayer.position - transform.position).sqrMagnitude;

        if (distsqr <= detectRange && distsqr > 2)
        {
            inRange = true;
            // get a velocity based on the normalized direction, multiplied by move speed.
            Vector2 velocity = (Tplayer.transform.position - transform.position).normalized * moveSpeed;
            rbPLAYER.velocity = velocity;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Tplayer.position.x, Tplayer.position.y), moveSpeed * Time.deltaTime);
        }
    }

    public int getDamageVal()
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
        //gameObject.GetComponent<AIPath>().destination = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void getDistance()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position);
        //print(distanceToPlayer);
    }

    private void checkDistance()
    {
        if(distanceToPlayer < maxDistanceCanAttack)
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

        if (dir.x < 0.0)
        {
            if (spr.flipX == true)
            {
                spr.flipX = false;
            }
        }
        else if (dir.x >= 0f)
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
        //Debug.Log(hit.collider.tag);
        //If the collider of the object hit is not NUll
        if (hit.collider.tag == "Player" || hit.collider.tag == "Enemy")
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
            //bullet.transform.parent = gameObject.transform;
            Rigidbody2D rb1 = bullet.GetComponent<Rigidbody2D>();
            rb1.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            
        }

        
    }

    



}
