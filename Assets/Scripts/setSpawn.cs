using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    private GameObject spawnPoint;
    private Vector2 spawnLoc;
    private Transform playerLoc;
    // Start is called before the first frame update
    private void Start()
    {
        spawnPoint = GameObject.Find("StartingPoint");
        spawnLoc = (spawnPoint.transform.position);
        player.transform.position = spawnLoc;
        getNumberOfEnemies();
    }


    public void getNumberOfEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        print(enemies.Length);
    }



}
