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
    private Image button_image;


    void Start()
    {
        button_image = transform.Find("ImageMask").Find("image").GetComponent<Image>();

        image = GetComponent<Image>();
        color = image.color;
        color.a = 0f;
        image.color = color;
        GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        switch (button_image.GetComponent<Image>().sprite.name)
        {
            case "호중구1-1":
            case "호중구1-2":
            case "호중구2-1":
            case "호중구2-2":
            case "비만1-1":
            case "비만1-2":
            case "비만2-1":
            case "비만2-2":
            case "수지상1-1":
            case "수지상1-2":
            case "수지상2-1":
            case "수지상2-2":
            case "B1-1"://스프라이트 잘못 넣어서 T세포랑 B세포 바뀜
            case "B1-2":
            case "B2-1":
            case "B2-2":
                transform.Find("LockImage").gameObject.SetActive(true);
                break;
            default:
                transform.Find("LockImage").gameObject.SetActive(false);
                break;
        }

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
