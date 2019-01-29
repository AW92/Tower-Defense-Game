using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    //headers make multiple variables in a document easier to understand and provide headers on variables in the unity ui

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    

    // Start is called before the first frame update
    void Start()
    {
        //to call the updatetarget twice a second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        //get an array of all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //the shortest distance we have to an enemy
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                //finding what enemy is the closest to our turret, setting sights on that
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            //this is called when an enemy leaves our range, so we dont set sights on the enemy anymore
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check first if we have a target. If we do, forget updating the rest. Save processing power.
        if (target == null)
            return;

        //to change where the turret part to rotate is pointing, when target locks on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        
        //smooth current rotation to a new rotation
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        //every second, fireCountdown will go down by one
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //pull the bullet script and run seek for that bullet
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
