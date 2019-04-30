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
        Vector3 vel = rigidBody.velocity;
        float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;


        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<BowController>()!=null)
        {
            BowController otherController = other.GetComponent<BowController>();
            if (otherController.numberOfBullets < otherController.data.magazineSize)
            {
                otherController.numberOfBullets++;
                Destroy(gameObject);
            }
        }
    }
}
