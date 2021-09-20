using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject
        toolTip;

    [SerializeField]
    private string popTXT;
      
    private bool hasKey;

    [SerializeField]
    CinemachineVirtualCamera doorCam;
        

    CinemachineVirtualCamera playerCam;
        

    private bool playerCamON = true;

    //private PlayerControls playerControls;
    PopUpSystem pop;

    Animator doorAnim;

    private void Awake()
    {
        toolTip.SetActive(false);
        hasKey = false;
        doorAnim = gameObject.GetComponentInParent<Animator>();
        playerCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();

        playerCam.Priority = 1;
        doorCam.Priority = 0;
        doorCam.transform.position = new Vector3(doorCam.transform.position.x, doorCam.transform.position.y, -100f);
        //playerControls = new PlayerControls();
        //playerControls.Player.Interact.performed += _ => ShowPopup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasKey == false)
        {
            //toolTip.SetActive(true);
            pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();

            pop.PopUp(popTXT);
            //setDoorTrigger();
        }

        if(hasKey)
        {
            levelLoader lvl = GameObject.Find("GameManager").GetComponent<levelLoader>();
            lvl.LoadNextLevel();
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        toolTip.SetActive(false);
        if(hasKey == false)
        {
            pop.PopUpSetTrigger("close");

        }


    }

    private void LateUpdate()
    {
        if (hasKey == false)
        {
            GameObject[] enemies = GameObject.Find("GameManager").GetComponent<setSpawn>().getNumberOfEnemies();
            if (enemies.Length == 0)
            {
                hasKey = true;
                SwitchPriorityToDoor();
                StartCoroutine(SwitchToDoorCamWait());
                StartCoroutine(SwitchBackToPlayer());
            }
        }
    }

    IEnumerator SwitchBackToPlayer()
    {
        yield return new WaitForSeconds(3);

        SwitchPriorityToPlayer();
    }
    IEnumerator SwitchToDoorCamWait()
    {
        setDoorTrigger();
        yield return new WaitForSeconds(3);

    }


    public void setHasKey(bool key)
    {
        hasKey = key;
    }

    public bool getHasKey()
    {
        return hasKey;
    }

    public void setDoorTrigger()
    {
        doorAnim.SetTrigger("openDoor");
    }

    private void SwitchPriorityToDoor()
    {
        playerCam.Priority = 0;
        doorCam.Priority = 1;
    }

    private void SwitchPriorityToPlayer()
    {
        playerCam.Priority = 1;
        doorCam.Priority = 0;
    }



}
