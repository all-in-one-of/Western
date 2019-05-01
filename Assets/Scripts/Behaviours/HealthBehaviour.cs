using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    public Image healthBar;
    public float healthRegenerationSpeed;

  ControllerBehaviour controllerBehaviour;

    public void Start()
    {
        health = MaxHealth;
        controllerBehaviour = GetComponent<ControllerBehaviour>();
    }
    public void Update()
    {
        healthBar.fillAmount = health / MaxHealth;

        if (Input.GetKeyDown(KeyCode.Space) && health>0)
        {
            health -= 10;
        }

        FillingUpHealth();
        PlayerDead();
    }


    public void FillingUpHealth()
    {
        if (health < MaxHealth && controllerBehaviour.data.state!=ControllerData.PlayerStates.Dead)
        {
            health += Time.deltaTime * healthRegenerationSpeed;
        }
    }

    public void PlayerDead()
    {
        if (health <= 0 && controllerBehaviour.data.state== ControllerData.PlayerStates.Alive)
        {
            SoundManager.instance.PlayDie();
           controllerBehaviour.data.state = ControllerData.PlayerStates.Dead;
        }
    }
}
