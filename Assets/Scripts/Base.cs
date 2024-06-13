using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 100;
    private int maxHealth = 100;
    private float healthRegen = 0;
    private bool gameOver = false;
    public TextMeshProUGUI healthDisplay;
    public AudioSource hitSFX;
    public AudioSource deathSFX;
    void Start()
    {
        StartCoroutine(Regeneration());
    }

    public void ChangeHealth(int change)
    {
        if(change < 0)
        {
            hitSFX.Play();
        }
        health += change;
        if (health < 1)
        {
            health = 0;
            StartCoroutine("Death");
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthDisplay.SetText("Base HP: " + health);
    }

    private IEnumerator Death()
    {
        gameOver = true;
        deathSFX.Play();
        yield return new WaitForSeconds(2);
        GameObject.Find("Manager").GetComponent<Manager>().SetGameOver();
        Destroy(gameObject);
    }
    private IEnumerator Regeneration()
    {
        while(!gameOver)
        {
            if (healthRegen == 0)
            {
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                float regenPerSec = 1 / healthRegen;
                if (health < maxHealth)
                {
                    ChangeHealth(1);
                }
                yield return new WaitForSeconds(regenPerSec);
            }
        }
    }
    public void ChangeMaxHealth(int change)
    {
        maxHealth += change;
        ChangeHealth(change);
    }
    public void ChangeHealthRegen(float change)
    {
        healthRegen += change;
    }
}
