using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f; //snelheid toe lang het duurd
    public int attackDamage = 10;


    Animator anim; 
    GameObject player;
    PlayerHealth playerHealth; //ref naar een andere game object om de player te damagen
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer; //om de atspeed aan te passen van de enemy


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> (); 
    }


    void OnTriggerEnter (Collider other) //wanneer iets in een trigger komt
    {
        if(other.gameObject == player) // dit target de player, de speler dus
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other) //
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update () // hier gebeurt de attacking
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0) //speler gaat dood als de hp o is
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage); // gaat over in de idle state als het over is
        }
    }
}
