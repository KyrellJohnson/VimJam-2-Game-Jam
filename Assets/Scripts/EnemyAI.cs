using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 startingPosition;
    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDirection() * Random.Range(10f, 70f);
    }

    private static Vector3 GetRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, -1f), UnityEngine.Random.Range(-1f, -1f)).normalized;
    }

    private void Move()
    {

    }


}
