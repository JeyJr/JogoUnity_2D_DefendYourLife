using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int dmg;
    public void TakeDMG(int dmg)
    {
        life -= dmg;
    }

    public int DealDmg()
    {
        return dmg;
    }
}
