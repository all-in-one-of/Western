using UnityEngine;

public class RevivingBehaviour : MonoBehaviour
{

     ControllerBehaviour controllerBehaviour;
     HealthBehaviour healthBehaviour;
     BowController bowController;
     float timerRevive;
     public float timeToRevivePlayer;
    PlayerGameplayValues playerStats;
    bool PlayerReviving;

    public void Start()
    {
        playerStats = GetComponent<PlayerGameplayValues>();

        controllerBehaviour = transform.parent.GetComponent<ControllerBehaviour>();
        healthBehaviour = transform.parent.GetComponent<HealthBehaviour>();
        bowController = transform.parent.GetComponent<BowController>();
        
    }

    public void Update()
    {
        if (timerRevive > 0 && PlayerReviving==true)
        {
            timerRevive -= Time.deltaTime;
        }

        if (timerRevive <= 0 && controllerBehaviour.data.state == ControllerData.PlayerStates.Dead && PlayerReviving==true)
        {
            SoundManager.instance.PlayUniqueSound(SoundManager.instance.revive);
            controllerBehaviour.data.state = ControllerData.PlayerStates.Alive;
            playerStats.health = playerStats.maxHealth/2;
            bowController.numberOfBullets = 0;

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ControllerBehaviour>() && controllerBehaviour.data.state==ControllerData.PlayerStates.Dead)
        {
            PlayerReviving = true;
            timerRevive = timeToRevivePlayer;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ControllerBehaviour>() )
        {
            PlayerReviving = false;
            timerRevive = 0;
        }
    }
}
