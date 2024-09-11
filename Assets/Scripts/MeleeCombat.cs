using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public bool GuardState = false;

    [SerializeField] float _punchTimer;

    [SerializeField] GameObject _punchObject;
    Animation _punchAnim;

    // Start is called before the first frame update
    void Start()
    {
        _punchAnim = _punchObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "Player") Punch();
        if (Input.GetMouseButtonDown(1) && gameObject.tag == "Player") Guard();
    }

    public void Punch()
    {
        _punchAnim.Play();
    }

    public void Guard()
    {
        GuardState = true;
    }

    public void Stunned()
    {
        if (gameObject.tag == "Player")
        {

        }
        else if (gameObject.tag == "Enemy")
        {
            
        }
            
    }

    //public IEnumerator Punch(float punchTimer)
    //{
    //_punchHitBox.enabled = true;
    //yield return new WaitForSeconds(punchTimer);
    //_punchHitBox.enabled = false;
    //}
}
