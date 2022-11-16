using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public float moveSpeed;
    float initialPosition;

    public GameObject player;

    public bool active;

    private void Start()
    {
        initialPosition = transform.position.x;
    }

    private void FixedUpdate()
    {
        float d = player.transform.position.x * moveSpeed;
        transform.position = new Vector3(d + initialPosition, transform.position.y, transform.position.z);
    }



}
