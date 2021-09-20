using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 10;
    [SerializeField]
    private int attackDamage = 1;
    float playerDamage;

    [SerializeField]
    float maxDistanceCanAttack = 10;
    float distanceToPlayer;
    SpriteRenderer spr;
    private Vector2 dir;

    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;
    public Transform firePoint5;
    public Transform firePoint6;
    public Transform firePoint7;
    public Transform firePoint8;

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
    bool switchShot = false;

    [SerializeField]
    private GameObject healthDropItem;

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
            int rand = Random.Range(0, 100);
            print("RAND" + rand);
            if (rand < 33) //33%
            {
                Instantiate(healthDropItem, gameObject.transform.position, gameObject.transform.rotation);
            }
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


        Shoot();
        
        
    }



    private void Shoot()
    {
        
        timer += Time.deltaTime;
        if (timer > bulletWaitTime)
        {

            timer = 0.0f;
            if (switchShot == false)
            {
                GameObject bullet2 = Instantiate(bulletPreFab, firePoint2.position, firePoint2.rotation);
                GameObject bullet4 = Instantiate(bulletPreFab, firePoint4.position, firePoint4.rotation);
                GameObject bullet6 = Instantiate(bulletPreFab, firePoint6.position, firePoint6.rotation);
                GameObject bullet8 = Instantiate(bulletPreFab, firePoint8.position, firePoint8.rotation);

                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();
                Rigidbody2D rb6 = bullet6.GetComponent<Rigidbody2D>();
                Rigidbody2D rb8 = bullet8.GetComponent<Rigidbody2D>();

                rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);
                rb4.AddForce(firePoint4.up * bulletForce, ForceMode2D.Impulse);
                rb6.AddForce(firePoint6.up * bulletForce, ForceMode2D.Impulse);
                rb8.AddForce(firePoint8.up * bulletForce, ForceMode2D.Impulse);


                switchShot = true;

            }
            else if (switchShot == true)
            {
                GameObject bullet1 = Instantiate(bulletPreFab, firePoint1.position, firePoint1.rotation);
                GameObject bullet3 = Instantiate(bulletPreFab, firePoint3.position, firePoint3.rotation);
                GameObject bullet5 = Instantiate(bulletPreFab, firePoint5.position, firePoint5.rotation);
                GameObject bullet7 = Instantiate(bulletPreFab, firePoint7.position, firePoint7.rotation);

                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                Rigidbody2D rb5 = bullet5.GetComponent<Rigidbody2D>();
                Rigidbody2D rb7 = bullet7.GetComponent<Rigidbody2D>();

                rb1.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);
                rb3.AddForce(firePoint3.up * bulletForce, ForceMode2D.Impulse);
                rb5.AddForce(firePoint5.up * bulletForce, ForceMode2D.Impulse);
                rb7.AddForce(firePoint7.up * bulletForce, ForceMode2D.Impulse);


                switchShot = false;
            }
                //-GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
                //bullet.transform.parent = gameObject.transform;
                //-Rigidbody2D rb1 = bullet.GetComponent<Rigidbody2D>();
                //-rb1.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            
        }

        
    }

    



}
