using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Scream Stats")]
    [SerializeField] float _screamDamage;
    [SerializeField] float _screamMultiplier;
    [SerializeField] float _screamMoveSpeed;
    [SerializeField] float _screamAttackSpeed;
    public bool ScreamEnabled = false;

    AttackManager attackManager;

    PlayerMovement playerMovement;

    private void Start()
    {
        attackManager = GetComponent<AttackManager>();
        playerMovement= GetComponent<PlayerMovement>();
    }

    public void ApplyScream(Animator animator)
    {
        Debug.Log("Screaming 1");
        if (!ScreamEnabled)
        {
            Debug.Log("Screaming 2");
            attackManager.AddTempDamage(2, _screamDamage, true); // Damage
            attackManager.AddTempDamage(2, _screamMultiplier, false); // Multiplier
            attackManager.AddTempHitSpeed(animator, 2, _screamAttackSpeed); // Hitspeed

            playerMovement.PlayerSpeed += _screamMoveSpeed;

            ScreamEnabled = true;
        }
    }

    public void ResetScream(Animator animator)
    {
        Debug.Log("Reset scream");
        ScreamEnabled = false;
        attackManager.CurrentDamage -= _screamDamage; 
        attackManager.CurrentMultiplier -= _screamMultiplier;
        attackManager.AbilityHitSpeed -= _screamAttackSpeed; // N�r den h�r variablen blir till 0 kan den inte ta bort speed fr�n animationen. Animationen m�ste s�ttas tillbaka till dess base speed.
        animator.speed = attackManager.BaseHitSpeed;
        playerMovement.PlayerSpeed -= _screamMoveSpeed;
    }
}
