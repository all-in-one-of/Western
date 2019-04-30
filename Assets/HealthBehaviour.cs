using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    float health;
    public float MaxHealth;
    public Image healthBar;
    public float healthRegenerationSpeed;


    public void Start()
    {
        health = MaxHealth;
    }
    public void Update()
    {
        healthBar.fillAmount = health / MaxHealth;

        if (Input.GetKeyDown(KeyCode.Space)){
            health -= 1;
        }


    }


    public void FillingUpHealth()
    {
        if (health < MaxHealth)
        {
            health += Time.deltaTime * healthRegenerationSpeed;
        }
    }
}
