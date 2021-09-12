using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    private float timer = 0.0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //effect.transform.parent = gameObject.transform;

        Destroy(effect, 0.2f);
        Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            float enemyDamage = GetComponentInParent<Enemy>().getDamageVal();
            collision.transform.GetComponent<PlayerController>().takeDamage(enemyDamage);
             Debug.Log("Hit Player");
        }
    }

    public void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 5.0f)
        {
            Destroy(gameObject);
        }
    }

}
