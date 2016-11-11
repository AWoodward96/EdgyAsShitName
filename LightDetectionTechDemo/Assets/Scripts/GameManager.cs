﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    static GameManager instance;

    static Vector3 playerStartingPosition;

	// Use this for initialization
	void Start ()
    {
        instance = this;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //gets all tagged enemies
        for (int i = 0; i < enemies.Length; i++) //loops through all the enemies
        {
            for (int i2 = 0; i2 < enemies.Length; i2++) //loops through all the enemies
            {
                Physics.IgnoreCollision(enemies[i].GetComponent<Collider>(), enemies[i2].GetComponent<Collider>());
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //gets all tagged enemies
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //gets the tagged player
        for (int i = 0; i < enemies.Length; i++) //loops through all the enemies
        {
            if(!enemies[i].GetComponent<EnemyController>().guarding) //if this enemy is chasing the player, check it's target position. If it can see the player, chase them. If not, go to the players last seen position
            {
                //the result of the raycast check
                RaycastHit hit;
                //the distance between the player and enemy, for raycasting
                Vector3 dist = player.transform.position - enemies[i].transform.position;

                //checks to see if the enemy can see the player(if a raycast between them results in colliding with the player's collider)
                if (Physics.Raycast(enemies[i].transform.position, dist, out hit) && hit.collider != player.GetComponent<Collider>())
                {
                }
                //only chase the player if the enemy can see them
                else
                {
                    enemies[i].GetComponent<EnemyController>().target = player.transform.position; //...update it's target position
                }
            }
        }
    }

    public void PlayerInLight() //run this function if the player is lit up
    {
        //Debug.Log("IN LIGHT");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //gets all tagged enemies
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //gets the tagged player
        for (int i = 0; i < enemies.Length; i++) //loops through all the enemies
        {
            //the result of the raycast check
            RaycastHit hit;
            //the distance between the player and enemy, for raycasting
            Vector3 dist = player.transform.position - enemies[i].transform.position;

            //checks to see if the enemy can see the player(if a raycast between them results in colliding with the player's collider)
            if (Physics.Raycast(enemies[i].transform.position, dist, out hit) && hit.collider != player.GetComponent<Collider>())
            {
            }
            //only chase the player if the enemy can see them
            else
            {
                enemies[i].GetComponent<EnemyController>().guarding = false; //sets this enemy to chase the player
                enemies[i].GetComponent<EnemyController>().target = player.transform.position; //sets the enemies target to the player's position
            }
        }
    }

    public static void Restart() //resets the level to it's original state
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //gets all tagged enemies
        for (int i = 0; i < enemies.Length; i++) //loops through all the enemies
        {
            enemies[i].transform.position = new Vector3(enemies[i].GetComponent<EnemyController>().startingPos.x, enemies[i].transform.position.y, enemies[i].GetComponent<EnemyController>().startingPos.y); //sets this enemy to it's starting position
            enemies[i].GetComponent<EnemyController>().guarding = true; //sets this enemy to follow it's path
            enemies[i].GetComponent<EnemyController>().resetPath(); //makes sure this enemies path is followed in the right order
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //gets the tagged player
        player.transform.position =playerStartingPosition; //sets the player to it's starting position
    }

    public static void InitializePlayer( Vector3 startingPosition)
    {
        playerStartingPosition = startingPosition;
    }
}
