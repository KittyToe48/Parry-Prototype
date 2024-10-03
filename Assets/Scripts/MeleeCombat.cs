using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    PunchDamage damage;

    int punchCheck = 0;

    public bool ParryState = false;
    public bool GuardState = false;

    ChaserAI enemyAI;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = transform.GetComponentInParent<ChaserAI>();

        damage = transform.GetChild(0).GetComponent<PunchDamage>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "Player") PunchUp();
        if (Input.GetMouseButtonDown(1) && gameObject.tag == "Player") GuardUp();
    }

    public void PunchUp()
    {
        punchCheck = 0;
        damage.DamageMultiplier = 1;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) animator.SetTrigger("Punch Up");
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

    //public IEnumerator Punch(float punchTimer)
    //{
    //_punchHitBox.enabled = true;
    //yield return new WaitForSeconds(punchTimer);
    //_punchHitBox.enabled = false;
    //}
}
