using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCellUnit : MonoBehaviour
{
    public Transform target = null;
    public int targetHp;

    private Transform parent;
    [SerializeField]
    private float dispersionX;
    [SerializeField]
    private float dispersionY;

    [SerializeField]
    private float attackDelayLimit = 0.5f;
    private float attackDelay;
    public int damage;

    private Animator anime;
    private float wayPointX;
    private float wayPointY;

    public UnitState state = UnitState.move;

    public enum UnitState
    {
        idle,
        move,
        attack,
        death
    }

    private void Start()
    {
        parent = gameObject.transform.parent;
        //canvas = GameObject.Find("enemyUI");
        anime = GetComponent<Animator>();

        SetDispersion();
        Create();
    }

    void Update()
    {
        CheckState();
    }

    public void Create()
    {
        transform.position = parent.position;
        SetWayPoint(parent.GetComponent<Tower>().GetUnitWayPointX(),
        parent.GetComponent<Tower>().GetUnitWayPointY());
    }

    public void SetWayPoint(float x, float y)
    {
        Vector3 point = new Vector3(x, y, 0);

        wayPointX = transform.InverseTransformDirection(point).x + dispersionX;
        wayPointY = transform.InverseTransformDirection(point).y + dispersionY;
    }

    public void SetDispersion()
    {
        dispersionX = 0;
        dispersionY = 0;

        dispersionX += Random.Range(-0.2f, 0.2f);
        dispersionY += Random.Range(-0.2f, 0.2f);
    }

    public void CheckState()
    {
        switch (state)
        {
            case UnitState.idle:
                anime.SetBool("isMove", false);
                break;
            case UnitState.move:
                anime.SetBool("isMove", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(wayPointX, wayPointY, transform.position.z), 0.05f);
                anime.speed = 1;
                if (transform.position.x >= wayPointX - 0.1f &&
                    transform.position.x <= wayPointX + 0.1f &&
                    transform.position.y >= wayPointY - 0.1f &&
                    transform.position.y <= wayPointY + 0.1f)
                    state = UnitState.idle;
                break;
            case UnitState.attack:
                attackDelay += Time.deltaTime;
                transform.position = target.position;
                if (targetHp > 0)
                {
                    if (attackDelay >= attackDelayLimit)
                    {
                        attackDelay = 0;
                        target.GetComponent<EnemyManager>().ChangeHp(-damage);
                        targetHp = target.GetComponent<EnemyManager>().GetEnemyHp();
                    }
                }
                else
                {
                    target = null;
                    state = UnitState.move;
                }

                break;
            case UnitState.death:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            EnemyManager temp = collision.GetComponent<EnemyManager>();
            if (state == UnitState.idle && target == null && temp.GetEnemyHp() > 0)
            {
                target = temp.transform;
                //Debug.Log("attacked");
                state = UnitState.attack;
                //temp.SetTarget(transform);
                targetHp = temp.GetEnemyHp();


            }
        }
    }
}