using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRamassageNEW : MonoBehaviour
{
    public GameObject Arrow;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BowController>() != null)
        {
            BowController otherController = other.GetComponent<BowController>();
            PlayerGameplayValues otherControllerValues = other.GetComponent<PlayerGameplayValues>();
            if (otherController.numberOfBullets < otherControllerValues.magazineSize)
            {
                SoundManager.instance.PlayArrowTake();
                otherController.numberOfBullets++;
                Debug.Log("ici");
                Destroy(Arrow);
            }
        }
    }
}
