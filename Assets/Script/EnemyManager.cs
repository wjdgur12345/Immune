using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private int wayCount;
    private Transform[] wayPoints;
    [SerializeField]
    private int currentIndex = 0;
    private EnemyMovement movement;
    private RectTransform hpbar;
    private GameObject canvas;
    private int hp;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private int maxHp = 100;
    public EnemyState state = EnemyState.move;
    public GameObject hpBarImage;
    public Transform target;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float attackDelayLimit;
    private float attackDelay = 0;
    [SerializeField]
    private GameObject deathAnime;
    private AudioSource deathSound;
    public AudioClip deathSoundClip;

    //temp
    private SpriteRenderer sr;
    private bool getDeathCost = true;
    private bool lifeDecrease = true;
    //wave count
    private bool waveReduce = true;

    public enum EnemyState
    {
        move,
        attack,
        death
    }

    public void Setup(Transform[] wayPoints)
    {
        movement = GetComponent<EnemyMovement>();

        wayCount = wayPoints.Length;
        this.wayPoints = new Transform[wayCount];
        this.wayPoints = wayPoints;

        sr = GetComponent<SpriteRenderer>();
        target = null;

        transform.position = new Vector3(wayPoints[currentIndex].position.x, wayPoints[currentIndex].position.y, 133);
        canvas = GameObject.Find("enemyUI");
        hpbar = Instantiate(hpBarImage, canvas.transform).GetComponent<RectTransform>();
        hp = maxHp;

        deathSound = GetComponent<AudioSource>();
        deathSound.clip = deathSoundClip;
        deathSound.loop = false;

        StartCoroutine("OnMove");
    }

    private void FixedUpdate()
    {
        
        hpbar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        hpbar.GetComponent<Slider>().value = (float)hp / (float)maxHp;
        //gameObject.GetComponent<EnemyMovement>().isMove = false;
        CheckState();
        //if(target != null) state = EnemyState.attack;

        if(currentIndex < wayPoints.Length - 1)
        {
            if (wayPoints[currentIndex].transform.position.x > wayPoints[currentIndex + 1].transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        

    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            //transform.Rotate(Vector3.forward * 10);

            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    

    private void NextMoveTo()
    {
        if(currentIndex < wayCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement.MoveTo(direction);
        }
        else
        {
            state = EnemyState.death;
            if (lifeDecrease)
            {
                GameObject StageUI = GameObject.Find("StateUI");
                StageUI.GetComponent<StageUIManager>().ChangeLife(-1);
                lifeDecrease = false;
            }
            getDeathCost = false;
        }
    }

    private void OnMouseDown()
    {
        hp -= 10;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "unit")
        {
            Unit temp = collision.GetComponent<Unit>();
            if (target == null && state == EnemyState.move && temp.state == Unit.UnitState.idle)
            {
                target = temp.transform;
                //temp.target = transform;
                //temp.targetHp = hp;
                //temp.state = Unit.UnitState.attack;
                state = EnemyState.attack;
            }
        }
    }

    public void CheckState()
    {
        if (hp <= 0)
            state = EnemyState.death;

        switch (state)
        {
            case EnemyState.move:
                //gameObject.GetComponent<EnemyMovement>().isMove = true;
                break;
            case EnemyState.attack:

                if (target != null)
                {
                    gameObject.GetComponent<EnemyMovement>().isMove = false;

                    attackDelay += Time.deltaTime;

                    if (target.GetComponent<Unit>().hp > 0)
                    {

                        if (attackDelay >= attackDelayLimit)
                        {
                            target.GetComponent<Unit>().ChangeUnitHp(-damage);
                            attackDelay = 0;
                        }

                    }
                    else
                    {
                        target = null;
                        state = EnemyState.move;
                        gameObject.GetComponent<EnemyMovement>().isMove = true;
                        Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
                        movement.MoveTo(direction);
                    }


                }
                else
                {
                    //Debug.Log("not target");
                    state = EnemyState.move;
                    gameObject.GetComponent<EnemyMovement>().isMove = true;
                    Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
                    movement.MoveTo(direction);
                }
                
                    
                break;
            case EnemyState.death:
                Color color = sr.color;
                color.a = 0;
                sr.color = color;

                transform.Find("Shadow").gameObject.SetActive(false);

                if (getDeathCost)
                {
                    deathSound.Play();
                    GameObject temp = Instantiate(deathAnime, GameObject.Find("Enemy").transform);
                    temp.transform.position = transform.position;
                    Destroy(temp, 1f);
                    GameObject StageUI = GameObject.Find("StateUI");
                    StageUI.GetComponent<StageUIManager>().ChangeCost(+15);
                    getDeathCost = false;
                }

                if (waveReduce)
                {
                    waveReduce = false;
                    GameObject waveManager = GameObject.Find("WaveManager");
                    waveManager.GetComponent<StageWaveManager>().enemy_count--;
                }

                
                hpbar.gameObject.SetActive(false);
                Destroy(hpbar.gameObject, 1f);
                Destroy(gameObject, 1f);

                break;
        }
    }

    //public void ChangeStateAttack() { state = EnemyState.attack; }

    public void ChangeHp(int n) { hp += n; }
    public int GetEnemyHp() { return hp; }

    public void SetTarget(Transform target) 
    { 
        this.target = target;
        state = EnemyState.attack;
        //movement.MoveTo(target.position);
    }
}
