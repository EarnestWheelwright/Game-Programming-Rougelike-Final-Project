using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private int heal = 5;
    private Base playerBase;
    private AudioSource repairSFX;
    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.Find("Base").GetComponent<Base>();
        repairSFX = GameObject.Find("RepairSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            repairSFX.Play();
            playerBase.ChangeHealth(heal);
            Destroy(gameObject);
        }
    }
    public void ChangeHealAmount(float change)
    {
        if(heal == (int)(heal * change))
        {
            heal += 1;
        }
        else
        {
            heal = (int)(heal * change);
        }
    }
}
