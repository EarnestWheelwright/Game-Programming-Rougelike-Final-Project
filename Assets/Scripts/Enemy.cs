using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    private GameObject playerBase;
    private Rigidbody enemyRb;
    private int damage;
    public float health;
    public float maxVelocity;
    public GameObject dizzyEffect;
    private GameObject dizzyFx;
    private bool gameOver = false;
    private AudioSource hitSFX;
    private AudioSource deathSFX;
    public Slider healthBar;
    private Canvas worldCanvas;
    private float maxHealth;
    private Image healthBarColor;
    public Gradient colors;
    // Start is called before the first frame update
    void Awake()
    {
        hitSFX = GameObject.Find("EnemyHitSFX").GetComponent<AudioSource>();
        deathSFX = GameObject.Find("EnemyDeathSFX").GetComponent<AudioSource>();
        playerBase = GameObject.Find("Base");
        enemyRb = gameObject.GetComponent<Rigidbody>();
        worldCanvas = GameObject.Find("WorldCanvas").GetComponent<Canvas>();
        dizzyFx = Instantiate(dizzyEffect, transform.position, dizzyEffect.transform.rotation);
        healthBar = Instantiate(healthBar, worldCanvas.transform);
        healthBar.transform.Rotate(90, 0, 0);
        healthBarColor = healthBar.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gameOver)
        {
            Vector3 direction = (playerBase.transform.position - transform.position).normalized;
            dizzyFx.transform.position = transform.position;
            if (enemyRb.velocity.magnitude < maxVelocity)
            {
                enemyRb.AddForce(direction * speed * .02f);
                dizzyFx.SetActive(false);
            }
            else
            {
                dizzyFx.SetActive(true);
            }
        }
        healthBar.transform.position = new Vector3(this.transform.position.x, 10, this.transform.position.z + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Base"))
        {
            damage = (int)(health / 10);
            if (damage <= 0) damage = 1;
            Base baseComponent = other.gameObject.GetComponent<Base>();
            baseComponent.ChangeHealth(-damage);
            Destroy(healthBar.gameObject);
            Destroy(dizzyFx);
            Destroy(gameObject);
        }
    }


    public virtual void ChangeHealth(float healthChange)
    {
        if(healthChange < 0)
        {
            hitSFX.Play();
        }
        else
        {
            maxHealth = health += healthChange;
        }
        health += healthChange;
        if(health < 0)
        {
            health = 0;
            StartCoroutine("Death");
        }
        Debug.Log(health);
        healthBar.SetValueWithoutNotify(health/maxHealth);
        SetHealthBarColor(health/maxHealth);
    }
    private void SetHealthBarColor(float progress)
    {
        healthBarColor.color = colors.Evaluate(progress);
    }
    public float GetHealth()
    {
        return health;
    }
    private IEnumerator Death()
    {
        deathSFX.Play();
        yield return new WaitForSeconds(1);
        Destroy(healthBar.gameObject);
        Destroy(dizzyFx);
        Destroy(gameObject);
    }
    public void SetGameOver()
    {
        gameOver = true;
        enemyRb.isKinematic = true;
    }
}
