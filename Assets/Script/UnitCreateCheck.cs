using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreateCheck : MonoBehaviour
{
    private Transform parent;
    private float range = 2.5f;
    private float timer = 0;
    private SpriteRenderer sr;
    private Color color;
    private new BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("@@@CREATE PRE UNIT@@@");
        parent = gameObject.transform.parent;
        transform.position = parent.position;
        transform.position += new Vector3(
            Random.Range(range * -1, range),
            Random.Range(range * -1, range),
            parent.transform.position.z
            );

        sr = GetComponent<SpriteRenderer>();

        color.a = 0.1f;
        sr.color = color;

        collider = GetComponent<BoxCollider2D>();
        collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.01f)
        {
            timer = 0f;
            transform.position = parent.position;
            transform.position += new Vector3(
                Random.Range(range * -1, range),
                Random.Range(range * -1, range),
                parent.transform.position.z
                );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ON TRIGGER");
        if(other.gameObject.tag == "Tile")
        {
            parent.GetComponent<Tower>().CreateUnit(transform.position.x, transform.position.y);
            Destroy(gameObject);
        }
    }
}
