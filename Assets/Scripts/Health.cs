using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool _immortal;

    [SerializeField] TextMeshProUGUI _healthText;
    //[SerializeField] TextMeshProUGUI _scoreText;
    float _health, _damageBuffer, _damageBufferValue = 0.5f;
    [SerializeField] float _maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        _health = _maxHealth;
        if (gameObject.tag == "Player")
        {
            _healthText.text = _health.ToString() + " / " + _maxHealth;
        }
        _damageBuffer = _damageBufferValue;
    }

    // Update is called once per frame
    void Update()
    {
        //_damageBuffer -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (!_immortal)
        {
            _health -= damage;
            if (gameObject.tag == "Player")
            {
                _healthText.text = _health.ToString() + " / " + _maxHealth;
            }
            if (_health <= 0)
            {
                Death();
            }
            _damageBuffer = _damageBufferValue;
        }
    }

    public void TakeHealth(float regen)
    {
        _health += regen;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        //_healthText.text = _health.ToString() + " / " + _maxHealth;
    }

    void Death()
    {
        //if (gameObject.tag == "Player") Application.Quit();
        Destroy(gameObject);

    }
}
