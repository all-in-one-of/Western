using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public Image healthBar;

    public PlayerGameplayValues playerStats;
    ControllerBehaviour controllerBehaviour;

    public void Start()
    {
        playerStats = GetComponent<PlayerGameplayValues>();
        controllerBehaviour = GetComponent<ControllerBehaviour>();
    }

    

    public void Update()
    {

        if (playerStats != null)
        {
            healthBar.fillAmount = playerStats.health / playerStats.maxHealth;
        }
        else
        {
            print("zbeb");
            playerStats = GetComponent<PlayerGameplayValues>();

        }

        if (Input.GetKeyDown(KeyCode.Space) && playerStats.health > 0)
        {
            playerStats.health -= 10;
        }

        FillingUpHealth();
        PlayerDead();
    }


    public void FillingUpHealth()
    {
        if (playerStats != null)
        {
            if (playerStats.health < playerStats.maxHealth && controllerBehaviour.data.state != ControllerData.PlayerStates.Dead)
            {
                playerStats.health += Time.deltaTime * playerStats.healthRegen;
            }
        }
        else
        {
            playerStats= GetComponent<PlayerGameplayValues>();
        }
    }

    public void PlayerDead()
    {
        if (playerStats != null)
        {
            if (playerStats.health <= 0 && controllerBehaviour.data.state == ControllerData.PlayerStates.Alive)
            {
                SoundManager.instance.PlayDie();
                controllerBehaviour.data.state = ControllerData.PlayerStates.Dead;
                GetComponent<PlayerBehaviour>().animator.SetTrigger("Dead");
            }
        }
    }
}
