using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public Canvas stage_select;
    public Canvas stage1_select;
    public Canvas stage2_select;
    public Canvas stage3_select;

    public Button back_button;

    public MainMenuState state = MainMenuState.main;

    public enum MainMenuState
    {
        main,
        stage1,
        stage2,
        stage3
    }

    // Start is called before the first frame update
    void Start()
    {
        stage_select.gameObject.SetActive(true);
        stage1_select.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case MainMenuState.main:
                back_button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Stage Select";
                break;
            case MainMenuState.stage1:
                back_button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Stage 1";
                break;
        }
    }

    public void GotoStage1()
    {
        //SceneManager.LoadScene("Stage1 Select");
        state = MainMenuState.stage1;
        stage_select.gameObject.SetActive(false);
        stage1_select.gameObject.SetActive(true);
    }

    public void GotoStage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void BackButton()
    {
        switch (state)
        {
            case MainMenuState.main:
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false; // 빌드시 삭제
                break;
            case MainMenuState.stage1:
                stage_select.gameObject.SetActive(true);
                stage1_select.gameObject.SetActive(false);
                state = MainMenuState.main;
                break;
        }
    }
}
