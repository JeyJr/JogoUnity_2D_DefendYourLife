using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMovement : MonoBehaviour
{
    void Update()
    {
        transform.localPosition += transform.right * 8 * Time.deltaTime;
    }
}
