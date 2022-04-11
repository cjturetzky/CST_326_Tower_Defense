using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public float shotRate = 2.0f;
    private float timeSinceLastShot = 0.0f;
    public float maxRange = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            NavEnemyScript[] enemies = FindObjectsOfType<NavEnemyScript>();
            if(enemies.Length >= 1){
                NavEnemyScript closestEnemy = enemies[0];
                float distanceToClosestEnemy = Vector3.Distance(closestEnemy.transform.position, transform.position);
                foreach(NavEnemyScript enemy in enemies){
                    float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                    if(distanceToEnemy < distanceToClosestEnemy){
                        closestEnemy = enemy;
                        distanceToClosestEnemy = distanceToEnemy;
                    }
                }
                if(distanceToClosestEnemy < maxRange){
                    target = closestEnemy.gameObject;
                }
            }
        }
        if(timeSinceLastShot >= shotRate && target != null){
            Debug.Log("Firing!");
            timeSinceLastShot = 0.0f;
            GameObject firedBullet = Instantiate(bullet);
            firedBullet.GetComponent<BulletBehavior>().target = target;
            if(Vector3.Distance(target.transform.position, transform.position) > maxRange){
                target = null;
            }

        }
        else{
            timeSinceLastShot += Time.deltaTime;
        }
    }
}
