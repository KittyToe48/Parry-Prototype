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

    AttackManager attackManager;

    PlayerMovement playerMovement;

    private void Start()
    {
        attackManager = GetComponent<AttackManager>();
        playerMovement= GetComponent<PlayerMovement>();
    }

    public void ApplyScream()
    {
        Debug.Log("Screaming 1");
        if (!ScreamEnabled)
        {
            Debug.Log("Screaming 2");
            attackManager.AddTempDamage(2, _screamDamage, true); // Damage
            attackManager.AddTempDamage(2, _screamMultiplier, false); // Multiplier

            playerMovement.PlayerSpeed += _screamMoveSpeed;

            ScreamEnabled = true;
        }
    }

    public void ResetScream()
    {
        ScreamEnabled = false;
        attackManager.CurrentDamage -= _screamDamage;
        attackManager.CurrentMultiplier-= _screamMultiplier;
        playerMovement.PlayerSpeed -= _screamMoveSpeed;
    }
}
