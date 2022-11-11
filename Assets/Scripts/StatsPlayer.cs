using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class StatsPlayer : ScriptableObject
{
    public string Playername;
    public float health;
    public Sprite PlayerArt;

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
