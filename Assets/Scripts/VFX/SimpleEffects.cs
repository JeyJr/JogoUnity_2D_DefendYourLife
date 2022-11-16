using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEffects : MonoBehaviour
{

    public void Rotate() => transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    public void Destroy() => Destroy(this.gameObject);
}
