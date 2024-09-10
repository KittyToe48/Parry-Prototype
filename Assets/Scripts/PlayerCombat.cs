using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float _punchTimer;

    [SerializeField] GameObject _punchObject;
    BoxCollider _punchHitBox;

    // Start is called before the first frame update
    void Start()
    {
        _punchHitBox = _punchObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartCoroutine(Punch());
    }

    IEnumerator Punch()
    {
        _punchHitBox.enabled = true;
        yield return new WaitForSeconds(_punchTimer);
        _punchHitBox.enabled = false;
    }
}
