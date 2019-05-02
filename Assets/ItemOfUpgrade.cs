using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfUpgrade
{
    health,
    healthRegen,
    speed,
    maxAmmo,
    ammoStart,
    stamina,
    staminaRegen
}

public class ItemOfUpgrade : MonoBehaviour
{
    public TypeOfUpgrade typeOfUpgrade;

    bool playerIn;
    PlayerGameplayValues playerValues;
    ControllerBehaviour playerController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerGameplayValues>() != null)
        {
            playerIn = true;
            playerValues=other.GetComponent<PlayerGameplayValues>();
            playerController = other.GetComponent<ControllerBehaviour>();
            //player is buying

            Debug.Log(typeOfUpgrade);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        playerIn = false;
        playerValues = null;
        playerController = null;
    }

    public void Update()
    {
        if(playerController!=null)
        if (Input.GetButtonDown("A_Button" + playerController.data.playerID) && playerIn==true)
        {
            if (typeOfUpgrade == TypeOfUpgrade.health)
            {
                    // if (GameManagerValues.instance.score >= playerValues.scriptableStats.maxHealth.priceOverUpgradeLevel)
                    // {
                    playerValues.UpgradeHealth();
                // }

            
            }

            if (typeOfUpgrade == TypeOfUpgrade.healthRegen)
            {
                // if (playerValues.currentMoney >= playerValues.scriptableStats.maxHealth.priceOverUpgradeLevel)
                // {
                playerValues.UpgradeHealthRegen();
                // }
            }

            if (typeOfUpgrade == TypeOfUpgrade.speed)
            {
                // if (playerValues.currentMoney >= playerValues.scriptableStats.maxHealth.priceOverUpgradeLevel)
                // {
                playerValues.UpgradeSpeed();
                // }
            }

            if (typeOfUpgrade == TypeOfUpgrade.maxAmmo)
            {
                // if (playerValues.currentMoney >= playerValues.scriptableStats.maxHealth.priceOverUpgradeLevel)
                // {
                playerValues.UpgradeMaxAmmo();
                // }
            }

            if (typeOfUpgrade == TypeOfUpgrade.ammoStart)
            {
                // if (playerValues.currentMoney >= playerValues.scriptableStats.maxHealth.priceOverUpgradeLevel)
                // {
                playerValues.UpgradeStartAmmo();
                // }
            }

            if (typeOfUpgrade == TypeOfUpgrade.stamina)
            {
                // if (playerValues.currentMoney >= playerValues.scriptableStats.maxHealth.priceOverUpgradeLevel)
                // {
                playerValues.UpgradeStamina();
                // }
            }

            if (typeOfUpgrade == TypeOfUpgrade.staminaRegen)
            {
                // if (playerValues.currentMoney >= playerValues.scriptableStats.maxHealth.priceOverUpgradeLevel)
                // {
                playerValues.UpgradeStaminaRegen();
                // }
            }
        }

    }
}
