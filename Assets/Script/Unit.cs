using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private Transform parent;
    [SerializeField]
    private float range = 2.5f;
    [SerializeField]
    private float wayPointX;
    private float wayPointY;
    public UnitState state = UnitState.move;
    [SerializeField]
    private float dispersionX;
    [SerializeField]
    private float dispersionY;
    private CircleCollider2D circleCollider;
    public Transform target;
    [SerializeField]
    private float attackDelayLimit = 0.5f;
    private float attackDelay;
    private GameObject canvas;
    public GameObject hpBarImage;
    private RectTransform hpBar;
    public int hp;
    [SerializeField]
    private int maxHp;
    public int damage;

    private SpriteRenderer sr;
    [SerializeField]
    private GameObject deathAnime;

    private Animator anime;

    //temp
    [SerializeField]
    public int targetHp;
    private bool isDeath = true;

    //자동회복
    private float regen_hp = 15.0f;
    private float regen_time = 0.0f;
    private float regen_time_limit = 0.5f;

    //대식세포 관련 변수
    [SerializeField]
    private int death_count = 0;
    public int death_count_limit = 5;
    public GameObject poison_smoke;
    //////////
    

    public enum UnitState
    {
        idle,
        move,
        attack,
        death
    }

    void Start()
    {
        parent = gameObject.transform.parent;
        //transform.position = new Vector3(createX, createY, parent.GetComponent<Tower>().transform.position.z);
        circleCollider = GetComponent<CircleCollider2D>();
        target = null;
        targetHp = 0;
        attackDelay = 0;
        hp = maxHp;
        canvas = GameObject.Find("enemyUI");
        hpBar = Instantiate(hpBarImage, canvas.transform).GetComponent<RectTransform>();

        sr = GetComponent<SpriteRenderer>();

        anime = GetComponent<Animator>();
        

        SetDispersion();
        Create();
        
    }

    private void FixedUpdate()
    {
        if(state == UnitState.idle || state == UnitState.move)
        {
            if(regen_time >= regen_time_limit)
            {
                regen_time = 0;
                if (hp < maxHp - regen_hp)
                    hp += (int)regen_hp;
                else if (hp < maxHp && (hp + regen_hp) >= maxHp)
                    hp = maxHp;
            }
            else
            {
                regen_time += Time.deltaTime;
            }
        }

        hpBar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        hpBar.GetComponent<Slider>().value = (float)hp / (float)maxHp;
        CheckState();
        if (hp <= 0)
            state = UnitState.death;
    }

    public void SetWayPoint(float x, float y)
    {
        Vector3 point = new Vector3(x, y, 0);

        wayPointX = transform.InverseTransformDirection(point).x + dispersionX;
        wayPointY = transform.InverseTransformDirection(point).y + dispersionY;
    }

    public void RandomCreate()
    {
        transform.position = parent.position;
        transform.position += new Vector3(
            Random.Range(range * -1, range),
            Random.Range(range * -1, range),
            parent.transform.position.z
            );
    }

    public void Create() 
    {
        transform.position = parent.position;
        SetWayPoint(    parent.GetComponent<Tower>().GetUnitWayPointX(),
                        parent.GetComponent<Tower>().GetUnitWayPointY()     );
    }

    public void OnMouseDown()
    {
        //Destroy(gameObject);
    }
    private void OnDestroy()
    {
        parent.GetComponent<Tower>().DestroyUnit();
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
        Color color;
        switch (state)
        {
            case UnitState.idle:
                anime.SetBool("isMove", false);
                attackDelay = 0;
                //anime.speed = 0;
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
                if(targetHp > 0)
                {
                    if(attackDelay >= attackDelayLimit)
                    {
                        attackDelay = 0;
                        target.GetComponent<EnemyManager>().ChangeHp(-damage);
                        targetHp = target.GetComponent<EnemyManager>().GetEnemyHp();


                        //대식세포 킬카운트
                        if(transform.parent.gameObject.GetComponent<Tower>().towerIndex == 1 &&
                            transform.parent.gameObject.GetComponent<Tower>().tower_upgrade_tech1 == 1)
                        {
                            if (targetHp <= 0)
                            {
                                death_count++;
                                if (death_count >= death_count_limit)
                                {
                                    state = UnitState.death;
                                }
                            }
                        }
                        //////////////
                    }   
                }
                else
                {
                    target = null;
                    state = UnitState.idle;
                }
                
                break;
            case UnitState.death:
                if(target != null)
                    target.GetComponent<EnemyManager>().target = null;

                if (isDeath)
                {
                    isDeath = false;
                    Instantiate(deathAnime, gameObject.transform);

                    //대식세포 2-2상태에서 독구름생성
                    if (transform.parent.gameObject.GetComponent<Tower>().towerIndex == 1 &&
                        transform.parent.gameObject.GetComponent<Tower>().tower_upgrade_tech1 == 1 &&
                        transform.parent.gameObject.GetComponent<Tower>().tower_upgrade_tech2 == 2 &&
                        transform.parent.gameObject.GetComponent<Tower>().tower_upgrade_level == 1)
                    {
                        poison_smoke.transform.position = transform.position;
                        GameObject temp = Instantiate(poison_smoke);
                        temp.transform.SetParent(GameObject.Find("Units").transform);
                    }
                    ////////
                }


                

                color = sr.color;
                color.a = 0;
                sr.color = color;

                transform.Find("Shadow").gameObject.SetActive(false);
                Destroy(gameObject, 1f);
                hpBar.gameObject.SetActive(false);
                Destroy(hpBar.gameObject, 1f);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        //Debug.Log(collision.name);
        if (collision.tag == "enemy")
        {
            EnemyManager temp = collision.GetComponent<EnemyManager>();
            if (state == UnitState.idle && target == null && temp.GetEnemyHp() > 0)
            {
                
                //if (temp.GetComponent<EnemyManager>().state == EnemyManager.EnemyState.move)
                
                target = temp.transform;
                //Debug.Log("attacked");
                state = UnitState.attack;
                //temp.SetTarget(transform);
                targetHp = temp.GetEnemyHp();
                
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.tag == "enemy" && target != null)
        {
            if(target.gameObject == collision.gameObject)
                target = null;
        }
        */
    }

    public void ChangeUnitHp(int n) { hp += n; }

}
