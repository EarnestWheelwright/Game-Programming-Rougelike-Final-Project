using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float force;
    private Rigidbody playerRb;
    private float verticalInput;
    private float horizontalInput;
    public float maxVelocity;
    public ParticleSystem collisionParticle;
    public float boostStrength;
    private bool onBoostCooldown = false;
    public float boostCooldown = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        force = playerRb.mass * 10;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        movePlayer(horizontalInput, verticalInput);
        if (playerRb.velocity.magnitude > maxVelocity)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxVelocity;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && !onBoostCooldown)
        {
            Boost();
        }
    }

    void movePlayer(float horizontalInput, float verticalInput)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            playerRb.AddForce(new Vector3(force * Time.deltaTime * horizontalInput, 0, 0), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            playerRb.AddForce(new Vector3(0, 0, force * Time.deltaTime * verticalInput), ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyComponent = collision.gameObject.GetComponent<Enemy>();
            enemyComponent.ChangeHealth(-playerRb.velocity.magnitude);
        }
    }
    private void Boost()
    {
        StartCoroutine(BoostCooldown());
        Debug.Log("X: " + Input.mousePosition.x + " Y: " + Input.mousePosition.y + " Z: " + Input.mousePosition.z);
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.y));
        Vector3 direction = (worldMousePosition - transform.position);
        direction.y = 0;
        direction = direction.normalized;
        playerRb.AddForce(direction * playerRb.velocity.magnitude * 2 - playerRb.velocity, ForceMode.VelocityChange);
        playerRb.AddForce(direction * boostStrength, ForceMode.Impulse);
    }
    private IEnumerator BoostCooldown()
    {
        onBoostCooldown = true;
        yield return new WaitForSeconds(boostCooldown);
        onBoostCooldown = false;
    }
    public void SetGameOver()
    {
        playerRb.isKinematic = true;
    }
}