using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public bool ParryState = false;
    public bool GuardState = false;
    public bool Stunned = false;

    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetMouseButtonDown(0) && gameObject.tag == "Player") Punch();
        if (Input.GetMouseButtonDown(1) && gameObject.tag == "Player") GuardUp();
        if (Input.GetMouseButtonUp(1) && gameObject.tag == "Player") GuardDown();
    }

    public void Punch()
    {
        if (!ParryState &&  !GuardState) _animator.SetTrigger("Punch Trigger");
    }

    public void GuardUp()
    {
        _animator.SetTrigger("Up Trigger");
    }

    public void GuardDown()
    {
        _animator.SetTrigger("Down Trigger");
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
