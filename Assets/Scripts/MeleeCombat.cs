using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public bool ParryState = false;
    public bool GuardState = false;
    public bool Stunned = false;

    [SerializeField] float _punchTimer;

    [SerializeField] GameObject _punchObject;
    Animator _animator;

    // ToDo: Testa med att sätta meleecombat i armarna.

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        
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
        _animator.SetTrigger("Punch Trigger");
    }

    public void GuardUp()
    {
        _animator.SetTrigger("Guard Trigger");
    }

    public void GuardDown()
    {
        _animator.StopPlayback();
        _animator.SetTrigger("Guard Trigger");
    }

    //public void Stunned() //Gör om till coroutine
    //{
    //    if (gameObject.tag == "Player")
    //    {

    //    }
    //    else if (gameObject.tag == "Enemy")
    //    {
            
    //    }
            
    //}

    //public IEnumerator Punch(float punchTimer)
    //{
    //_punchHitBox.enabled = true;
    //yield return new WaitForSeconds(punchTimer);
    //_punchHitBox.enabled = false;
    //}
}
