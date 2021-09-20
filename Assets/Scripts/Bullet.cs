using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    private float timer = 0.0f;
    private Collider2D col;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //effect.transform.parent = gameObject.transform;
        Destroy(effect, 0.2f);
        Destroy(gameObject);
        Debug.Log("!!!!" + collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.name == "Enemy 1 GFX")
            {
                collision.transform.GetComponent<Enemy>().TakeDamage();
            }else if(collision.gameObject.name == "Enemy 2 GFX")
            {
                collision.transform.GetComponent<BoxEnemy>().TakeDamage();
            }
            

            


            Debug.Log("Hit Enemey");
        }
    }

    public void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 7.0f)
        {
            Destroy(gameObject);
        }


        
       
    }

}
