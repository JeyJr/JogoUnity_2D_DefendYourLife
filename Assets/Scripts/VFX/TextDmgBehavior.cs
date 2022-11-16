using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Jobs;
using UnityEngine;


public class TextDmgBehavior : MonoBehaviour
{
    private float speed;

    private void Awake()
    {
        speed = Random.Range(1f, 2.5f);
        Destroy(this.gameObject, 1f);
    }

    private void Update()
    {
        transform.Translate((Vector3.up * speed) * Time.deltaTime);
    }


}
