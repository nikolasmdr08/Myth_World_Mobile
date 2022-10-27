using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Elements
{
    FANTASMA,
    ACERO,
    AGUA,
    ELECTRICIDAD,
    FUEGO,
    HIELO,
    PLANTA,
    VENENO
}

public class ShootToEnemy : MonoBehaviour
{
    private Transform target;
    private float speed = .05f;

    public string elementAtack;
    public float hit;
    public Sprite[] sprites;

    void Start()
    {
        target = FindObjectOfType<Enemy>().transform;
        var val = (int)Enum.Parse(typeof(Elements), elementAtack);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[val];

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }
}
