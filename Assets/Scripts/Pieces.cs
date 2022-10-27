using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;

    private FindMatches findMatches;
    private Board board;
    private GameObject otherPiece;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;
    public float swipeResist = .5f;

    void Start() {
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
    }

    void Update()
    {
        if (isMatched) {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0f, 0f, 0f, .2f);
        }

        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1f) {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .2f);
            if(board.allpieces[column,row]!= this.gameObject) {
                board.allpieces[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1f) {
            tempPosition = new Vector2(transform.position.x,targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .2f);
            if (board.allpieces[column, row] != this.gameObject) {
                board.allpieces[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allpieces[column, row] = this.gameObject;
        }
    }

    public IEnumerator CheckMoveCo() {
        yield return new WaitForSeconds(.5f);
        if(otherPiece != null) {
            if (!isMatched && !otherPiece.GetComponent<Pieces>().isMatched) {
                otherPiece.GetComponent<Pieces>().row = row;
                otherPiece.GetComponent<Pieces>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.move;
            }
            else {
                board.DestroyMatches();
            }
            otherPiece = null;
        }
    }

    private void OnMouseDown() {
        if(board.currentState == GameState.move) {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp() {
        if (board.currentState == GameState.move) {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle() {
        // aplica trigonometria
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist ||
           Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist) {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.beforeAtack;
        }
        else {
            board.currentState = GameState.move;
        }
    }

    void MovePieces() {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1) {
            //Right Swipe
            otherPiece = board.allpieces[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherPiece.GetComponent<Pieces>().column -= 1;
            column += 1;

        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1) {
            //Up Swipe
            otherPiece = board.allpieces[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherPiece.GetComponent<Pieces>().row -= 1;
            row += 1;

        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0) {
            //Left Swipe
            otherPiece = board.allpieces[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherPiece.GetComponent<Pieces>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0) {
            //Down Swipe
            otherPiece = board.allpieces[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherPiece.GetComponent<Pieces>().row += 1;
            row -= 1;
        }

        StartCoroutine(CheckMoveCo());
    }

    void FindMaches() {
        if(column > 0 && column < board.width - 1) {
            GameObject leftPiece1 = board.allpieces[column - 1, row];
            GameObject rightPiece1 = board.allpieces[column + 1, row];
            if(leftPiece1!=null && rightPiece1 != null) {
                if (leftPiece1.tag == gameObject.tag && rightPiece1.tag == gameObject.tag) {
                    leftPiece1.GetComponent<Pieces>().isMatched = true;
                    rightPiece1.GetComponent<Pieces>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1) {
            GameObject upPiece = board.allpieces[column, row + 1];
            GameObject downPiece = board.allpieces[column, row - 1];
            if (upPiece != null && downPiece != null) {
                if (upPiece.tag == gameObject.tag && downPiece.tag == gameObject.tag) {
                    upPiece.GetComponent<Pieces>().isMatched = true;
                    downPiece.GetComponent<Pieces>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }
}
