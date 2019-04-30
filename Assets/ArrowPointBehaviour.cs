using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointBehaviour : MonoBehaviour
{
    public Rigidbody parentRigidbody;

    public void Start()
    {
        parentRigidbody = transform.parent.GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ControllerBehaviour>() == null)
        {
            parentRigidbody.isKinematic = true;
            transform.parent.parent = other.transform.GetChild(0).transform;
        }
    }
}
