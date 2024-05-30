using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject healthPickup;
    private OutOfBounds player;
    private int waveCount = 1;
    public float distance = 15;
    public int intermission = 5;
    private bool isWaiting = false;
    private bool gameOver = false;
    public TextMeshProUGUI waveDisplay;
    public TextMeshProUGUI intermissionTimer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<OutOfBounds>();
    }

    // Update is called once per frame
    void Update()
    {
        int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemiesAlive == 0 && !isWaiting && !gameOver)
        {
            StartCoroutine("Wait");
            isWaiting = true;
        }
    }

    private void SpawnWave(int wave)
    {
        for(int i = 0; i < wave; i++)
        {
            int random = Random.Range(0, enemyPrefabs.Length - 1);
            Vector3 position = new Vector3(Random.RandomRange(-1.0f, 1.0f), 0, Random.RandomRange(-1.0f, 1.0f)).normalized * distance;
            Instantiate(enemyPrefabs[random], position, enemyPrefabs[random].transform.rotation);
        }
        Vector3 pickupPosition = new Vector3(Random.Range(player.leftBound + 1, player.rightBound - 1), 0, Random.Range(player.lowBound + 1, player.topBound - 1));
        Instantiate(healthPickup, pickupPosition, healthPickup.transform.rotation);
        waveDisplay.text = "Wave: " + waveCount;
        waveCount++;
        isWaiting = false;
    }
    
    private IEnumerator Wait()
    {
        for(int i = intermission; i >= 0; i--)
        {
            intermissionTimer.text = "Intermission\n" + i;
            yield return new WaitForSeconds(1);
        }
        SpawnWave(waveCount);
        intermissionTimer.text = " ";
    }
    public void SetGameOver()
    {
        gameOver = true;
    }
}
