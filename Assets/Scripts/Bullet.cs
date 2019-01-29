using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        //if the bullet loses its target, say if the enemy gets to the end of the level.
        if (target ==null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        if(dir.magnitude <= distanceThisFrame)
        {
            //we have hit something, we have already hit it
            HitTarget();
            return;
        }

        //we havent hit a target
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        //add a new game object, being the impactEffect. The bulletImpactEffect prefab is referenced to in unity through the ui field + we then destroy it from the game after 2 seconds of being alive
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        //destroy the bullet itself
        Destroy(gameObject);
        //simple way for now of destroying the enemy. 
        Destroy(target.gameObject);
    }
}
