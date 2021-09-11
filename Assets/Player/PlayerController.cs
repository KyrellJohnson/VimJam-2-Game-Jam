using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private Transform weaponPivot;

    private BoxCollider2D col;
    [SerializeField] private float speed;
    
    

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //read movement value
        Vector2 movementInput = playerControls.Player.Movement.ReadValue<Vector2>();

        Vector2 mousePos = playerControls.Player.MouseAim.ReadValue<Vector2>();
        //--
        Vector2 dir = Camera.main.ScreenToWorldPoint(mousePos) - weaponPivot.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weaponPivot.rotation = rotation;
        
        //--
        //weaponPivot.transform.Rotate(mousePos.x, mousePos.y, 0);
        Debug.Log(angle);


        //get current location
        Vector2 currentPos = transform.position;
        currentPos += movementInput* speed * Time.deltaTime;
        transform.position = currentPos;




    }
}
