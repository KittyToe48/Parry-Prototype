using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public float CurrentDamage = 0;
    public float CurrentMultiplier = 1;
    public float BaseDamage;
    public float AbilityDamage = 0;
    public float BaseMultiplier;
    public float AbilityMultiplier = 0;

    public List<float> TempHitDamage = new List<float>();
    public List<float> TempHitMultiplier = new List<float>();
    public List<float> TempAbilitiesDamage = new List<float>();
    public List<float> TempAbilitiesMultiplier = new List<float>();

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
