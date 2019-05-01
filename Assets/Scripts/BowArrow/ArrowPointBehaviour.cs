using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointBehaviour : MonoBehaviour
{
    public Rigidbody parentRigidbody;

    public bool trajectoireFinished;

    int comboValue;


    Vector3 scale;

    public void Start()
    {
        parentRigidbody = transform.parent.GetComponent<Rigidbody>();
        scale = transform.localScale;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ControllerBehaviour>() == null && other.gameObject.tag!="Ennemy")
        {
            parentRigidbody.isKinematic = true;
            transform.localScale = scale;
            trajectoireFinished = true;

            //son flache qui se plante
            SoundManager.instance.PlayUniqueSound(SoundManager.instance.arrowBounce);
        }

        else if(other.gameObject.tag == "Ennemy")
        {
            if (trajectoireFinished == false)
            {
                GameManagerValues.instance.score++;
                GameManagerValues.instance.comboValue++;

                GameManagerValues.instance.timerGoingOn = false;

                SoundManager.instance.PlayArrowHit();
            }
        }
    }
}
