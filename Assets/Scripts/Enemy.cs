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
    private float attackRange = 10;
    [SerializeField]
    private float attackSpeed = 10;
    [SerializeField]
    private float attackDamage = 5;
    [SerializeField]
    private float speed = 10;
    float playerDamage;
    

    private void Awake()
    {
        health = maxHealth;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerDamage = playerController.getAttackDamage();
    }

    public void TakeDamage()
    {
        health = health - playerDamage;

        if(health <= 0f)
        {
            Destroy(gameObject);
        }

    }




}
