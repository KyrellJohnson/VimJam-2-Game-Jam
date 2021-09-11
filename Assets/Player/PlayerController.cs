using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
       

        weaponPivot = transform.Find("WeaponPivot").GetComponent<Transform>();
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

        Vector2 mousePos = playerControls.Player.MouseAim.ReadValue<Vector2>();
       
        Vector2 dir = Camera.main.ScreenToWorldPoint(mousePos) - weaponPivot.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weaponPivot.rotation = rotation;

        //Flip Player based on mouse position
        Debug.Log(dir);
        if(dir.x >= 0.0)
        {
            if(spr.flipX == true)
            {
                spr.flipX = false;
            }
        }else if(dir.x < 0f)
        {
            if (spr.flipX == false)
            {
                spr.flipX = true;
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


}
