using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    PunchDamage _damage;

    int _punchCheck = 0;

    public bool ParryState = false;
    public bool GuardState = false;
    public bool Stunned = false;

    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _damage = GetComponent<PunchDamage>();
        _animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "Player") PunchUp();
        if (Input.GetMouseButtonUp(0) && gameObject.tag == "Player") PunchDown();
        if (Input.GetMouseButtonDown(1) && gameObject.tag == "Player") GuardUp();
        if (Input.GetMouseButtonUp(1) && gameObject.tag == "Player") GuardDown();
    }

    public void PunchUp()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) _animator.SetTrigger("Punch Up");
    }

    void PunchCheck()
    {
        _punchCheck++;
        Debug.Log(_punchCheck);
        if (_punchCheck == 3)
        {
            _animator.SetTrigger("Punch Down");
            _punchCheck = 0;
        }
    }

    public void PunchDown()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) _animator.SetTrigger("Punch Down");
        _punchCheck = 0;
    }

    public void GuardUp()
    {
        _animator.SetTrigger("Guard Up");
    }

    public void GuardDown()
    {
        _animator.SetTrigger("Guard Down");
    }

    public IEnumerator StunnedTimer() //Gör om till coroutine
    {
        Debug.Log("Arggh I am stunned: " + gameObject.tag);
        Stunned = true;
        if (gameObject.tag == "Player")
        {
            yield return new WaitForSeconds(2);
        }
        else if (gameObject.tag == "Enemy")
        {
            yield return new WaitForSeconds(2);
        }
        Stunned = false;
    }

    //public IEnumerator Punch(float punchTimer)
    //{
    //_punchHitBox.enabled = true;
    //yield return new WaitForSeconds(punchTimer);
    //_punchHitBox.enabled = false;
    //}
}
