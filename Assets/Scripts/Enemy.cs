using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Board board;
    public GameObject[] bullets;

    public Slider barLifeEnemy;
    public float maxHealth;
    int currentHealth;

    void Awake() {
        SetMaxHealth(maxHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void SetMaxHealth(float health) {
        barLifeEnemy.maxValue = health;
        barLifeEnemy.value = health;
    }

    public void SetHealth(float health) {
        Debug.Log(health);
        barLifeEnemy.value -= health;
        //textLife.text = maxHealth - currentHealth + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(board.currentState == GameState.wait) {
            board.currentState = GameState.turnEnemy;
            StartCoroutine(EnemyAtack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Atack") {
            string element = collision.gameObject.GetComponent<ShootToEnemy>().elementAtack;
            float damage = collision.gameObject.GetComponent<ShootToEnemy>().hit;
            SetHealth(damage);
            /* restar barra vida */
            board.currentState = GameState.wait;
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator EnemyAtack() {
        yield return new WaitForSeconds(.5f);
        /*ataque enemigo*/
        definirAtaque();
    }

    private void definirAtaque() {
        System.Random rn = new System.Random();
        string[] arrayvalores = new string[] { "basico", "especial", "Super Ataque" };
        double[] pesos = new double[] { 0.6, 0.3, 0.1 };
        double[] pesosAcumulados = pesos.Aggregate((IEnumerable<double>)new List<double>(),
                    (x, i) => x.Concat(new[] { x.LastOrDefault() + i })).ToArray();
        double rando = 0;
        rando = rn.NextDouble() * pesos.Sum();
        int posicionArray = pesosAcumulados.ToList().IndexOf(pesosAcumulados.Where(x => x > rando).FirstOrDefault());

        
        GameObject bullet = Instantiate(bullets[posicionArray], transform.position, Quaternion.identity);

    }
}
