using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public int my_gold;
    public int my_star;
    public StageStateNode[] stage_node;
    public int current_stage;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        stage_node = new StageStateNode[12];

        stage_node[0] = new StageStateNode(2, 0, 1, 1);
        int sn = 1;
        int bn = 1;
        for(int i = 1; i< stage_node.Length; i++)
        {
            bn++;
            if (bn >= 5)
            {
                bn = 1;
                sn++;
            }

            stage_node[i] = new StageStateNode(0, 0, sn, bn);
        }
    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        GameObject temp = GameObject.Find("Status_Coin");
        if(temp != null)
        {
            GameObject.Find("Status_Coin").GetComponent<MainMenuUIState>().StateLoad();
            GameObject.Find("Status_Star").GetComponent<MainMenuUIState>().StateLoad();
        }
    }
}

public class StageStateNode
{
    //0 lock
    //1 nomal
    //2 select
    //3 clear
    public int stage_number;
    public int button_number;
    public int state;
    public int star;
    public StageStateNode(int state, int star, int stage_number, int button_number)
    {
        this.state = state;
        this.star = star;
        this.stage_number = stage_number;
        this.button_number = button_number;
    }

    
}
