using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;


    public void TurnSpawnPosition()
    {
        Vector3 newPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = newPosition;
    }
    public void SpawnBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
