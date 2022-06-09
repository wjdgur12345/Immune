using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveProgressManager : MonoBehaviour
{
    private GameObject[] wave_check;
    private GameObject wave_slider;
    private StageWaveManager stage_wave_manager;
    void Start()
    {
        wave_slider = transform.Find("Slider").gameObject;

        stage_wave_manager = GameObject.Find("WaveManager").GetComponent<StageWaveManager>();

        wave_check = new GameObject[transform.Find("Num").childCount];
        for(int i=0; i<wave_check.Length; i++)
        {
            wave_check[i] = transform.Find("Num").GetChild(i).Find("On").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(stage_wave_manager.wave_count != 0)
        {
            wave_slider.GetComponent<Slider>().value = ((1f / (float)stage_wave_manager.max_wave_count) * ((float)stage_wave_manager.wave_count - 1)) +
                        (((1f / (float)stage_wave_manager.max_wave_count) / (float)stage_wave_manager.max_enemy_count) 
                        * ((float)stage_wave_manager.max_enemy_count -(float)stage_wave_manager.enemy_count));

            if (stage_wave_manager.field_state == StageWaveManager.FieldState.idle)
            {
                wave_check[stage_wave_manager.wave_count - 1].SetActive(true);
            }
                
        }
            

        

    }
}
