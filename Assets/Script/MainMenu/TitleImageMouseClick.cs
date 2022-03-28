using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleImageMouseClick : MonoBehaviour
{
    private float waiting_count = 3.0f;
    private GameObject text;
    private Color text_color;
    private bool is_plus_color = false;
    private float change_color_timer_limit = 0.01f;
    private float change_color_timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Text").gameObject;
        text_color = text.GetComponent<Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting_count > 0)
            waiting_count -= Time.deltaTime;
        else
            if (Input.anyKeyDown)
            ChangeScene();

        if(change_color_timer > change_color_timer_limit)
        {
            change_color_timer = 0f;
            if (is_plus_color)
                text_color = new Color(text_color.r, text_color.g, text_color.b, text_color.a + 0.02f);
            else
                text_color = new Color(text_color.r, text_color.g, text_color.b, text_color.a - 0.02f);

            text.GetComponent<Text>().color = text_color;
            //text_color = text.GetComponent<Text>().color;

            if (text_color.a > 1 || text_color.a < 0)
            {
                if (is_plus_color) is_plus_color = false;
                else is_plus_color = true;
            }
        }
        else
        {
            change_color_timer += Time.deltaTime;
        }
        
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
