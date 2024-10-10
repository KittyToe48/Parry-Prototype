using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [HideInInspector] public bool Alive = false;
    [HideInInspector] public int Level = 1;
    [HideInInspector] public float LifeSpan;

    public void CreateNoise(float radius, int level, float lifeSpan, GameObject prefab, Vector3 position)
    {
        GameObject Noise = Instantiate(prefab, position, transform.rotation);
        Noise.GetComponent<SphereCollider>().radius = radius;
        Noise.GetComponent<Noise>().Level = level;
        Noise.GetComponent<Noise>().LifeSpan = lifeSpan;
        Noise.GetComponent<Noise>().Alive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Alive) StartCoroutine(NoiseLife());
    }

    IEnumerator NoiseLife()
    {
        while (LifeSpan > 0)
        {
            LifeSpan -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collided: " + other.name);
        if (other.tag == "Enemy" && Alive)
        {
            Debug.Log("Heard");
            other.transform.LookAt(transform.position);
            RaycastHit[] hits = new RaycastHit[5];
            if (Physics.RaycastNonAlloc(transform.position, other.transform.position - transform.position, hits, 100, 1<<7) < Level)
            {
                other.GetComponent<ChaserAI>().InvestigateNoise(transform.position);
            }
        }
    }
}
