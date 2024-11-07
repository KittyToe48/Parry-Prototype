using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Current Stats")]
    public float CurrentDamage = 0;
    public float CurrentMultiplier = 1;

    [Header("Base Stats")]
    public float BaseDamage = 0;
    public float BaseMultiplier = 1;

    [Header("Temp Stats")]
    public List<float> TempHitDamage = new List<float>();
    public List<float> TempHitMultiplier = new List<float>();

    private void Start()
    {
        CurrentDamage = BaseDamage;
        CurrentMultiplier = BaseMultiplier;
    }

    public void AddTemp(int type, float amount, bool damage)
    {
        // 1 = hits / combo, 2 = abilities
        switch (type)
        {
            case 1:
                if (damage)
                {
                    TempHitDamage.Add(amount); 
                    CurrentDamage += amount;
                }
                else
                {
                    TempHitMultiplier.Add(amount);
                    CurrentMultiplier += amount;
                }
                break;
            case 2:
                if (damage) CurrentDamage += amount;
                else CurrentMultiplier += amount;

                //if (damage) TempAbilitiesDamage.Add(amount);
                //else TempAbilitiesMultiplier.Add(amount);
                //CurrentDamage += amount;
                break;
        }
    }

    public void ResetTempHits()
    {
        for (int i = 0; i < TempHitDamage.Count; i++)
        {
            CurrentDamage -= TempHitDamage[i];
            TempHitDamage.Remove(i);
        }

        for (int i = 0; i < TempHitMultiplier.Count; i++)
        {
            CurrentMultiplier -= TempHitMultiplier[i];
            TempHitMultiplier.Remove(i);
        }
    }
}
