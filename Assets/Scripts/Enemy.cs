using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.Find("Base");
        enemyRb = gameObject.GetComponent<Rigidbody>();
        dizzyFx = Instantiate(dizzyEffect, transform.position, dizzyEffect.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            Vector3 direction = (playerBase.transform.position - transform.position).normalized;
            dizzyFx.transform.position = transform.position;
            if (enemyRb.velocity.magnitude < maxVelocity)
            {
                enemyRb.AddForce(direction * speed * Time.deltaTime);
                dizzyFx.SetActive(false);
            }
            else
            {
                dizzyFx.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Base"))
        {
            damage = (int)(health / 10);
            if (damage == 0) damage = 1;
            Base baseComponent = other.gameObject.GetComponent<Base>();
            baseComponent.ChangeHealth(-damage);
            Destroy(dizzyFx);
            Destroy(gameObject);
        }
    }


    public virtual void ChangeHealth(float healthChange)
    {
        health += healthChange;
        Debug.Log(health);
        if(health < 0)
        {
            StartCoroutine("Death");
        }
    }
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(dizzyFx);
        Destroy(gameObject);
    }
    public void SetGameOver()
    {
        gameOver = true;
        enemyRb.isKinematic = true;
    }
}
