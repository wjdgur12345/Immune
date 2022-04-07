using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        enemy_array = new int[5][]
        {
            new int[] {},
            new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new int[] { 1},
            new int[] { 0},
            new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        };
    }

    private void Update()
    {

        if(field_state == FieldState.idle)
        {
            Time.timeScale = 3f;
            wave_button.GetComponent<Button>().interactable = true;
            if(wave_count != 0)
            {
                start_button_loaging_bar.GetComponent<Image>().fillAmount -= Time.deltaTime / 45f;
                if (start_button_loaging_bar.GetComponent<Image>().fillAmount <= 0) WaveStart();
            }
        }
        else if (field_state == FieldState.play)
        {
            Time.timeScale = 1f;
            wave_button.GetComponent<Button>().interactable = false;
            if(enemy_count == 0)
            {
                field_state = FieldState.idle;
                enemy_spawner.GetComponent<EnemySpawner>().spawn_way_count = 1;
                start_button_loaging_bar.GetComponent<Image>().fillAmount = 1;
            }
        }
    }

    public void WaveStart()
    {
        wave_count++;
        max_enemy_count = enemy_array[wave_count].Length;
        field_state = FieldState.play;
        enemy_count = max_enemy_count;
       

        enemy_spawner.GetComponent<EnemySpawner>().EnemySpawn(enemy_array[wave_count], max_enemy_count);

        start_button_loaging_bar.GetComponent<Image>().fillAmount = 0;

        //Debug.Log("max :" + max_wave_count + "\nwave : " + wave_count);
    }
}
