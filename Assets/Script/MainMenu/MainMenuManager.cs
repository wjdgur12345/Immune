using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoStage1()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void GotoStage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void GameEnd()
    {
        Application.Quit();
    }
}
