using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuUIState : MonoBehaviour
{
    public GameObject game_data_manager;
    public UIState state;

    public enum UIState
    {
        gold,
        star
    }
    void Start()
    {
        //StateLoad();
    }

    public void StateLoad()
    {
        game_data_manager = GameObject.Find("GameDataManager");

        if (state == UIState.gold)
        {
            transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = game_data_manager.GetComponent<GameDataManager>().my_gold.ToString();
            //transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "123";
        }
        else
        {
            transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = game_data_manager.GetComponent<GameDataManager>().my_star.ToString();
        }
    }

}
