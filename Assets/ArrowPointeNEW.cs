using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointeNEW : MonoBehaviour
{
    public Rigidbody parentRigidbody;
    public GameObject parent;

    public bool trajectoireFinished;

    int comboValue;


    Vector3 scale;

    public void Start()
    {
        scale = transform.localScale;
    }

    public void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2))
        {
            if (hit.collider.gameObject.tag == "SurfaceRebond")
            {
                parentRigidbody.velocity = Vector3.Reflect(parentRigidbody.velocity, Vector3.ProjectOnPlane(hit.normal, Vector3.up))*1.02f;
                parent.transform.LookAt(parent.transform.forward);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ControllerBehaviour>() == null && other.gameObject.tag != "Ennemy" && other.gameObject.tag != "SurfaceRebond")
        {
            Debug.Log("collission arrow point: " + other.gameObject.name);
            parentRigidbody.isKinematic = true;
            transform.localScale = scale;
            trajectoireFinished = true;

            //son flache qui se plante
            SoundManager.instance.PlayUniqueSound(SoundManager.instance.arrowBounce);
        }

        else if (other.gameObject.tag == "Ennemy")
        {
            Debug.Log("ennemy");
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
