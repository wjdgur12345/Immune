using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButtonManager : MonoBehaviour
{
    //게임데이터 매니저 만들어서 관리
    //세이브와 동시에 관리해야함
    public GameObject StageSelectManager;
    public Sprite complect_sprite;
    public Sprite select_sprite;
    public Sprite nomal_sprite;
    public Sprite locked_sprite;

    public ButtonState state = ButtonState.nomal;
    public int count_star;

    public int button_number;

    public enum ButtonState
    {
        complet,
        select,
        nomal,
        locked
    }

    private void Start()
    {
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
                StageSelectManager.GetComponent<StageSelectManager>().GotoStage(button_number);
                break;
        }
    }
}
