using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastFloorOfHell : MonoBehaviour
{
    public GameObject MagicFloorOfHell;
    public LayerMask target;
    public Transform spawnPoint;
    public CharacterAttributes cAttributes;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {

            MagicFloorOfHell.GetComponent<FloorOfHell>().SetSkillValues(cAttributes.DealDmg(), 2f, 50, target);
            Instantiate(MagicFloorOfHell, transform.position, Quaternion.identity);
        }
    }
}
