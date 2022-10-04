using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehavior : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothT, speed;
    Vector3 currentVelocity;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref currentVelocity, smoothT, speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
