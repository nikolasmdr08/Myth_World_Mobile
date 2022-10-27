using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoard : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject[,] allKillPieces;
    public GameObject[] powersElements;

    void Start()
    {
        allKillPieces = new GameObject[width, height];
    }

    public void InstantiatePiece(int i,int j, string tag) {
        Vector2 tempPosition = new Vector2(i, j);
        int pieceToUse = GetPiece(tag);
        GameObject piece = Instantiate(powersElements[pieceToUse], tempPosition, Quaternion.identity);
        //allKillPieces[i, j] = piece;

    }

    private int GetPiece(string tag) {
        switch (tag) {
            case "ACERO":
                return 0;
            case "AGUA":
                return 1;
            case "ELECTRICIDAD":
                return 2;
            case "FANTASMA":
                return 3;
            case "FUEGO":
                return 4;
            case "HIELO":
                return 5;
            case "PLANTA":
                return 6;
            case "VENENO":
                return 7;
            default:
                return 0;
        }
    }

}
