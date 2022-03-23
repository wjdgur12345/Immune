﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] createTowerFields;
    public GameObject wave_manager;
    public TextMeshProUGUI life_text;

    public Button pauseButton;
    public Image gray_image;
    public Button home_button;
    public Button restart_button;
    public Button continue_button;

    //이미지
    public Image game_over_image;
    public Image game_clear_image;
    public Canvas ui_canvas; 

    //게임 상태 관리
    public GameState game_state = GameState.play;

    public GameObject state_ui;

    public enum GameState
    {
        play,
        pause,
        clear,
        gameover
    }

    private void Start()
    {
        Time.timeScale = 1;

        gray_image.transform.position = new Vector3(100, 0, -10);
    }

    private void Update()
    {
        //타일 클릭
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())   //mobile : Input.GetTouch(0).fingerId
            ClickTile();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPause();
        }

        //게임오버 및 클리어 확인
        //게임 오버
        if(int.Parse(life_text.text) <= 0)
        {
            Time.timeScale = 0;
            if(game_state != GameState.gameover)
            {
                Image temp = Instantiate(game_over_image);
                temp.transform.SetParent(ui_canvas.transform);
                for(int i=0; i< ui_canvas.transform.parent.childCount; i++)
                {
                    if(ui_canvas.transform.parent.GetChild(i).gameObject.name != "GameControlUI")
                        ui_canvas.transform.parent.GetChild(i).gameObject.SetActive(false);
                }
                GameObject.Find("PlayObject").SetActive(false);

                pauseButton.transform.position = new Vector3(20, 4);
                gray_image.transform.position = new Vector3(0, 0, -10);
                home_button.transform.position = new Vector3(-4, -4);
                restart_button.transform.position = new Vector3(4, -4);
            }
            game_state = GameState.gameover;
            //Debug.Log("game over");
        }

        //클리어
        if(wave_manager.GetComponent<StageWaveManager>().max_wave_count == wave_manager.GetComponent<StageWaveManager>().wave_count &&
            wave_manager.GetComponent<StageWaveManager>().field_state != StageWaveManager.FieldState.play)
        {
            Time.timeScale = 0;
            if (game_state != GameState.clear)
            {
                Image temp = Instantiate(game_clear_image);
                temp.transform.SetParent(ui_canvas.transform);
                for (int i = 0; i < ui_canvas.transform.parent.childCount; i++)
                {
                    if (ui_canvas.transform.parent.GetChild(i).gameObject.name != "GameControlUI")
                        ui_canvas.transform.parent.GetChild(i).gameObject.SetActive(false);
                }
                GameObject.Find("PlayObject").SetActive(false);

                pauseButton.transform.position = new Vector3(20, 4);
                gray_image.transform.position = new Vector3(0, 0, -10);
                home_button.transform.position = new Vector3(-4, -4);
                restart_button.transform.position = new Vector3(4, -4);
            }
            game_state = GameState.clear;
            //Debug.Log("clear");
        }

    }

    private void ClickTile()
    {
        int layerMask = 1 << LayerMask.NameToLayer("TowerSpawner");

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero, 0.1f, layerMask);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<TowerCreateField>().OnToggle(true);
        }
        else
        {
            for(int i=0; i< createTowerFields.Length; i++)
            {
                createTowerFields[i].GetComponent<TowerCreateField>().OnToggle(false);
            }
        }
    }

    public void onPause()
    {
        //일시정지 버튼 + 컨티뉴 버튼
        if (game_state == GameState.pause)
        {
            //컨티뉴
            game_state = GameState.play;
            Time.timeScale = 1;
            gray_image.transform.position = new Vector3(100, 0, -10);
            pauseButton.transform.position = new Vector3(8, 4);
            home_button.transform.position = new Vector3(100, 0);
            restart_button.transform.position = new Vector3(100, 0);
            continue_button.transform.position = new Vector3(100, 0);
            state_ui.SetActive(true);
        }
        else 
        { 
            //일시정지
            game_state = GameState.pause;
            Time.timeScale = 0;
            gray_image.transform.position = new Vector3(0, 0, -10);
            pauseButton.transform.position = new Vector3(20, 4);
            home_button.transform.position = new Vector3(-4, 0);
            restart_button.transform.position = new Vector3(0, 0);
            continue_button.transform.position = new Vector3(4, 0);
            state_ui.SetActive(false);
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}