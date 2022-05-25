using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    public bool isMove = true;

    public float MoveSpeed => moveSpeed;

    private void Start()
    {
        if(GameObject.Find("GameManager") != null)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().dendritic_cell_debuff == GameManager.DendriticCellDebuffState.active)
                moveSpeed = moveSpeed - moveSpeed / 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
