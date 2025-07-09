using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;

    private float timeBtwShots;
    public float startTimeBtwnShots;

    public GameObject projectile;
    public Transform player;

    void Start()
    {

        timeBtwShots = startTimeBtwnShots;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update ()
    {
        if(timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwnShots;
        } else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}