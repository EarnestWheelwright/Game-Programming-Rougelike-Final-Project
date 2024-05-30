using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject gameOverMenu;
    public void Start()
    {
        gameOverMenu = GameObject.Find("Game Over Menu");
        gameOverMenu.SetActive(false);
    }
    public void SetGameOver()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().SetGameOver();
        }
        GameObject playerRb = GameObject.Find("Player");
        playerRb.GetComponent<PlayerController>().SetGameOver();
        GameObject spawnManager = GameObject.Find("Spawn Manager");
        spawnManager.GetComponent<SpawnManager>().SetGameOver();
        gameOverMenu.SetActive(true);
    }
}
