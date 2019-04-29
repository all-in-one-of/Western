using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovingBehaviour : MonoBehaviour
{
    Rigidbody rigidBody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
