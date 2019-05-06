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
    ControllerBehaviour controllerBehaviour;

    

    public void Start()
    {
        bowController = GetComponent<BowController>();
        controllerBehaviour = GetComponent<ControllerBehaviour>();
        //if premiere partie et jamais joué avant
        print(scriptableStats);
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
        if (controllerBehaviour.data.playerID == "_1")
        {
            if (GameManagerValues.instance.score >= scriptableStats.maxHealth.priceOverUpgradeLevel.Evaluate(GameManager.instance.p1healthUpgradeLevel))
            {
                GameManager.instance.p1healthUpgradeLevel++;
                maxHealth += 10;
                health += 10;
                GameManagerValues.instance.score -= scriptableStats.maxHealth.priceOverUpgradeLevel.Evaluate(GameManager.instance.p1healthUpgradeLevel);
            }
            
        }
        else
        {
            if (GameManagerValues.instance.score >= scriptableStats.maxHealth.priceOverUpgradeLevel.Evaluate(GameManager.instance.p2healthUpgradeLevel))
            {
                GameManager.instance.p2healthUpgradeLevel++;
                maxHealth += 10;
                health += 10;
                GameManagerValues.instance.score -= scriptableStats.maxHealth.priceOverUpgradeLevel.Evaluate(GameManager.instance.p2healthUpgradeLevel);
            }
        }
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
        if (controllerBehaviour.data.playerID == "_1")
        {
            if (GameManagerValues.instance.score >= scriptableStats.speed.priceOverUpgradeLevel.Evaluate(GameManager.instance.p1speedUpgradeLevel))
            {
                GameManager.instance.p1speedUpgradeLevel++;
                speed *= 1.05f;
            }

        }
        else
        {
            if (GameManagerValues.instance.score >= scriptableStats.speed.priceOverUpgradeLevel.Evaluate(GameManager.instance.p2speedUpgradeLevel))
            {
                GameManager.instance.p2speedUpgradeLevel++;
                speed *= 1.05f;
            }
        }
        Debug.Log("speed");

    }

    public void UpgradeMaxAmmo()
    {
        

        if (GetComponent<ControllerBehaviour>().data.playerID == "_1") {
            if (GameManagerValues.instance.score >= scriptableStats.maxAmmo.priceOverUpgradeLevel.Evaluate(GameManager.instance.p1magazineSizeUpgradeLevel))
            {
                GameManager.instance.p1magazineSizeUpgradeLevel++;
                magazineSize++;
                bowController.numberOfBullets++;
            }
            GameObject arrow = Instantiate(Menu.instance.lotFlechePlayer1.transform.GetChild(0).gameObject, Menu.instance.lotFlechePlayer1.transform);
        }
        else
        {
            GameObject arrow = Instantiate(Menu.instance.lotFlechePlayer2.transform.GetChild(0).gameObject, Menu.instance.lotFlechePlayer2.transform);
            if (GameManagerValues.instance.score >= scriptableStats.maxAmmo.priceOverUpgradeLevel.Evaluate(GameManager.instance.p2magazineSizeUpgradeLevel))
            {
                GameManager.instance.p2magazineSizeUpgradeLevel++;
                magazineSize++;
                bowController.numberOfBullets++;
            }
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
        if (GetComponent<ControllerBehaviour>().data.playerID == "_1")
        {
            if (GameManagerValues.instance.score >= scriptableStats.maxStamina.priceOverUpgradeLevel.Evaluate(GameManager.instance.p1maxStaminaUpgradeLevel))
            {
                GameManager.instance.p1maxStaminaUpgradeLevel++;
                maxStamina += 10;
                stamina += 10;
            }
        }
        else
        {
            if (GameManagerValues.instance.score >= scriptableStats.maxStamina.priceOverUpgradeLevel.Evaluate(GameManager.instance.p2maxStaminaUpgradeLevel))
            {
                GameManager.instance.p2magazineSizeUpgradeLevel++;
                maxStamina += 10;
                stamina += 10;
            }
        }
        Debug.Log("stamina");

    }

    public void UpgradeStaminaRegen()
    {
        staminaRegen++;
        Debug.Log("staminaRegen");

    }
}
