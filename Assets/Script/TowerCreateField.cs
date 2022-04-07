using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCreateField : MonoBehaviour
{
    [SerializeField]
    private float spawnPointX;
    [SerializeField]
    private float spawnPointY;
    //private int towerIndex = 0;
    [SerializeField]
    private TowerSpawner spawner;
    [SerializeField]
    private Button[] TowerCreateButton;
    
    private bool isToggle;
    private bool isTowerCreate;
    private Transform tower;

    public Button[] upgradeButton;

    public Button sellButton;

    [SerializeField]
    private int upgrade_tech1 = 0;
    [SerializeField]
    private int upgrade_tech2 = 0;
    [SerializeField]
    private int upgrade_Level = 0;
    [SerializeField]
    public int upgrade_cost = 0;
    

    private void Start()
    {
        /*spawner = GetComponentInParent<TowerSpawner>();*/
        isToggle = false;
        isTowerCreate = false;
        tower = null;
    }

    private void Update()
    {
        if (upgrade_cost == 0 && tower != null)
            upgrade_cost += tower.GetComponent<Tower>().default_cost;
    }

    public void OnToggle(bool b = true)
    {
        isToggle = b;

        if (isTowerCreate)
        {
            //업그레이드
            if (isToggle)
            {
                if(upgrade_tech1 == 0)
                {
                    upgradeButton[0].GetComponent<TowerButtonUI>().isInteractable(350, transform.position.x - 1.5f, transform.position.y + 1.5f);
                    upgradeButton[1].GetComponent<TowerButtonUI>().isInteractable(350, transform.position.x + 1.5f, transform.position.y + 1.5f);
                    if(tower != null && tower.GetComponent<Tower>().towerIndex == 0)
                        upgradeButton[2].GetComponent<TowerButtonUI>().isInteractable(350, transform.position.x - 1.5f, transform.position.y - 1.5f);
                }
                else if(upgrade_tech2 == 0)
                {
                    upgradeButton[0].GetComponent<TowerButtonUI>().isInteractable(350, transform.position.x - 1.5f, transform.position.y + 1.5f);
                    upgradeButton[1].GetComponent<TowerButtonUI>().isInteractable(350, transform.position.x + 1.5f, transform.position.y + 1.5f);
                }
                else
                {
                    upgradeButton[upgrade_tech2 - 1].GetComponent<TowerButtonUI>().isInteractable(350, transform.position.x - 1.5f, transform.position.y + 1.5f);
                }
                
                sellButton.GetComponent<TowerButtonUI>().isInteractable(0, transform.position.x + 1.5f, transform.position.y - 1.5f);

                

                if(upgrade_Level >= 1)
                {
                    upgradeButton[upgrade_tech2 - 1].GetComponent<TowerButtonUI>().isInteractable(false, true);
                }
            }
            else
            {
                upgradeButton[0].GetComponent<TowerButtonUI>().isInteractable(350, 100f, 100f);
                upgradeButton[1].GetComponent<TowerButtonUI>().isInteractable(350, 100f, 100f);
                upgradeButton[2].GetComponent<TowerButtonUI>().isInteractable(350, 100f, 100f);
                sellButton.GetComponent<TowerButtonUI>().isInteractable(0, 100f, 100f);
            }

            
        }
        else
        {
            //타워 생성
            if (isToggle)
            {
                TowerCreateButton[0].GetComponent<TowerButtonUI>().isInteractable(350, transform.position.x - 1.5f, transform.position.y + 1.5f);
                TowerCreateButton[1].GetComponent<TowerButtonUI>().isInteractable(400, transform.position.x + 1.5f, transform.position.y + 1.5f);
                TowerCreateButton[2].GetComponent<TowerButtonUI>().isInteractable(450, transform.position.x - 1.5f, transform.position.y - 1.5f);
            }
            else
            {
                TowerCreateButton[0].GetComponent<TowerButtonUI>().isInteractable(350, 100f, 100f);
                TowerCreateButton[1].GetComponent<TowerButtonUI>().isInteractable(400, 100f, 100f);
                TowerCreateButton[2].GetComponent<TowerButtonUI>().isInteractable(400, 100f, 100f);
            }
        }
        
    }

    public void CreateTower()
    {
        spawner.GetComponent<TowerSpawner>().SetUnitWayPoint(spawnPointX, spawnPointY);
        //spawner.GetComponent<TowerSpawner>().SetUpgradeButton(upgradeButton1);
        spawner.GetComponent<TowerSpawner>().CreateTower(transform.position.x, transform.position.y);
        for(int i=0; i< TowerCreateButton.Length; i++)
        {
            TowerCreateButton[i].GetComponent<TowerButtonUI>().isInteractable(false, false);
        }
        //Destroy(this);
    }

    public void CreateTower(int index)
    {
        spawner.GetComponent<TowerSpawner>().SetUnitWayPoint(spawnPointX, spawnPointY);
        tower = spawner.GetComponent<TowerSpawner>().CreateTower(transform.position.x, transform.position.y, index);
        for (int i = 0; i < TowerCreateButton.Length; i++)
        {
            TowerCreateButton[i].GetComponent<TowerButtonUI>().isInteractable(false, false);
        }


        isTowerCreate = true;
        isToggle = false;
        upgrade_Level = 0;
    }

    /// <summary>
    /// ///////////////////
    /// </summary>
    public void upgradeTower1()
    {
        if (upgrade_tech1 == 0)
        {
            upgrade_tech1 = 1;
        }
        else 
        {
            if (upgrade_tech2 == 0)
            {
                upgrade_tech2 = 1;
            }
            else if(upgrade_Level < 2)
            {
                upgrade_Level++;
            }
        }
        tower.GetComponent<Tower>().upgradeTower(upgrade_tech1, upgrade_tech2, upgrade_Level);
        //Debug.Log("tech 1 : " + upgrade_tech1 + ", tech2 : " + upgrade_tech2 + "\nLevel : " + upgrade_Level);
        OnToggle(false);
    }

    public void upgradeTower2()
    {
        if (upgrade_tech1 == 0)
        {
            upgrade_tech1 = 2;
        }
        else
        {
            if (upgrade_tech2 == 0)
            {
                upgrade_tech2 = 2;
            }
            else if (upgrade_Level < 2)
            {
                upgrade_Level++;
            }
        }
        tower.GetComponent<Tower>().upgradeTower(upgrade_tech1, upgrade_tech2, upgrade_Level);
        //Debug.Log("tech 1 : " + upgrade_tech1 + ", tech2 : " + upgrade_tech2 + "\nLevel : " + upgrade_Level);
        OnToggle(false);
    }

    public void upgradeTower3()
    {
        if (upgrade_tech1 == 0)
        {
            upgrade_tech1 = 3;
        }
        else
        {
            if (upgrade_tech2 == 0)
            {
                upgrade_tech2 = 1;
            }
            else if (upgrade_Level < 2)
            {
                upgrade_Level++;
            }
        }
        tower.GetComponent<Tower>().upgradeTower(upgrade_tech1, upgrade_tech2, upgrade_Level);
        //Debug.Log("tech 1 : " + upgrade_tech1 + ", tech2 : " + upgrade_tech2 + "\nLevel : " + upgrade_Level);
        OnToggle(false);
    }

    public void SellTower()
    {
        if(tower != null)
        {
            upgrade_Level = 0;
            upgrade_tech1 = 0;
            upgrade_tech2 = 0;
            tower.GetComponent<Tower>().SellTower();
            tower = null;
            OnToggle(false);
            isTowerCreate = false;
            GameObject StageUI = GameObject.Find("StateUI");
            StageUI.GetComponent<StageUIManager>().ChangeCost(upgrade_cost);
            upgrade_cost = 0;
        }
    }

}
