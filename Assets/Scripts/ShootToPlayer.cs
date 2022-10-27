using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToPlayer : MonoBehaviour
{
    public float damage;
    Transform target;
    private float speed = .05f;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().transform; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }
}
