using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;

    public List<Transform> points;
    public Transform path;
    int goalPoint = 0;
    public float moveSpeed;


    void Start()
    {
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        Shoot();
        MoveToNextPoint();
        


    }

    void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void MoveToNextPoint()
    {
        path.position = Vector2.MoveTowards(path.position, points[goalPoint].position, Time.deltaTime * moveSpeed);

        if (Vector2.Distance(path.position, points[goalPoint].position) < 0.1f)
        {
            if (goalPoint == points.Count - 1)
                goalPoint = 0;
            else
                goalPoint++;
        }
    }
}