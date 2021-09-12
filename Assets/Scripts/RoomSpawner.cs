using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;


    /* Room Definitions
     * 1 --> needs bottom door
     * 2 --> needs top door
     * 3 --> need left door
     * 4 --> need right door
     */

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    public float waitTime = 4f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if(spawned == false)
        {
            switch (openingDirection)
            {
                case 1:
                    //need to spawn a room with a BOTTOM door
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 2:
                    //need to spawn a room with a TOP door
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 3:
                    //need to spawn a room with a LEFT door
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 4:
                    //need to spawn a room with a RIGHT door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
                    break;
            }

            spawned = true;
        }
            
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spawn Point") && spawned == false)
        {
            if(collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                //spawn walls blocking opening
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
