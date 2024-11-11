using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Scream Stats")]
    [SerializeField] float _screamDamage;
    [SerializeField] float _screamMultiplier;
    [SerializeField] float _screamMoveSpeed;
    public float ScreamAttackSpeed;
    public bool ScreamEnabled = false;

    DamageManager damageManager;

    PlayerMovement playerMovement;

    private void Start()
    {
        damageManager = GetComponent<DamageManager>();
        playerMovement= GetComponent<PlayerMovement>();
    }

    public void ApplyScream()
    {
        Debug.Log("Screaming 1");
        if (!ScreamEnabled)
        {
            Debug.Log("Screaming 2");
            damageManager.AddTemp(2, _screamDamage, true); // Damage
            damageManager.AddTemp(2, _screamMultiplier, false); // Multiplier

            playerMovement.PlayerSpeed += _screamMoveSpeed;

            ScreamEnabled = true;
        }
    }

    public void ResetScream()
    {
        ScreamEnabled = false;
        damageManager.CurrentDamage -= _screamDamage;
        damageManager.CurrentMultiplier-= _screamMultiplier;
        playerMovement.PlayerSpeed -= _screamMoveSpeed;
    }
}
