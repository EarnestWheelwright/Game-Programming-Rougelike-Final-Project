using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int heal;
    private Base playerBase;
    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.Find("Base").GetComponent<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerBase.ChangeHealth(heal);
        }
        Destroy(gameObject);
    }
}
