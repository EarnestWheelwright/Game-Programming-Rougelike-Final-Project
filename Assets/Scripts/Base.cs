using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 0;
    private int maxHealth = 100;
    public TextMeshProUGUI healthDisplay;
    void Start()
    {
        ChangeHealth(100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(int change)
    {
        health += change;
        if (health < 1)
        {
            health = 0;
            StartCoroutine("death");
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthDisplay.SetText("Base HP: " + health);
    }

    private IEnumerator death()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Manager").GetComponent<Manager>().SetGameOver();
        Destroy(gameObject);
    }
}
