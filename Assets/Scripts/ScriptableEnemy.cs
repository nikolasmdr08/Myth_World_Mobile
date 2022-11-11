using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemys")]
public class ScriptableEnemy : ScriptableObject
{
    public string Enemyname;
    public float health;
    public Sprite monsterArt;

    public int Attackwater;
    public int Attackfire;
    public int Attackghost;
    public int Attackiron;
    public int Attackelectric;
    public int Attackglass;
    public int Attackpoison;

    public int Defendwater;
    public int Defendfire;
    public int Defendghost;
    public int Defendiron;
    public int Defendelectric;
    public int Defendglass;
    public int Defendpoison;
}
