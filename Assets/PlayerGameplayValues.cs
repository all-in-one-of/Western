using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplayValues : MonoBehaviour
{
    public PlayerStats scriptableStats;

    [Header("READ ONLY FOR DEBUG")]
    public float health;            //varie
    public float maxHealth;
    public float healthRegen;   
    public float speed;             
    public float magazineSize;
    public float numberOfArrowStartingGame;
    public float stamina;           //varie
    public float maxStamina;
    public float staminaRegen;

    BowController bowController;

    

    public void Start()
    {
        bowController = GetComponent<BowController>();
        //if premiere partie et jamais joué avant
        maxHealth = scriptableStats.maxHealth.startValue;
        healthRegen= scriptableStats.healthRegen.startValue;
        speed= scriptableStats.speed.startValue;
        magazineSize= scriptableStats.maxAmmo.startValue;
        numberOfArrowStartingGame = scriptableStats.startAmmo.startValue;
        maxStamina = scriptableStats.maxStamina.startValue;
        staminaRegen = scriptableStats.staminaRegen.startValue;


        health = maxHealth;
        stamina = maxStamina;
    }

    public void UpgradeHealth()
    {
        //if assez de money
        maxHealth += 10;
        health += 10;
        Debug.Log("health");
    }

    public void UpgradeHealthRegen()
    {
        //if assez de money
        healthRegen += 0.2f;
        Debug.Log("healthRegen");

    }

    public void UpgradeSpeed()
    {
        speed *= 1.05f;
        Debug.Log("speed");

    }

    public void UpgradeMaxAmmo()
    {
        magazineSize++;
        bowController.numberOfBullets++;

        if (GetComponent<ControllerBehaviour>().data.playerID == "_1") {
            GameObject arrow = Instantiate(Menu.instance.lotFlechePlayer1.transform.GetChild(0).gameObject, Menu.instance.lotFlechePlayer1.transform);
        }
        else
        {
            GameObject arrow = Instantiate(Menu.instance.lotFlechePlayer2.transform.GetChild(0).gameObject, Menu.instance.lotFlechePlayer2.transform);
        }
        Debug.Log("magazineSize");

    }

    public void UpgradeStartAmmo()
    {
        if (numberOfArrowStartingGame < magazineSize)
        {
            numberOfArrowStartingGame++;
            Debug.Log("numberOfArrowStartingGame");
        }
        else
        {
            Debug.Log("numberOfStartArrow supérieur a maxAmmo");
        }

    }

    public void UpgradeStamina()
    {
        maxStamina += 10;
        stamina += 10;
        Debug.Log("stamina");

    }

    public void UpgradeStaminaRegen()
    {
        staminaRegen++;
        Debug.Log("staminaRegen");

    }
}
