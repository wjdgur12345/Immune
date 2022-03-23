using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private static bool isPause;

    public Button pauseButton;

    private void Start()
    {
        isPause = false;
    }

    private void Update()
    {
        if (isPause)
        {
            Time.timeScale = 0;
            return;
        }

        if (!isPause)
        {
            Time.timeScale = 1;
            return;
        }
    }

    public void onPause()
    {
        if (isPause) isPause = false;
        else isPause = true;
    }
}
