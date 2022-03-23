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
    public enum FieldState 
    {
        idle,
        play
    }

    private void Start()
    {
        enemy_array = new int[5][]
        {
            new int[] {},
            new int[] { 0, 1, 2 },
            new int[] { 0, 0, 0, 0, 1, 1 },
            new int[] { 0, 0, 0, 1, 1, 1, 1, 1},
            new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        };
    }

    private void Update()
    {

        if(field_state == FieldState.idle)
        {
            wave_button.GetComponent<Button>().interactable = true;
        }
        else if (field_state == FieldState.play)
        {
            wave_button.GetComponent<Button>().interactable = false;
            if(enemy_count == 0)
            {
                field_state = FieldState.idle;
                enemy_spawner.GetComponent<EnemySpawner>().spawn_way_count = 1;
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

        //Debug.Log("max :" + max_wave_count + "\nwave : " + wave_count);
    }
}
