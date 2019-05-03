using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowValuesGeneralNEW : MonoBehaviour
{

    public float speed;

    public BoxCollider colliderTrigger;

    public void Start()
    {
        colliderTrigger.enabled = false;
        StartCoroutine(waitForSeconds(0.1f));
    }


    IEnumerator waitForSeconds(float timer)
    {
        yield return new WaitForSeconds(timer);
        colliderTrigger.enabled = true;

    }
}
