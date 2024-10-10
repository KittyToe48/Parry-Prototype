using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    Noise noise;
    [SerializeField] GameObject noisePrefab;

    PunchDamage damage;

    int punchCheck = 0;

    public bool ParryState = false;
    public bool GuardState = false;

    ChaserAI enemyAI;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        noise = GetComponent<Noise>();

        enemyAI = transform.GetComponentInParent<ChaserAI>();

        damage = transform.GetChild(0).GetComponent<PunchDamage>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "Player") PunchUp();
        if (Input.GetMouseButtonDown(1) && gameObject.tag == "Player") HeavyPunchUp();
        if (Input.GetKeyDown(KeyCode.R) && gameObject.tag == "Player") GuardUp();
        if (Input.GetKeyDown(KeyCode.J) && gameObject.tag == "Player") Whistle();
    }

    public void PunchUp()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Punch Up");
            punchCheck = 0;
            damage.DamageMultiplier = 1;
            damage.Damage = 10;
            //Debug.Log("Punch Up: " + damage.Damage + ", Multiplier: " + damage.DamageMultiplier);
        }
    }

    void HeavyPunchUp()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            damage.DamageMultiplier = 1;
            damage.Damage = 45;
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
            damage.DamageMultiplier = 2;
            animator.SetTrigger("Punch Down");
            punchCheck = 0;
        }
    }

    public void PunchDown()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) animator.SetBool("Punch Down", false);
        punchCheck = 0;
    }

    public void GuardUp()
    {
        animator.SetTrigger("Guard Up");
    }

    public void GuardDown()
    {
        animator.SetTrigger("Guard Down");
    }

    void Whistle()
    {
        noise.CreateNoise(10, 1, 1.5f, noisePrefab, transform.position);
    }
}
