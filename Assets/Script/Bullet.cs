using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2;
    public int damage = 5;
    public int attack_count = 2;

    public Transform target = null;
    public Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y, target.position.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-moveDirection), 1f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            collision.GetComponent<EnemyManager>().ChangeHp(-damage);
            if (attack_count <= 1)
                Destroy(gameObject);
            else
                attack_count--;
        }
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
