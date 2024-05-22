using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public float topBound = 5.7f;
    public float lowBound = -12f;
    public float rightBound =15f;
    public float leftBound = -15f;
    private Rigidbody characterRb;
    // Start is called before the first frame update
    void Start()
    {
        characterRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < leftBound)
        {
            characterRb.AddForce(new Vector3(characterRb.velocity.x * -2, 0, 0), ForceMode.VelocityChange);
            gameObject.transform.SetPositionAndRotation(new Vector3(leftBound, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);

        }
        else if (gameObject.transform.position.x > rightBound)
        {
            characterRb.AddForce(new Vector3(characterRb.velocity.x * -2, 0, 0), ForceMode.VelocityChange);
            gameObject.transform.SetPositionAndRotation(new Vector3(rightBound, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
        }
        if (gameObject.transform.position.z > topBound)
        {
            characterRb.AddForce(new Vector3(0, 0, characterRb.velocity.z * -2), ForceMode.VelocityChange);
            gameObject.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, topBound), gameObject.transform.rotation);
        }
        else if (gameObject.transform.position.z < lowBound)
        {
            characterRb.AddForce(new Vector3(0, 0, characterRb.velocity.z * -2), ForceMode.VelocityChange);
            gameObject.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, lowBound), gameObject.transform.rotation);
        }
    }
}
