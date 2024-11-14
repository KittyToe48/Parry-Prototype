using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    NoiseBehaviour noise;
    [SerializeField] GameObject _noisePrefab;

    [SerializeField] float _screamCooldown = 10;
    bool hasScreamed;

    [SerializeField] AttackManager _attackManager;

    [SerializeField] AbilityManager _abilities;

    int punchCheck = 0;

    public bool ParryState = false;
    public bool GuardState = false;

    ChaserAI enemyAI;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        noise = GetComponent<NoiseBehaviour>();

        enemyAI = transform.GetComponentInParent<ChaserAI>();

        //damage = transform.GetChild(0).GetComponent<PunchDamage>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "Player") PunchUp();
        if (Input.GetMouseButtonDown(1) && gameObject.tag == "Player") HeavyPunchUp();
        if (Input.GetKeyDown(KeyCode.R) && gameObject.tag == "Player") GuardUp();
        if (Input.GetKeyDown(KeyCode.H) && gameObject.tag == "Player") Scream();
        if (Input.GetKeyDown(KeyCode.J) && gameObject.tag == "Player") Whistle();
    }

    public void PunchUp()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _attackManager.ApplyAbilitySpeed(true, animator);
            animator.SetTrigger("Punch Up");
            punchCheck = 0;
            //Debug.Log("Punch Up: " + damage.Damage + ", Multiplier: " + damage.DamageMultiplier);
        }
    }

    void HeavyPunchUp()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _attackManager.ApplyAbilitySpeed(true, animator);
            _attackManager.AddTempDamage(1, 40, true);
            animator.SetTrigger("Heavy Up");
        }
    }

    void PunchCheck()
    {
        if (gameObject.tag == "Enemy") animator.SetTrigger("Punch Down");
        punchCheck++;
        //Debug.Log(_punchCheck);
        if (punchCheck == 3)
        {
            _attackManager.AddTempDamage(1, 1, false); 
            animator.SetTrigger("Punch Down");
            punchCheck = 0;
        }
    }

    //public void PunchDown()
    //{
    //    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) animator.SetBool("Punch Down", false);
    //    punchCheck = 0;
    //}

    public void GuardUp()
    {
        _attackManager.ApplyAbilitySpeed(false, animator);
        animator.SetTrigger("Guard Up");
    }

    public void GuardDown()
    {
        animator.SetTrigger("Guard Down");
    }

    void Whistle()
    {
        noise.CreateNoise(10, 2, 1.5f, _noisePrefab, transform.position);
    }

    void Scream()
    {
        Debug.Log("Screaming 0");
            _abilities.ApplyScream(animator);
            StartCoroutine(ScreamCooldown());
        noise.CreateNoise(20, 3, 1, _noisePrefab, transform.position);
    }

    IEnumerator ScreamCooldown()
    {
        yield return new WaitForSeconds(_screamCooldown);
        _abilities.ResetScream();
    }

    public void AnimationEnd()
    {
        _attackManager.ResetAbilitySpeed(animator);
    }
}
