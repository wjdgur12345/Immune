using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEffect : MonoBehaviour
{
    public GameObject attack_effect;
    private float delete_timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        attack_effect.transform.position = transform.position;
        GameObject temp = Instantiate(attack_effect);
        temp.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        delete_timer += Time.deltaTime;

        if(delete_timer >= 3.0f)
            Destroy(gameObject);
        
    }
}
