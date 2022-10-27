using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPieces : MonoBehaviour
{

    private Transform target;
    private float speed = .05f;

    void Start()
    {

        target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }
}
