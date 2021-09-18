using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField]
    private float
        maxHealth = 10,
        maxDistanceCanAttack = 10,
        bulletWaitTime = 0.4f,
        bulletForce = 35f,
        detectRange = 6,
        moveSpeed = 2f;

    [SerializeField]
    private int attackDamage = 1;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject
        bulletPreFab,
        weaponPivot;

    private float health;
    private float timer = 0.0f;
    private float playerDamage;
    private float distanceToPlayer;

    private Vector2 dir;

    private SpriteRenderer spr;
    private Transform playerTransform;
    private Rigidbody2D rb_Player;

    private void Awake()
    {
        //set health equal to max health
        health = maxHealth;

        //Get instance of player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();

        //get player damage
        playerDamage = playerController.getAttackDamage();

        //get boss sprite
        spr = GetComponent<SpriteRenderer>();

        //player Transform
        playerTransform = player.transform;

        //detect range
        detectRange *= detectRange;

        //rigid body of player
        rb_Player = player.GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        float distsqr = (playerTransform.position - transform.position).sqrMagnitude;

        if(distsqr <= detectRange && distsqr > 10)
        {
            Vector2 velocity = (playerTransform.transform.position - transform.position).normalized * moveSpeed;

            rb_Player.velocity = velocity;

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, playerTransform.position.y), moveSpeed * Time.deltaTime);

        }

        flipBoss();
    }

    void flipBoss()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position);

        if(distanceToPlayer < maxDistanceCanAttack)
        {
            shouldFlip();
            attackPlayer();
        }

    }

    void shouldFlip()
    {
        Transform weaponPivotTransfrom = weaponPivot.GetComponent<Transform>();

        dir = GameObject.Find("Player").transform.position - weaponPivotTransfrom.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        weaponPivotTransfrom.rotation = rotation;

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

    void attackPlayer()
    {
        //get layer to attack
        int layerMask = LayerMask.GetMask("Player");

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir, layerMask);
        print(hit.collider.tag);
        if (hit.collider.tag == "Player" || hit.collider.tag == "Enemy Projectile" || hit.collider.tag == "Boss")
        {
            Shoot();
        }


    }

    void Shoot()
    {
        timer += Time.deltaTime;
        if(timer > bulletWaitTime)
        {
            //reset timer
            timer = 0.0f;

            GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb1 = bullet.GetComponent<Rigidbody2D>();
            rb1.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }

}
