using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Board board;
    private string elementAtack;
    private float atackPower = 0;

    public GameObject atack;

    public Slider barLifePlayer;
    public int maxHealth;
    int currentHealth;

    void Awake() {
        SetMaxHealth(maxHealth);
    }

    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void SetMaxHealth(int health) {
        barLifePlayer.maxValue = health;
        barLifePlayer.value = health;
    }

    public void SetHealth(int health) {
        barLifePlayer.value -= health;
        //textLife.text = maxHealth - currentHealth + " / " + maxHealth;
    }

    void Update()
    {
        int checkPowers = FindObjectsOfType<PowerPieces>().Length;
        if(board.currentState == GameState.onAtack && checkPowers==0) {
            board.currentState = GameState.atacking;
            atackEnemy();
        }
    }

    private void atackEnemy() {
        //instancio el ataque y seteo los valores
        GameObject shootAtack = Instantiate(atack, transform.position, Quaternion.identity);
        shootAtack.GetComponent<ShootToEnemy>().hit = atackPower;
        shootAtack.GetComponent<ShootToEnemy>().elementAtack = elementAtack;
        board.currentState = GameState.afterAtack;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 6) { // 6 -> POWERPIECE layer
            
            if (elementAtack == null) {
                elementAtack = collision.gameObject.tag;
            }
            /* logica para calcular ataque con stats */
            atackPower++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == 8) { // 8 -> enemy atack
            /* restar vida player */
            if (collision.transform.name == "EnemyBasicAttack(Clone)") {
                currentHealth += 50;
                SetHealth(currentHealth);

            }
            if (collision.transform.name == "EnemyEspecialAttack(Clone)") {
                currentHealth += 100;
                SetHealth(currentHealth);
            }
            if (collision.transform.name == "EnemySuperAttack(Clone)") {
                currentHealth += 150;
                SetHealth(currentHealth);
            }

            board.currentState = GameState.move;
            Destroy(collision.gameObject);
        }
    }
}
