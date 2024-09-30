using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitNumber : MonoBehaviour
{
    GameObject _player;
    
    [SerializeField] float _lifeTimer;

    TextMeshPro _damageText;
    [HideInInspector] public float Damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _damageText = GetComponent<TextMeshPro>();
        _damageText.text = Damage.ToString();
        Debug.Log("I exist:" + Damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (_lifeTimer <= 0) Destroy(gameObject);
        else _lifeTimer -= Time.deltaTime;
        //transform.LookAt(Camera.main.transform);
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        //transform.LookAt(new Vector3(_player.transform.position.x, 0, _player.transform.position.y));
    }
}
