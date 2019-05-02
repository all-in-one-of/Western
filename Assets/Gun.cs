using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour;
    public Transform embout;
    public SpriteRenderer spriteRenderer;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour player = other.transform.parent.GetComponentInParent<PlayerBehaviour>();
        print("object in trigger : " + other);
        if (player != null)
        {
            enemyBehaviour.playerInTriggerBox = player;
            return;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
        if (player != null && player==enemyBehaviour.playerInTriggerBox)
        {
            enemyBehaviour.playerInTriggerBox = null;
        }
    }

    public void ShootOnPlayer(PlayerBehaviour player) 
    {
        BulletBehaviour bullet = Instantiate(EnemyManager.instance.bulletPrefab, embout.position, embout.rotation).GetComponent<BulletBehaviour>();

        Vector3 direction = (player.self.position - enemyBehaviour.self.position).normalized;

        bullet.Init(new Vector3(direction.x,0,direction.z) * enemyBehaviour.enemy.bulletSpeed,enemyBehaviour.enemy.bulletDamage);
    }
}
