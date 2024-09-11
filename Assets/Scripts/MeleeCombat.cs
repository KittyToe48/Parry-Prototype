using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
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
    }

    public void Punch()
    {
        _punchAnim.Play();
    }

    //public IEnumerator Punch(float punchTimer)
    //{
    //_punchHitBox.enabled = true;
    //yield return new WaitForSeconds(punchTimer);
    //_punchHitBox.enabled = false;
    //}
}
