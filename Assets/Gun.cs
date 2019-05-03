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
        PlayerBehaviour player = other.GetComponentInParent<PlayerBehaviour>();
        if (player == null)
        {
            player = other.GetComponent<PlayerBehaviour>();
        }
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
        BulletBehaviour bullet = Instantiate(EnemyManager.instance.bulletPrefab, new Vector3(embout.position.x,transform.position.y,embout.position.z), Quaternion.identity).GetComponent<BulletBehaviour>();
        Vector3 direction = (player.self.position - enemyBehaviour.self.position).normalized;

        bullet.Init(new Vector3(direction.x,0,direction.z) * enemyBehaviour.enemy.bulletSpeed,enemyBehaviour.enemy.bulletDamage);
    }
}
