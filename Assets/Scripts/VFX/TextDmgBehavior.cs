using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDmgBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    public GameObject bgCritical;

    private void Awake()
    {
        speed = Random.Range(0.5f, 2);
        Destroy(this.gameObject, 1f);
    }

    private void Update()
    {
        this.transform.Translate((Vector3.up * speed) * Time.deltaTime);
    }
}
