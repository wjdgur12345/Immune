using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubObject : MonoBehaviour
{
    private float timer = 0f;
    private bool get_dagame = false;

    private void Start()
    {
        transform.position = transform.parent.position;
        GetComponent<CircleCollider2D>().radius = transform.parent.GetComponent<CircleCollider2D>().radius;
        GetComponent<CircleCollider2D>().offset = transform.parent.GetComponent<CircleCollider2D>().offset;
    }

    private void Update()
    {
        if (get_dagame)
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "aoe")
        {
            get_dagame = true;
            if(collision.GetComponent<PoisonSmoke>().damage_tick_limit <= timer)
            {
                transform.parent.GetComponent<EnemyManager>().ChangeHp(collision.GetComponent<PoisonSmoke>().damage * -1);
                timer = 0f;
                //Debug.Log("poison damage");
            }
        }
        else
        {
            //get_dagame = false;
        }
    }
}
