using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GotoStage(int n = 1)
    {
        string str = "Stage1-" + n.ToString();
        Debug.Log(str);
        SceneManager.LoadScene(str);
    }
}
