using System.Collections;
using UnityEngine;

public class SkillFloorOfHell : MonoBehaviour
{
    CharacterAttributes cAttributes;

    public GameObject floorOfHell;
    [SerializeField] private int skillDMG;
    [SerializeField] private int skillLevel;
    private int skillMaxLevel = 10;
    [SerializeField] private int manaCost;
    public LayerMask target;
    bool gainExp;

    private void Start()
    {
        cAttributes = GetComponentInParent<CharacterAttributes>();
        SetLevelUpgrade(1);
    }
    public void SetSkillActivation(bool gainExp)
    {
        this.gainExp = gainExp;

        skillDMG = cAttributes.MagicAtkPower;

        if (cAttributes.Mana > manaCost)
        {
            if (skillMaxLevel > 0)
                StartCoroutine(SpawnMagicFloorOfHell());
            else
                Debug.Log("Skill não upada!");
        }
        else
            Debug.Log("Sem mana!");
    }

    IEnumerator SpawnMagicFloorOfHell()
    {
        yield return new WaitForSeconds(0.33f);

        cAttributes.Mana -= manaCost;

        if(gainExp)
            floorOfHell.GetComponent<FloorOfHellBehavior>().SetSkillValues(skillDMG, skillLevel, target, GetComponentInParent<CharacterExpControl>());
        else
            floorOfHell.GetComponent<FloorOfHellBehavior>().SetSkillValues(skillDMG, skillLevel, target);

        Instantiate(floorOfHell, transform.position, Quaternion.identity);
    }

    public void SetLevelUpgrade(int value)
    {
        if(skillLevel < skillMaxLevel)
        {
            skillLevel += value;
            manaCost = skillLevel * 15;
        }
    }

}