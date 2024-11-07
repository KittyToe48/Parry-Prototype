using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Scream Stats")]
    [SerializeField] float _screamDamage;
    [SerializeField] float _screamMultiplier;


    DamageManager damageManager;

    private void Start()
    {
        damageManager = GetComponent<DamageManager>();
    }

    public void ApplyScream()
    {
        damageManager.AddTemp(2, _screamDamage, true); // Damage
        damageManager.AddTemp(2, _screamMultiplier, false); // Multiplier
    }

    public void ResetScream()
    {
        damageManager.CurrentDamage -= _screamDamage;
        damageManager.CurrentMultiplier-= _screamMultiplier;
    }
}
