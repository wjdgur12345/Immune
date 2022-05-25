using System.Collections;
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

    //클리어 및 게임오버
    public GameObject game_over_object;
    public GameObject game_clear_object;
    public Canvas ui_canvas; 

    //게임 상태 관리
    public GameState game_state = GameState.play;

    public GameObject state_ui;

    //버프 및 디버프 관리
    public DendriticCellBuffState dendritic_cell_buff = DendriticCellBuffState.none;
    public DendriticCellDebuffState dendritic_cell_debuff = DendriticCellDebuffState.none;
    public TCellBuffState t_cell_buff = TCellBuffState.none;


    //게임 상태 관리
    public enum GameState
    {
        play,
        pause,
        clear,
        gameover
    }

    //수지상세포의 아군 버프 상태
    public enum DendriticCellBuffState
    {
        none,
        active
    }

    //수지상세포의 적군 디버프 상태
    public enum DendriticCellDebuffState
    {
        none,
        active
    }

    //T세포의 아군 버프 상태, 수지상과 차별점을 위해 중첩 가능하도록
    public enum TCellBuffState
    {
        none,
        active_level_1,
        active_level_2
    }

    private void Start()
    {
        Time.timeScale = 3.0f;

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
        if (int.Parse(life_text.text) <= 0)
        {
            Time.timeScale = 0;
            if(game_state != GameState.gameover)
            {
                GameObject temp = Instantiate(game_over_object, ui_canvas.transform);
                temp.transform.GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f);
                for (int i=0; i< ui_canvas.transform.parent.childCount; i++)
                {
                    if(ui_canvas.transform.parent.GetChild(i).gameObject.name != "GameControlUI")
                        ui_canvas.transform.parent.GetChild(i).gameObject.SetActive(false);
                }
                GameObject.Find("PlayObject").SetActive(false);

                pauseButton.transform.position = new Vector3(20, 4);
                gray_image.transform.position = new Vector3(0, 0, -10);
                home_button.transform.position = new Vector3(-4, -4);
                restart_button.transform.position = new Vector3(4, -4);

                home_button.transform.SetParent(temp.transform);
                restart_button.transform.SetParent(temp.transform);
            }
            game_state = GameState.gameover;
            //Debug.Log("game over");
        }

        //클리어
        if(wave_manager.GetComponent<StageWaveManager>().max_wave_count == wave_manager.GetComponent<StageWaveManager>().wave_count &&
            wave_manager.GetComponent<StageWaveManager>().field_state != StageWaveManager.FieldState.play)
        {
            Time.timeScale = 0f;
            if (game_state != GameState.clear && GameObject.Find("Play_Result_Victory_Common") == null)
            {

                //Image temp = Instantiate(game_clear_image);
                //temp.transform.SetParent(ui_canvas.transform);
                GameObject temp;

                
                temp = Instantiate(game_clear_object, ui_canvas.transform);
                

                temp.transform.GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f);
                temp.transform.position = new Vector3(0, 0);
                for (int i = 0; i < ui_canvas.transform.parent.childCount; i++)
                {
                    if (ui_canvas.transform.parent.GetChild(i).gameObject.name != "GameControlUI")
                        ui_canvas.transform.parent.GetChild(i).gameObject.SetActive(false);
                }
                if(GameObject.Find("PlayObject") != null)
                    GameObject.Find("PlayObject").SetActive(false);

                pauseButton.transform.position = new Vector3(20, 4);
                gray_image.transform.position = new Vector3(0, 0, -10);
                home_button.transform.position = new Vector3(-4, -4);
                restart_button.transform.position = new Vector3(4, -4);

                home_button.transform.SetParent(temp.transform);
                restart_button.transform.SetParent(temp.transform);

                //목숨의 수와 비례하여 별 획득 정보 보여주기

                //클리어 정보를 데이터매니저로 보냄
                GameObject data_manager = GameObject.Find("GameDataManager");
                if(data_manager != null)
                {
                    data_manager.GetComponent<GameDataManager>().stage_node[data_manager.GetComponent<GameDataManager>().current_stage].state = 3;
                    int temp_star;

                    if (int.Parse(life_text.text) == 20)
                        temp_star = 3;
                    else if (int.Parse(life_text.text) >= 10)
                        temp_star = 2;
                    else
                        temp_star = 1;

                    temp_star = temp_star - data_manager.GetComponent<GameDataManager>().stage_node[data_manager.GetComponent<GameDataManager>().current_stage].star;

                    if (temp_star < 0)
                        temp_star = 0;

                    if (temp_star != 0)
                    {
                        data_manager.GetComponent<GameDataManager>().stage_node[data_manager.GetComponent<GameDataManager>().current_stage].star += temp_star;
                        data_manager.GetComponent<GameDataManager>().my_star += temp_star;
                        temp.transform.Find("Popup").Find("Frame").Find("TextFrame").Find("Text_Value").GetComponent<TextMeshPro>().text = " + " + temp_star.ToString();
                    }
                }
                

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
            if (wave_manager.GetComponent<StageWaveManager>().field_state == StageWaveManager.FieldState.play)
                Time.timeScale = 1.0f;
            else
                Time.timeScale = 3.0f;
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
            Time.timeScale = 0.0f;
            Debug.Log(Time.timeScale.ToString());
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
