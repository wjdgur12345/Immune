using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUIManager : MonoBehaviour
{
    [SerializeField]
    public Text cost;
    private int stageCost;

    //public GameObject lifeImage;
    //public Text lifeText;
    public Text lifeText;
    private int life;

    void Start()
    {
        stageCost = 5000;
        life = 20;

        
    }

    private void Update()
    {
        cost.text = "" + stageCost;
        lifeText.text = "" + life;

        //lifeImage.GetComponent<Slider>().value = (float)life / (float)20;

    }

    public void ChangeCost(int n) 
    {
        stageCost += n;
    }

    public void ChangeLife(int n) { life += n; }
}
