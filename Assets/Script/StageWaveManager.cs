using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageWaveManager : MonoBehaviour
{
    public FieldState field_state = FieldState.idle;

    //wave
    public int wave_count = 0;
    public int max_wave_count;

    //enemy
    public int max_enemy_count;
    public int enemy_count;

    public GameObject enemy_spawner;

    public Button wave_button;

    public int[][] enemy_array;

    public GameObject start_button_loaging_bar;

    public enum FieldState 
    {
        idle,
        play
    }

    private void Start()
    {
        start_button_loaging_bar.GetComponent<Image>().fillAmount = 0;


        //웨이브에 등장하는 적 유닛들
        switch(SceneManager.GetActiveScene().name)
        {
            case "Stage1-1":
                max_wave_count = 5;
                enemy_array = new int[6][]
                {
                new int[] {},
                new int[] { 2,2,2,2,2},
                new int[] { 2,2,2,0,0,0},
                new int[] { 2,2,2,0,0,0,1,1},
                new int[] { 2,2,2,0,0,0,1,1},
                new int[] { 2,2,2,2,0,0,0,0,1,1,1,1}
                };
                break;
            case "Stage1-2":
                max_wave_count = 7;
                enemy_array = new int[8][]
                {
                new int[] {},
                new int[] { 1,1,0,0,2,2},
                new int[] { 0,0,1,1,2,2,1,1,1},
                new int[] { 1,1,2,2,2,0,0,0,0,0},
                new int[] { 0,2,0,2,0,2,1,1,1,1,1},
                new int[] { 1,1,0,2,1,1,0,2,1,1,0,2,2},
                new int[] { 2,2,2,1,1,1,1,1,1,1,0,0,0,0},
                new int[] { 0,1,1,2,0,1,1,2,0,1,1,2,0,1,1,2}
                };
                break;
            case "Stage1-3":
                max_wave_count = 8;
                enemy_array = new int[9][]
                {
                new int[] {},
                new int[] {0,1,2,1,1,0,0,0},
                new int[] {2,2,0,0,0,1,1,1,1},
                new int[] {1,1,1,1,1,0,0,0,2,2},
                new int[] {0,2,0,2,0,2,1,1,1,1,1},
                new int[] {0,1,1,2,0,1,1,2,0,1,1,2,2},
                new int[] {1,1,1,0,0,2,2,1,1,1,0,0,2,2},
                new int[] {2,2,2,2,2,0,0,0,0,0,1,1,1,1,1,1,1,1},
                new int[] {1,1,1,1,0,0,0,2,2,2,1,1,1,1,0,0,0,2,2,2},
                };
                break;
            case "Stage1-4":
                max_wave_count = 10;
                enemy_array = new int[11][]
                {
                new int[] {},
                new int[] {2,2,2,1,1,0,0,0},
                new int[] {2,2,2,2,0,0,0,0},
                new int[] {2,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                new int[] {0,0,0,0,0,0,2,2,2,2,2},
                new int[] {0,0,0,2,2,2,2,2,1,1,1,1,1,1,1,1},
                new int[] {1,1,1,1,1,1,1,1,1,0,0,0,0,0,2,2,2,2,2},
                new int[] {1,1,1,1,0,0,2,2,1,1,1,1,0,0,2,2},
                new int[] {2,2,2,2,0,0,0,0,0,2,2,2,0,0,0,0,2,0},
                new int[] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},//boss
                new int[] {1,1,1,2,1,1,1,1,0,1,1,1,0,1,1,1,1,1,1,2,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,2},//boss
                };
                break;
            case "Stage2-1":
                max_wave_count = 5;
                enemy_array = new int[6][]
                {
                new int[] {},
                new int[] {0,0,0},
                new int[] {1,1,1},
                new int[] {2,2,2},
                new int[] {0,0,0,0,0,0},
                new int[] {1,1,1,1,1,1,1,1,1},
                };
                break;
        }
        
    }

    private void Update()
    {

        if(field_state == FieldState.idle)
        {
            wave_button.GetComponent<Button>().interactable = true;
            if(wave_count != 0)
            {
                start_button_loaging_bar.GetComponent<Image>().fillAmount -= Time.deltaTime / 45f;
                if (start_button_loaging_bar.GetComponent<Image>().fillAmount <= 0) WaveStart();
            }
        }
        else if (field_state == FieldState.play)
        {
            wave_button.GetComponent<Button>().interactable = false;
            if(enemy_count == 0)
            {
                Time.timeScale = 3f;
                field_state = FieldState.idle;
                enemy_spawner.GetComponent<EnemySpawner>().spawn_way_count = 1;
                start_button_loaging_bar.GetComponent<Image>().fillAmount = 1;
            }
        }
    }

    public void WaveStart()
    {
        Time.timeScale = 1f;

        wave_count++;
        max_enemy_count = enemy_array[wave_count].Length;
        field_state = FieldState.play;
        enemy_count = max_enemy_count;
       

        enemy_spawner.GetComponent<EnemySpawner>().EnemySpawn(enemy_array[wave_count], max_enemy_count);

        start_button_loaging_bar.GetComponent<Image>().fillAmount = 0;

        //Debug.Log("max :" + max_wave_count + "\nwave : " + wave_count);
    }
}
