using System.Collections;
using UnityEngine;

public class SkillFloorOfHell : MonoBehaviour
{
    CharacterAttributes cAttributes;

    public GameObject floorOfHell;
    [SerializeField] private int skillDMG;
    [SerializeField] private int skillLevel;
    [SerializeField] private int manaCost;
    public LayerMask target;

    bool castFloorOfHell, gainExp;

    private void Start()
    {
        cAttributes = GetComponentInParent<CharacterAttributes>();
        SetLevelUpgrade(1);
    }

    private void Update()
    {
        if (castFloorOfHell)
        {
            castFloorOfHell = false;
            skillDMG = cAttributes.MagicAtkPower;
            

            if (cAttributes.Mana > manaCost)
                StartCoroutine(SpawnMagicFloorOfHell());
            else
                Debug.Log("Sem mana!");
        }
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
        skillLevel += value;
        manaCost = skillLevel * 15;
    }

    public void SetSkillActivation(bool castFloorOfHell, bool gainExp)
    {
        this.castFloorOfHell = castFloorOfHell;
        this.gainExp = gainExp;
    }
}