using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    NoiseBehaviour noise;
    [SerializeField] GameObject noisePrefab;

    [SerializeField] float _screamCooldown = 10;
    bool hasScreamed;

    [SerializeField] DamageManager damage;

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
        if (Input.GetKeyDown(KeyCode.J) && gameObject.tag == "Player") Whistle();
        if (Input.GetKeyDown(KeyCode.H) && gameObject.tag == "Player") Whistle();
    }

    public void PunchUp()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Punch Up");
            punchCheck = 0;
            //Debug.Log("Punch Up: " + damage.Damage + ", Multiplier: " + damage.DamageMultiplier);
        }
    }

    void HeavyPunchUp()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            damage.AddTemp(1, 40, true);
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
            damage.AddTemp(1, 1, false);
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
        noise.CreateNoise(10, 2, 1.5f, noisePrefab, transform.position);
    }

    void Scream()
    {
        if (!hasScreamed)
        {




            StartCoroutine(ScreamCooldown());
        }
        hasScreamed = true;
        noise.CreateNoise(20, 3, 1, noisePrefab, transform.position);
    }

    IEnumerator ScreamCooldown()
    {
        yield return new WaitForSeconds(_screamCooldown);
        hasScreamed = false;
    }
}
