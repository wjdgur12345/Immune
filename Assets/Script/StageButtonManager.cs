using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButtonManager : MonoBehaviour
{
    //게임데이터 매니저 만들어서 관리
    public GameObject game_data_manager;

    //상태에 따른 스프라이트들
    public GameObject StageSelectManager;
    public Sprite complect_sprite;
    public Sprite select_sprite;
    public Sprite nomal_sprite;
    public Sprite locked_sprite;

    //상태관리 변수
    /* 
     * clear : 스테이지 클리시 별과 함께 나타남
     * select : 이전 스테이지를 클리어하고 해당 스테이지가 클리어 되지 않은 것들중 가장 처음 스테이지
     * nomal : 플레이는 가능하나 이전 스테이지를 클리어 하지 않았을 때
     * locked : 스테이지 요구 별을 모으지 못하여 플레이가 불가능한 상태
     */
    public ButtonState state = ButtonState.nomal;
    public int count_star;

    //버튼들 구분하는 변수, 스테이지 및 버튼 번호
    public int stage_number;
    public int button_number;

    //해금에 요구되는 별의 갯수
    public int need_star = 0;

    //게임데이터에서 자신의 번호를 기억함
    private int my_number = 0;

    public enum ButtonState
    {
        complet,
        select,
        nomal,
        locked
    }

    private void Start()
    {
        //게임데이터 받아오기, 초기에는 기본값으로 설정되어있음
        game_data_manager = GameObject.Find("GameDataManager");
        for (int i = 0; i < game_data_manager.GetComponent<GameDataManager>().stage_node.Length; i++)
        {
            if (game_data_manager.GetComponent<GameDataManager>().stage_node[i].stage_number == stage_number &&
                game_data_manager.GetComponent<GameDataManager>().stage_node[i].button_number == button_number)
            {
                my_number = i;
                count_star = game_data_manager.GetComponent<GameDataManager>().stage_node[i].star;
                switch (game_data_manager.GetComponent<GameDataManager>().stage_node[i].state)
                {
                    case 0:
                        state = ButtonState.locked;
                        break;
                    case 1:
                        state = ButtonState.nomal;
                        break;
                    case 2:
                        state = ButtonState.select;
                        break;
                    case 3:
                        state = ButtonState.complet;
                        break;
                }
                break;
            }
        }

        //별의 갯수를 보고 스테이지 해금을 함
        if(game_data_manager.GetComponent<GameDataManager>().my_star >= need_star &&
            state == ButtonState.locked)
        {
            state = ButtonState.nomal;
            game_data_manager.GetComponent<GameDataManager>().stage_node[my_number].state = 1;
        }

        //다른 스테이지의 상태를 보고 select상태로 들어감
        //나중에 할게여

        //시작할 때 게임 데이터에서 상태를 받아오고 state변경
        switch (state)
        {
            case ButtonState.complet:
                gameObject.transform.Find("Star_Group").gameObject.SetActive(true);
                gameObject.transform.Find("Text_Num").gameObject.SetActive(true);
                gameObject.transform.Find("Icon_Lock").gameObject.SetActive(false);
                GetComponent<Image>().sprite = complect_sprite;

                //별 갯수만큼 활성화
                int temp = count_star;
                for(int i=0; i<3; i++)
                {
                    if (temp > 0)
                    {
                        gameObject.transform.Find("Star_Group").GetChild(i).gameObject.SetActive(true);
                        temp--;
                    }  
                    else
                        gameObject.transform.Find("Star_Group").GetChild(i).gameObject.SetActive(false);
                }

                break;
            case ButtonState.nomal:
                gameObject.transform.Find("Star_Group").gameObject.SetActive(false);
                gameObject.transform.Find("Text_Num").gameObject.SetActive(true);
                gameObject.transform.Find("Icon_Lock").gameObject.SetActive(false);
                GetComponent<Image>().sprite = nomal_sprite;
                break;
            case ButtonState.select:
                gameObject.transform.Find("Star_Group").gameObject.SetActive(false);
                gameObject.transform.Find("Text_Num").gameObject.SetActive(true);
                gameObject.transform.Find("Icon_Lock").gameObject.SetActive(false);
                GetComponent<Image>().sprite = select_sprite;
                break;
            case ButtonState.locked:
                gameObject.transform.Find("Star_Group").gameObject.SetActive(false);
                gameObject.transform.Find("Text_Num").gameObject.SetActive(false);
                gameObject.transform.Find("Icon_Lock").gameObject.SetActive(true);
                GetComponent<Image>().sprite = locked_sprite;
                break;
        }

        //버튼 숫자를 Text_Num에 입력
        gameObject.transform.Find("Text_Num").GetComponent<TextMeshProUGUI>().text = button_number.ToString();
    }

    public void onClick()
    {
        switch (state)
        {
            case ButtonState.complet:
            case ButtonState.nomal:
            case ButtonState.select:
                StageSelectManager.GetComponent<StageSelectManager>().GotoStage1(button_number);
                game_data_manager.GetComponent<GameDataManager>().current_stage = my_number;
                break;
        }
    }
}
