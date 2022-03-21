using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    void Awake ()
    {
        //find player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //get player health
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemy health
        enemyHealth = GetComponent<EnemyHealth>();
        //nav component
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update ()
    {
        //if current health >0 and player is still FUCKING ALIVE
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            //MOVE TO THE PLAYER YEEE AI
            nav.SetDestination (player.position);
        }
        else
        {   //dont enable navigation
            nav.enabled = false;
        }
    }
}
