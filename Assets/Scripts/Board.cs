using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    move,
    onAtack,
    beforeAtack,
    atacking,
    afterAtack,
    wait,
    turnEnemy
}

public class Board : MonoBehaviour
{
    
    public GameState currentState = GameState.move;
    public int width;
    public int height;
    public int offSet;
    public GameObject tilePrefab;
    public GameObject[] pieces;
    public GameObject destroyEffect;
    private BackGroundTile[,] allTiles;
    public GameObject[,] allpieces;
    private FindMatches findMatches;
    private KillBoard killboard;
    void Start()
    {
        findMatches = FindObjectOfType<FindMatches>();
        allTiles = new BackGroundTile[width, height];
        allpieces = new GameObject[width, height];
        killboard = FindObjectOfType<KillBoard>();
        SetUp();
    }

    private void SetUp() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                // asigna una posicion en la matriz
                Vector2 tempPosition = new Vector2(i, j+offSet);
                GameObject backGroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backGroundTile.transform.parent = this.transform;
                backGroundTile.name = "( " + i + ", " + j + " )";
                int piecesToUse = Random.Range(0, pieces.Length);
                 
                // chequea si la pieza no genera match de 3, si genera la remplaza por otra
                int maxIterations = 0;
                while(MatchesAt(i,j, pieces[piecesToUse]) && maxIterations<100) {
                    piecesToUse = Random.Range(0, pieces.Length);
                    maxIterations++;
                }

                // asigna un elemento y lo posiciona en la matriz
                GameObject piece = Instantiate(pieces[piecesToUse], tempPosition, Quaternion.identity);
                piece.GetComponent<Pieces>().row = j;
                piece.GetComponent<Pieces>().column = i;
                piece.transform.parent = transform;
                piece.name = "( " + i + ", " + j + " )";
                allpieces[i, j] = piece;
            }
        }
    }

    private bool MatchesAt( int column, int row, GameObject piece) {
        if(column > 1 && row > 1) {
            if(allpieces[column-1,row].tag == piece.tag &&
                allpieces[column-2, row].tag == piece.tag) {
                return true;
            }
            if (allpieces[column, row - 1].tag == piece.tag &&
                allpieces[column, row - 2].tag == piece.tag) {
                return true;
            }
        }
        else if(column <= 1 || row <= 1) {
            if(row > 1) {
                if (allpieces[column, row - 1].tag ==piece.tag || allpieces[column, row - 2].tag == piece.tag) {
                    return true;
                }
            }
            if (column > 1) {
                if (allpieces[column - 1, row].tag == piece.tag || allpieces[column - 2, row].tag == piece.tag) {
                    return true;
                }
            }
        }
        return false;
    }

    private void DestroyMatchesAt(int column, int row) {
        if (allpieces[column, row].GetComponent<Pieces>().isMatched) {
            InstantiateKillPiece(column,row, allpieces[column, row].GetComponent<Pieces>().tag);
            findMatches.currentMatches.Remove(allpieces[column, row]);
            GameObject particle = Instantiate(destroyEffect,allpieces[column,row].transform.position, Quaternion.identity);
            Destroy(particle, 0.5f);
            Destroy(allpieces[column, row]);
            allpieces[column, row] = null;
        }
    }

    public void DestroyMatches() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if(allpieces[i,j] != null) {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    private void InstantiateKillPiece(int column, int row, string tag) {
        killboard.InstantiatePiece(column, row, tag);
    }

    private IEnumerator DecreaseRowCo() {
        int nullCount = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allpieces[i, j] == null) {
                    nullCount++;
                } else if(nullCount>0) {
                    allpieces[i, j].GetComponent<Pieces>().row -= nullCount;
                    allpieces[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allpieces[i,j] == null) {
                    Vector2 tempPosition = new Vector2(i, j+offSet);
                    int pieceToUse = Random.Range(0, pieces.Length);
                    GameObject piece = Instantiate(pieces[pieceToUse],tempPosition,Quaternion.identity);
                    allpieces[i, j] = piece;
                    piece.GetComponent<Pieces>().row = j;
                    piece.GetComponent<Pieces>().column = i;
                }
            }
        }
    }

    private bool MatchesOnBoard() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allpieces[i, j] != null) {
                    if(allpieces[i, j].GetComponent<Pieces>().isMatched) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo() {
        RefillBoard();
        yield return new WaitForSeconds(.5f);
        while (MatchesOnBoard()) {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }

        if(!MatchesOnBoard() && currentState == GameState.beforeAtack) {
            currentState = GameState.onAtack;
        }
        //yield return new WaitForSeconds(.5f);
        //currentState = GameState.move;
    }
}
