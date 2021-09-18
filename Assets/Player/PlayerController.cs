using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private Transform weaponPivot;
    public WeaponControlller weaponController;
    private float horizontal;
    private float vertical;
    [SerializeField]
    private float runSpeed = 10.0f;
    float moveLimiter = 0.7f;
    [SerializeField]
    private float attackDamage = 1f;

    private BoxCollider2D col;
    private SpriteRenderer spr;

    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth = 5;
    private Animator anim;
    [SerializeField]
    private GameObject pistol;
    [SerializeField]
    private GameObject shotgun;
    SpriteRenderer weaponSpr;
    SpriteRenderer shotgunSpr;

    [SerializeField]
    private Image[] healthBar;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        health = maxHealth;
        weaponPivot = transform.Find("WeaponPivot").GetComponent<Transform>();
        weaponSpr = pistol.GetComponent<SpriteRenderer>();
        shotgunSpr = shotgun.GetComponent<SpriteRenderer>();

        playerControls.Player.Quit.performed += _ => quitGame();
    }

    void quitGame()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //read movement value
        horizontal = playerControls.Player.HorizontalMovement.ReadValue<float>();
        vertical = playerControls.Player.VerticalMovement.ReadValue<float>();

        if(horizontal != 0|| vertical != 0)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }


        Vector2 mousePos = playerControls.Player.MouseAim.ReadValue<Vector2>();
       
        Vector2 dir = Camera.main.ScreenToWorldPoint(mousePos) - weaponPivot.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weaponPivot.rotation = rotation;

        //Flip Player based on mouse position
        //Debug.Log(dir);
        if(dir.x >= 0.0)
        {
            if(spr.flipX == true)
            {
                spr.flipX = false;
            }

            if(weaponSpr.flipY == true)
            {
                weaponSpr.flipY = false;
                
            }

            if (shotgunSpr.flipY == true)
            {
                shotgunSpr.flipY = false;

            }

        }
        else if(dir.x < 0f)
        {
            if (spr.flipX == false)
            {
                spr.flipX = true;
            }

            if (weaponSpr.flipY == false)
            {
                weaponSpr.flipY = true;
            }

            if (shotgunSpr.flipY == false)
            {
                shotgunSpr.flipY = true;
            }
        }


    }

    private void FixedUpdate()
    {
        if(horizontal != 0 && vertical != 0)
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

    }

    public float getAttackDamage()
    {
        float damage = attackDamage;
        return damage;
    }

    public void healthCalc(int dmg)
    {
        health = health + dmg;
        //healthBar[0].GetComponent<CanvasGroup>().alpha = 0;

        if(health == 5)
        {
            healthBar[0].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[1].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[2].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[3].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[4].GetComponent<CanvasGroup>().alpha = 1;
        }
        else if(health == 4)
        {
            healthBar[0].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[1].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[2].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[3].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[4].GetComponent<CanvasGroup>().alpha = 1;
        }
        else if(health == 3)
        {
            healthBar[0].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[1].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[2].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[3].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[4].GetComponent<CanvasGroup>().alpha = 1;
        }
        else if (health == 2)
        {
            healthBar[0].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[1].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[2].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[3].GetComponent<CanvasGroup>().alpha = 1;
            healthBar[4].GetComponent<CanvasGroup>().alpha = 1;
        }
        else if (health == 1)
        {
            healthBar[0].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[1].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[2].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[3].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[4].GetComponent<CanvasGroup>().alpha = 1;
        }
        else if (health == 0)
        {
            healthBar[0].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[1].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[2].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[3].GetComponent<CanvasGroup>().alpha = 0;
            healthBar[4].GetComponent<CanvasGroup>().alpha = 0;
        }


        Debug.Log(health);

        if(health <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }


}
