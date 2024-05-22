using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed;
    public bool goingDown;
    public float hoverSpeed;
    public float range = .15f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        if (transform.position.y < -range)
        {
            goingDown = false;
        }
        else if (transform.position.y > range)
        {
            goingDown = true;
        }
        if (goingDown)
        {
            transform.Translate(Vector3.down * Time.deltaTime * hoverSpeed);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * hoverSpeed);
        }
    }
}
