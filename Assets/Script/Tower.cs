using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public int towerPrice;
    public int towerIndex = 0;
    public int tower_upgrade_tech1 = 0;
    public int tower_upgrade_tech2 = 0;
    public int tower_upgrade_level = 0;
    private SpriteRenderer sr;
    [SerializeField]
    private int unitLimit = 5;
    [SerializeField]
    private int currentUnit = 0;
    [SerializeField]
    private GameObject unitCreateCheckObject;
    [SerializeField]
    private GameObject[] unitsPref;
    private float createUnitCooldown = 0f;
    private bool isCreateUnit;
    [SerializeField]
    private float unitWayPointX;
    [SerializeField]
    private float unitWayPointY;
    private GameObject[] correntUnits;
    public int upgradeCost = 0;
    public int default_cost = 0;

    //upgrade
    public Sprite spr_tech1;
    public Sprite[] spr_tech1_1;
    public Sprite[] spr_tech1_2;

    public Sprite spr_tech2;
    public Sprite[] spr_tech2_1;
    public Sprite[] spr_tech2_2;

    public Sprite spr_tech3;
    public Sprite[] spr_tech3_1;
    public Sprite[] spr_tech3_2;

    //upgrade cost
    public int tech1_cost = 1;
    public int[] tech1_1_cost = new int[] { 11, 12, 13 };
    public int[] tech1_2_cost = new int[] { 21, 22, 23 };
    public int tech2_cost = 2;
    public int[] tech2_1_cost = new int[] { 11, 12, 13 };
    public int[] tech2_2_cost = new int[] { 21, 22, 23 };
    public int tech3_cost = 3;
    public int[] tech3_1_cost = new int[] { 11, 12, 13 };
    public int[] tech3_2_cost = new int[] { 21, 22, 23 };
    /*
     * 테크 1 : 유닛 선택(호중구, 호산구 등등)
     * 테크 2 : 세부 업그레이드(공격력증가, 공격속도 강화 등등)
     * 레벨 : 테크 2의 업그레이드 순서(3간계까지)
     */


    void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, 133);

        sr = GetComponent<SpriteRenderer>();
        isCreateUnit = false;
        correntUnits = new GameObject[unitLimit];
        //upgradeButtonToggle = false;

        

        /*
        switch (sr.sprite.name)
        {
            case "T1":
                towerIndex = 1;
                break;
            case "T2":
                towerIndex = 2;
                break;
            case "T3":
                towerIndex = 3;
                break;
            case "T4":
                towerIndex = 4;
                break;
            default:
                towerIndex = 0;
                break;
        }
        */
    }

    void Update()
    {

        if (!isCreateUnit)
            createUnitCooldown += Time.deltaTime;

        if (createUnitCooldown >= 3f)
        {
            createUnitCooldown = 0f;
            if (currentUnit < unitLimit)
            {
                //createUnitCheck();
                CreateUnit();
            }
        }


        
    }

    public void OnMouseDown()
    {

        /*
        if (upgradeButtonToggle) upgradeButtonToggle = false;
        else upgradeButtonToggle = true;

        if (upgradeButtonToggle)
        {
            upgradeButton.GetComponent<TowerButtonUI>().isInteractable(upgradeCost, transform.position.x - 1.5f, transform.position.y + 1.5f);
        }
        else
        {
            upgradeButton.GetComponent<TowerButtonUI>().isInteractable(upgradeCost, 100f, 100f);
        }
        */

        //upgradeTower();
    }

    public void CreateUnitCheck()
    {
        isCreateUnit = true;
        GameObject tempObject;
        tempObject = Instantiate(unitCreateCheckObject);
        tempObject.transform.SetParent(gameObject.transform);
    }

    public void CreateUnit(float x, float y)
    {
        int t = Random.Range(0, 4);
        GameObject tempObject = new GameObject();
        tempObject = Instantiate(unitsPref[t]);
        tempObject.transform.SetParent(gameObject.transform);
        //tempObject.GetComponent<Unit>().createX = x;
        //tempObject.GetComponent<Unit>().createY = y;
        currentUnit++;
        isCreateUnit = false;
    }

    public void CreateUnit()
    {
        //0-14
        //테크1 - 1 * 5

        //unitPref에서 해당 레벨에 맞는 유닛 선택
        int unit_select = 0;
        if (tower_upgrade_tech1 != 0)
        {
            unit_select = (tower_upgrade_tech1 - 1) * 5;

            if(tower_upgrade_tech2 != 0)
            {
                unit_select += 1 + (tower_upgrade_tech2 - 1) * 2;

                if(tower_upgrade_level != 0)
                {
                    unit_select++;
                }
            }
        }
            
        GameObject tempObject;
        /*
        if (towerIndex == 0)
            tempObject = unitsPref[0];
        else if (towerIndex == 1)
            tempObject = unitsPref[0];
        else
            tempObject = Resources.Load("Unit2_1") as GameObject;
        */

        tempObject = unitsPref[unit_select];
        //랜덤한 유닛
        //tempObject = Instantiate(units[t]);
        //지정생성
        correntUnits[currentUnit] = Instantiate(tempObject, transform);
        //tempObject.transform.SetParent(gameObject.transform);
        currentUnit++;
        isCreateUnit = false;
    }

    public void DestroyUnit()
    {
        currentUnit--;
    }

    public void DestroyUnit(int n)
    {
        currentUnit-=n;
    }

    public void SetUnitWayPoint(float x, float y)
    {
        Vector3 point = new Vector3(x, y, transform.position.z);

        unitWayPointX = x;
        unitWayPointY = y;
    }



    public void upgradeTower(int tech1, int tech2, int level)
    {
        tower_upgrade_tech1 = tech1;
        tower_upgrade_tech2 = tech2;
        tower_upgrade_level = level;

        if(towerIndex == 2 && tower_upgrade_tech2 == 1 && tower_upgrade_tech2 == 1)
        {
            if(tower_upgrade_level == 0)
            {
                unitLimit = 4;
            }
            else
            {
                unitLimit = 5;
            }
            correntUnits = new GameObject[unitLimit];
        }

 /*
  * 테크 1 : 유닛 선택(호중구, 호산구 등등)
  * 테크 2 : 세부 업그레이드(공격력증가, 공격속도 강화 등등)
  * 레벨 : 테크 2의 업그레이드 순서(2단계까지)
  */

        //업그레이드시 모든 자식 삭제
        for(int i=0; i<transform.childCount; i++)
            transform.GetChild(i).GetComponent<Unit>().state = Unit.UnitState.death;


        if (tower_upgrade_tech1 == 1) // 호중구, 대식세포, T세포
        {
            if(tower_upgrade_tech2 == 0)
            {
                sr.sprite = spr_tech1;
            }
            else if(tower_upgrade_tech2 == 1)
            {
                sr.sprite = spr_tech1_1[tower_upgrade_level];
            }
            else
            {
                sr.sprite = spr_tech1_2[tower_upgrade_level];
            }
        }
        else if(tower_upgrade_tech1 == 2) // 호산구, 수지상세포, B세포
        {
            if (tower_upgrade_tech2 == 0)
            {
                sr.sprite = spr_tech2;
            }
            else if (tower_upgrade_tech2 == 1)
            {
                sr.sprite = spr_tech2_1[tower_upgrade_level];
            }
            else
            {
                sr.sprite = spr_tech2_2[tower_upgrade_level];
            }
        }
        else                            //비만세포
        {
            if (tower_upgrade_tech2 == 0) 
            {
                sr.sprite = spr_tech3;
            }
            else if (tower_upgrade_tech2 == 1)
            {
                sr.sprite = spr_tech3_1[tower_upgrade_level];
            }
            else
            {
                sr.sprite = spr_tech3_2[tower_upgrade_level];
            }
        }

    }

    public void ChangeUnitSprite(Sprite spr) { unitsPref[0].GetComponent<SpriteRenderer>().sprite = spr; }

    public float GetUnitWayPointX() { return unitWayPointX; }
    public float GetUnitWayPointY() { return unitWayPointY; }

    public void SellTower()
    {
        for(int i=0; i< transform.childCount; i++)
        {
            //Destroy(transform.GetChild(i));
            transform.GetChild(i).GetComponent<Unit>().state = Unit.UnitState.death;
        }
            
        Destroy(gameObject, 1f);
    }

}
