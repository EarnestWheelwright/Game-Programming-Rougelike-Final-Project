using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float force;
    private float forceMult = 10;
    private Rigidbody playerRb;
    private float verticalInput;
    private float horizontalInput;
    private float maxVelocity = 30f;
    public ParticleSystem collisionParticle;
    private float boostStrength = 0.1f;
    private bool onBoostCooldown = false;
    private float boostCooldown = 3;
    private AudioSource dashSFX;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        force = playerRb.mass * forceMult;
        dashSFX = GameObject.Find("DashSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        MovePlayer(horizontalInput, verticalInput);
        if (playerRb.velocity.magnitude > maxVelocity)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxVelocity;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !onBoostCooldown)
        {
            Boost();
        }
    }

    void MovePlayer(float horizontalInput, float verticalInput)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            playerRb.AddForce(new Vector3(force * .02f * horizontalInput, 0, 0), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            playerRb.AddForce(new Vector3(0, 0, force * .02f * verticalInput), ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionParticle.transform.position = gameObject.transform.position;
        float yRotation = gameObject.transform.rotation.y;
        collisionParticle.transform.rotation = new Quaternion(0, yRotation, 0, 0);
        collisionParticle.transform.Translate(Vector3.forward);
        collisionParticle.transform.rotation = new Quaternion(0, yRotation + 180, 0, 0);
        collisionParticle.Play();
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyComponent = collision.gameObject.GetComponent<Enemy>();
            enemyComponent.ChangeHealth(-playerRb.velocity.magnitude);
        }
    }
    private void Boost()
    {
        dashSFX.Play();
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
    public void ChangeSpeed(float change)
    {
        forceMult *= change;
        force = playerRb.mass * forceMult;
        maxVelocity *= change;
    }
    public void ChangeMass(float change)
    {
        playerRb.mass *= change;
        force = playerRb.mass * forceMult;
    }
    public void ChangeSize(float change)
    {
        gameObject.transform.localScale *= change;
    }
    public void ChangeBoostCooldown(float change)
    {
        boostCooldown *= change;
    }
    public void ChangeBoostStrength(float change)
    {
        boostStrength += change;
    }
}