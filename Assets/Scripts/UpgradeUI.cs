using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    private SpawnManager gameSpawnManager;
    private PlayerController player;
    private Base playerBase;
    public int maxHealthChange;
    public int healsPerWaveChange;
    public float healthPickupHealChange;
    public float healthRegenChange;
    public float playerSpeedChange;
    public float playerMassChange;
    public float playerSizeChange;
    public float boostCooldownChange;
    public float boostStrengthChange;
    public List<Button> upgrades;
    private Vector3[] buttonPositions = { new Vector3(), new Vector3(), new Vector3() };


    // Start is called before the first frame update
    void Start()
    {
        gameSpawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerBase = GameObject.Find("Base").GetComponent<Base>();
        for (int i = -1; i < 2; i++)
        {
            buttonPositions[i + 1] = new Vector3(Screen.width/2 + 200*i, Screen.height/3*2, 0);
        }
    }
    public void ShowUpgrades()
    {
        for(int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, upgrades.Count);
            if(!upgrades[index].IsActive())
            {
                upgrades[index].gameObject.SetActive(true);
                upgrades[index].transform.position = buttonPositions[i];
            }
            else
            {
                i--;
            }
        }
    }
    private void hideButtons()
    {
        for(int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].gameObject.SetActive(false);
        }
    }
    public void PickupHealthStrength()
    {
        gameSpawnManager.ChangeHealAmount(healthPickupHealChange);
        hideButtons();
    }
    public void HealthPickupsPerWave()
    {
        gameSpawnManager.ChangeHealsPerWave(healsPerWaveChange);
        hideButtons();
    }
    public void PlayerSpeed()
    {
        player.ChangeSpeed(playerSpeedChange);
        hideButtons();
    }
    public void PlayerMass()
    {
        player.ChangeMass(playerMassChange);
        hideButtons();
    }
    public void PlayerSize()
    {
        player.ChangeSize(playerSizeChange);
        hideButtons();
    }
    public void BoostCooldown()
    {
        player.ChangeBoostCooldown(boostCooldownChange);
        hideButtons();
    }
    public void BoostStrength()
    {
        player.ChangeBoostStrength(boostStrengthChange);
        hideButtons();
    }
    public void MaxHealth()
    {
        playerBase.ChangeMaxHealth(maxHealthChange);
        hideButtons();
    }
    public void HealthRegen()
    {
        playerBase.ChangeHealthRegen(healthRegenChange);
        hideButtons();
    }
}
