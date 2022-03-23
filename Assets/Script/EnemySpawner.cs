using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab0;
    [SerializeField]
    private GameObject enemyPrefab1;
    [SerializeField]
    private GameObject enemyPrefab2;
    [SerializeField]
    private float spawnTime;
    public int way_point_count = 1;
    public int spawn_way_count = 1;
    public Transform[] wayPoints1;
    public Transform[] wayPoints2;
    public Transform[] wayPoints3;
    public Transform[] wayPoints4;
    [SerializeField]
    private GameObject enemyField;
    private int countEnemy;
    public int maxCountEnemy;

    private GameObject[] enemys;
    public int[] enemysIndex;


    private void Awake()
    {
        
        
    }

    public void EnemySpawn(int[] arr, int maxCount)
    {
        maxCountEnemy = maxCount;
        enemysIndex = arr;
        countEnemy = 0;
        enemys = new GameObject[maxCountEnemy];
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (countEnemy < maxCountEnemy)
        {
            //GameObject clone = Instantiate(enemyPrefab, enemyField.transform);
            GameObject temp;
            if (enemysIndex[countEnemy] == 0)
                temp = Resources.Load("EnermyTemp01") as GameObject;
            else if (enemysIndex[countEnemy] == 1)
                temp = Resources.Load("EnermyTemp02") as GameObject;
            else
                temp = Resources.Load("EnermyTemp03") as GameObject;
            enemys[countEnemy] = Instantiate(temp, enemyField.transform);
            //enemys[countEnemy].GetComponent<EnemyManager>().Setup(wayPoints1);

            if (spawn_way_count == 1)
                enemys[countEnemy].GetComponent<EnemyManager>().Setup(wayPoints1);
            else if (spawn_way_count == 2)
                enemys[countEnemy].GetComponent<EnemyManager>().Setup(wayPoints2);
            else if (spawn_way_count == 3)
                enemys[countEnemy].GetComponent<EnemyManager>().Setup(wayPoints3);
            else if (spawn_way_count == 4)
                enemys[countEnemy].GetComponent<EnemyManager>().Setup(wayPoints4);

            spawn_way_count++;
            if (spawn_way_count > way_point_count)
                spawn_way_count = 1;

            countEnemy++;

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void EnemySpawn1()
    {
        GameObject temp = Resources.Load("EnermyTemp01") as GameObject;
        GameObject enemyTemp = Instantiate(temp, enemyField.transform);
        if(spawn_way_count == 1)
            enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints1);
        else if(spawn_way_count == 2)
            enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints2);
        else if (spawn_way_count == 3)
            enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints3);
        else if (spawn_way_count == 4)
            enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints4);

        spawn_way_count++;
        if (spawn_way_count > way_point_count)
            spawn_way_count = 1;
    }

    

    public void EnemySpawn2()
    {
        GameObject temp = Resources.Load("EnermyTemp02") as GameObject;
        GameObject enemyTemp = Instantiate(temp, enemyField.transform);
        //enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints1);

        if (spawn_way_count == 1)
            enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints1);
        else if (spawn_way_count == 2)
            enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints2);
        else if (spawn_way_count == 3)
            enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints3);
        else if (spawn_way_count == 4)
            enemyTemp.GetComponent<EnemyManager>().Setup(wayPoints4);

        spawn_way_count++;
        if (spawn_way_count > way_point_count)
            spawn_way_count = 1;
    }
}
