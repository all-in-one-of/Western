using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Vector3 velocity;
    public Transform self;
    private float damage;


    public void Init(Vector3 velocity,float dmg)
    {
        this.velocity = velocity;
    }

    private void Update()
    {
        self.position+=(velocity*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
        if (other.gameObject.layer != 8) { return; }
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
