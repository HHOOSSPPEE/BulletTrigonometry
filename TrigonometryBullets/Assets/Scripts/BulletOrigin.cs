/*mhosster - Merry Hospelhorn
 * 2021 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOrigin : MonoBehaviour
{
    public enum BulletPattern
    {
        Random,
        Spiral,
        Waves,
        Wiggle
    };
    public BulletPattern bulletPattern;
    public GameObject bullet;

    [Header("Bullet Settings")]

    [Tooltip("How far away from the source the bullet spawns")]
    public float radius;
    [Tooltip("Space between bullets")]
    public float spaceBetween = 3f;
    [Tooltip("Seconds between bullet spawns")]
    public float spawnSpeed;
    [Tooltip("Speed of bullets")]
    public float bulletForce = 20f;
    [Tooltip("Make the circle start somewhere else")]
    //[Range(0, 2f * Mathf.PI)] public float circleOffset = 0f;
    [Range(0, 2f * Mathf.PI)] public float theta;


    bool canSpawn;
    Vector3 spawnPoint, originLocation;
    GameObject bullet1;
    Rigidbody rb1;

    void Start()
    {
        canSpawn = true;
    }

    //basically im calculating the instantiation point with the point on circle
    void Update()
    {
        if (canSpawn == true)
        {
            switch (bulletPattern)
            {
                case BulletPattern.Random:
                    spawnPoint = PointOnCircle(Random.Range(0f, theta), radius);
                    originLocation = spawnPoint + gameObject.transform.position;

                    bullet1 = Instantiate(bullet, originLocation, gameObject.transform.rotation);
                    rb1 = bullet1.GetComponent<Rigidbody>();
                    rb1.AddForce(Vector3.Normalize(spawnPoint) * bulletForce, ForceMode.Impulse);

                    //make sure theres some time between bullets
                    canSpawn = false;
                    StartCoroutine(TimeBetweenSpawn());
                    break;
                case BulletPattern.Spiral:
                    spawnPoint = PointOnCircle((Time.time * spaceBetween) + theta, radius);
                    originLocation = spawnPoint + gameObject.transform.position;

                    bullet1 = Instantiate(bullet, originLocation, gameObject.transform.rotation);
                    rb1 = bullet1.GetComponent<Rigidbody>();
                    rb1.AddForce(Vector3.Normalize(spawnPoint) * bulletForce, ForceMode.Impulse);

                    //make sure theres some time between bullets
                    canSpawn = false;
                    StartCoroutine(TimeBetweenSpawn());

                    break;
                case BulletPattern.Waves:
                    for (int i = 0; i < ((Mathf.PI - spaceBetween) * 50); i++) //spawn bullets all around the circle - the lower the density, the more bullets ((2f * Mathf.PI - spaceBetween) * 100) | theta / spaceBetween
                    {
                        spawnPoint = PointOnCircle(i + theta, radius);
                        originLocation = spawnPoint + gameObject.transform.position;

                        bullet1 = Instantiate(bullet, originLocation, gameObject.transform.rotation);
                        rb1 = bullet1.GetComponent<Rigidbody>();
                        rb1.AddForce(Vector3.Normalize(spawnPoint) * bulletForce, ForceMode.Impulse);
                    }

                    //make sure theres some time between bullets
                    canSpawn = false;
                    StartCoroutine(TimeBetweenSpawn());
                    break;
                case BulletPattern.Wiggle:

                    spawnPoint = PointOnCircle(((Mathf.Sin(Mathf.PI * (Time.time))) * spaceBetween) + theta, radius);
                    originLocation = spawnPoint + gameObject.transform.position;

                    bullet1 = Instantiate(bullet, originLocation, gameObject.transform.rotation);
                    rb1 = bullet1.GetComponent<Rigidbody>();
                    rb1.AddForce(Vector3.Normalize(spawnPoint) * bulletForce, ForceMode.Impulse);

                    //make sure theres some time between bullets
                    canSpawn = false;
                    StartCoroutine(TimeBetweenSpawn());

                    break;

            }
        }
    }

    IEnumerator TimeBetweenSpawn()
    {
        yield return new WaitForSeconds(spawnSpeed);

        canSpawn = true;
    }

    Vector3 PointOnCircle(float theta, float radius)
    {
        return new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta));
    }
}
