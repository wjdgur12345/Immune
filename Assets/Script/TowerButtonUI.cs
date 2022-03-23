using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButtonUI : MonoBehaviour
{
    private Color color;
    private Image image;
    [SerializeField]
    private Text costText;
    [SerializeField]
    private Image[] changeImage;
    void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
        color.a = 0f;
        image.color = color;
        GetComponent<Button>().interactable = false;
    }

    public void ChangeColorA(bool on)
    {
        if(on)
            color.a = 1f;
        else
            color.a = 0f;

        image.color = color;
    }

    public void ChangePos(float x, float y)
    {
        transform.position = new Vector3(x,y,transform.position.z);
    }

    public bool isInteractable(int upgradeCost, float x, float y)
    {
        ChangePos(x, y);
        ChangeColorA(true);
        //GetComponentInChildren<Text>().text = costText.text;

        if (int.Parse(costText.text) >= upgradeCost)
        {
            GetComponent<Button>().interactable = true;
            return true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
            return false;
        }
    }

    public void isInteractable(bool interactable, bool isVisible)
    {
        if (interactable)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            if(isVisible) ChangeColorA(true);
            else ChangeColorA(false);
            GetComponent<Button>().interactable = false;
        }
    }



}
