using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerManager : MonoBehaviour
{
    public GameObject bullet;
    public State state = State.idle;

    public float attack_delay = 1f;
    private float attack_time = 0;

    public Transform target = null;

    public enum State
    {
        idle,
        attack
    }

    private void Update()
    {
        if(transform.parent.GetComponent<Unit>().state == Unit.UnitState.attack)
        {
            target = transform.parent.GetComponent<Unit>().target;
        }

        if(target != null)
        {
            if(target.GetComponent<EnemyManager>().GetEnemyHp() <= 0)
            {
                state = State.idle;
                target = null;
            }
            else
            {
                if(attack_time >= attack_delay)
                {
                    GameObject temp = Instantiate(bullet);
                    temp.transform.SetParent(gameObject.transform);
                    temp.GetComponent<Bullet>().target = this.target;
                    temp.GetComponent<Bullet>().moveDirection = (target.position - transform.position).normalized;
                    temp.transform.rotation = new Quaternion(0, 0, Vector3.SignedAngle(transform.up,
                        target.transform.position - temp.transform.position, -transform.forward), 0);
                    temp.transform.position = gameObject.transform.position;
                    attack_time = 0;
                    target = null;
                }
                else
                {
                    attack_time += Time.deltaTime;
                }
                
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.tag == "enemy")
        {
            if(target == null && transform.parent.GetComponent<Unit>().state != Unit.UnitState.attack) 
            {
                target = collision.transform;
            }
                
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.tag == "enemy")
        {
            if(target == null)
                target = collision.transform;
        }
        */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
       
        
    }

}
