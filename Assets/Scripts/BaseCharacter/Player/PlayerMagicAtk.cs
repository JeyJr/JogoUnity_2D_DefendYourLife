using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicAtk : MonoBehaviour
{
    public GameObject MagicFloorOfHell;
    public CharacterAttributes cAttributes;
    public LayerMask target;

    private bool attackingMagic;
    public bool AttackingMagic { get => attackingMagic;}

    private void Update()
    {
        //Magic FloorOfHell
        if (Input.GetKeyDown(KeyCode.Alpha1) && !AttackingMagic) StartCoroutine(SpawnMagicFloorOfHell());
    }

    IEnumerator SpawnMagicFloorOfHell()
    {
        SetAttackingMagic();

        yield return new WaitForSeconds(0.33f);
        ActiveMagicFloorOfHell();
    }

    //Function magic atk
    public void ActiveMagicFloorOfHell()
    {
        MagicFloorOfHell.GetComponent<FloorOfHell>().SetSkillValues(cAttributes.DealDmg(), .5f, 10, target, GetComponent<CharacterExpControl>());
        Instantiate(MagicFloorOfHell, transform.position, Quaternion.identity);
    }

    public void SetAttackingMagic()
    {
        attackingMagic = !attackingMagic;
    }
}
