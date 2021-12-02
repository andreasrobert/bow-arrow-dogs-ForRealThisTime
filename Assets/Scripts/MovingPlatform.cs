using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points;
    public Transform subject;
    int goalPoint = 0;
    public float moveSpeed = 0;

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        subject.position = Vector2.MoveTowards(subject.position, points[goalPoint].position, Time.deltaTime * moveSpeed);

        if(Vector2.Distance(subject.position, points[goalPoint].position) < 0.1f)
        {
            if (goalPoint == points.Count - 1)
                goalPoint = 0;
            else
                goalPoint++;
        }
    }
}
