using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    [SerializeField] GameObject _dummy;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _pucci;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Instantiate(_dummy, new Vector3(5, 1, 0), transform.rotation);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Instantiate(_enemy, new Vector3(0, 1, 0), transform.rotation);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Instantiate(_pucci, new Vector3(0, 1, 0), transform.rotation);
    }
}
