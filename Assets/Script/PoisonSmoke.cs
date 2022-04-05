using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmoke : MonoBehaviour
{
    private float destroy_count = 0;        //연기가 사라지는 시간
    public float damage_tick_limit = 0.5f;  //데미지 주는 틱(딜레이)
    public int damage;                      //데미지

    void Start()
    {
        
    }

    void Update()
    {
        destroy_count += Time.deltaTime;

        if(destroy_count >= 10.0f)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

}
