using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOQuest", menuName = "Quest/Quest")]


public class SOQuest : ScriptableObject
{
    public string Descripcion;
    public int NivelRequerido;
    public float Recompensa;
    public SOQuest[] Requerimientos;
    public ScriptableEnemy Enemigo;

    public enum Estate
    {
        activada,
        desactivada,
    }
}

