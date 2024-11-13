using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AttackManager : MonoBehaviour
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
    public List<float> TempHitSpeed = new List<float>();

    [Header("Ability Stats")]
    public float AbilityHitSpeed = 0;
    public float AbilityGuardSpeed = 0;


    private void Start()
    {
        CurrentDamage = BaseDamage;
        CurrentMultiplier = BaseMultiplier;
    }

    public void AddTempDamage(int type, float amount, bool damage)
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
                    Debug.Log("Amount: " + amount);
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

    public void ResetTempDamage()
    {
        Debug.Log("Removing");
        if (TempHitDamage.Count > 0)
        {
            for (int i = 0; i < TempHitDamage.Count; i++)
            {
                Debug.Log("Damage: " + TempHitDamage[i]);

                CurrentDamage -= TempHitDamage[i];
                TempHitDamage.RemoveAt(i);
            }
        }
         
        if (TempHitMultiplier.Count > 0)
        {
            for (int i = 0; i < TempHitMultiplier.Count; i++)
            {
                Debug.Log("Multiplier: " + TempHitMultiplier[i]);
                CurrentMultiplier -= TempHitMultiplier[i];
                TempHitMultiplier.RemoveAt(i);
            }
        }
    }

    public void AddTempHitSpeed(Animator animator, int type, float amount)
    {
        switch(type)
        {
            case 1: // Temp hit
                animator.speed += amount;
                TempHitSpeed.Add(amount);
                break;
            case 2: // Ability offensive
                AbilityHitSpeed += amount;
                break;
            case 3: // Ability defensive
                AbilityGuardSpeed += amount;
                break;
        }
    }

    public void ResetTempHitSpeed(Animator animator)
    {
        if (TempHitSpeed.Count > 0)
        {
            for (int i = 0; i < TempHitSpeed.Count; i++)
            {
                Debug.Log("Multiplier: " + TempHitSpeed[i]);
                animator.speed -= TempHitSpeed[i];
                TempHitSpeed.RemoveAt(i);
            }
        }
    }

    
}
