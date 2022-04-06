using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] towers;
    private int createTowerIndex = 0;
    [SerializeField]
    private bool isCreateTower = false;
    private float unitWayPointX;
    private float unitWayPointY;
    [SerializeField]
    private GameObject towersField;

    [SerializeField]
    private GameObject costManager;

    private Button upgradeButton;

    private void Update()
    {
        //Debug.Log("key");
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isCreateTower) 
            {
                GameObject tower = Instantiate(towers[createTowerIndex], towersField.transform);
                Tower towerStatus = tower.GetComponent<Tower>();
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tower.transform.position = pos;
                isCreateTower = false;
            }
           
            //Debug.Log("key down mouse");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("key down Q");
            isCreateTower = true;
        }

    }

    public void CreateTower()
    {

    }

    public void SetUnitWayPoint(float x, float y)
    {
        unitWayPointX = x;
        unitWayPointY = y;
    }

    //public void SetUpgradeButton(Button button) { upgradeButton = button; }

    public Transform CreateTower(float x, float y, int index = 0)
    {
        createTowerIndex = index;

        
        GameObject tower = Instantiate(towers[createTowerIndex], towersField.transform);
        Tower towerStatus = tower.GetComponent<Tower>();
        towerStatus.SetUnitWayPoint(unitWayPointX, unitWayPointY);
        //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = new Vector3(x, y, 0);
        tower.transform.position = pos;

        switch (createTowerIndex)
        {
            case 0:
                costManager.GetComponent<StageUIManager>().ChangeCost(-350);
                tower.GetComponent<Tower>().default_cost = 350;
                break;
            case 1:
                costManager.GetComponent<StageUIManager>().ChangeCost(-400);
                tower.GetComponent<Tower>().default_cost = 400;
                break;
            default:
                break;
        }

        return tower.transform;
    }

}
