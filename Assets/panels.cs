using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panels : MonoBehaviour
{
    public GameObject bk;
    Vector2 startPosition;
    Vector2 targetPosition;
    public bool isShow = false;
    public float speed;

    void Start() {

        startPosition = transform.position;
        targetPosition = bk.transform.position;
    }

    void Update() {
        var step = speed;
        if (isShow) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
        }

    }

    public void ChangeState() {
        // cambio el panel a mostrar
        isShow = !isShow;
        Debug.Log("CLICK");
    }
}
