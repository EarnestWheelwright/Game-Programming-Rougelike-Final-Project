using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject healthPickup;
    private OutOfBounds player;
    private int waveCount = 50;
    public float distance = 15;
    public float intermission = 5;
    private bool isWaiting = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<OutOfBounds>();
    }

    // Update is called once per frame
    void Update()
    {
        int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemiesAlive == 0 && !isWaiting)
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
        waveCount++;
        isWaiting = false;
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(intermission);
        SpawnWave(waveCount);
    }
}
