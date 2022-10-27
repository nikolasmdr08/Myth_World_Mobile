using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        currentMatches = new List<GameObject>();
    }

    public void FindAllMatches() {
        StartCoroutine(FindAllMatchesCo());
    }


    private IEnumerator FindAllMatchesCo() {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++) {
            for (int j = 0; j < board.height; j++) {
                GameObject currentPiece = board.allpieces[i, j];
                if(currentPiece!= null) {
                    if(i>0 && i < board.width - 1) {
                        GameObject leftPiece = board.allpieces[i - 1, j];
                        GameObject rightPiece = board.allpieces[i + 1, j];
                        if(leftPiece != null && rightPiece != null) {
                            if(leftPiece.tag == currentPiece.tag &&
                               rightPiece.tag == currentPiece.tag) {
                                if (!currentMatches.Contains(leftPiece)) {
                                    currentMatches.Add(leftPiece);
                                }
                                leftPiece.GetComponent<Pieces>().isMatched = true;
                                if (!currentMatches.Contains(rightPiece)) {
                                    currentMatches.Add(rightPiece);
                                }
                                rightPiece.GetComponent<Pieces>().isMatched = true;
                                if (!currentMatches.Contains(currentPiece)) {
                                    currentMatches.Add(currentPiece);
                                }
                                currentPiece.GetComponent<Pieces>().isMatched = true;
                            }
                        }
                    }

                    if (j > 0 && j < board.height - 1) {
                        GameObject upPiece = board.allpieces[i, j - 1];
                        GameObject downPiece = board.allpieces[i, j + 1];
                        if (upPiece != null && downPiece != null) {
                            if (upPiece.tag == currentPiece.tag &&
                               downPiece.tag == currentPiece.tag) {
                                if (!currentMatches.Contains(upPiece)) {
                                    currentMatches.Add(upPiece);
                                }
                                upPiece.GetComponent<Pieces>().isMatched = true;
                                if (!currentMatches.Contains(downPiece)) {
                                    currentMatches.Add(downPiece);
                                }
                                downPiece.GetComponent<Pieces>().isMatched = true;
                                if (!currentMatches.Contains(currentPiece)) {
                                    currentMatches.Add(currentPiece);
                                }
                                currentPiece.GetComponent<Pieces>().isMatched = true;
                            }
                        }
                    }
                }
            }

        }
    }
}
