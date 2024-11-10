using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour
{
    public float startTimeBtwShots;
    private float timeBetweenShots;
    [SerializeField] GameObject bullet;

    private void Start()
    {
        timeBetweenShots = startTimeBtwShots;
    }

    void Update()
    {
        // Shoot bullets
        if (timeBetweenShots <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBtwShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
