using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseBehaviour : MonoBehaviour
{
    [HideInInspector] public bool Alive = false;
    [HideInInspector] public int Level = 1;
    [HideInInspector] public float LifeSpan;

    public void CreateNoise(float radius, int level, float lifeSpan, GameObject prefab, Vector3 position)
    {
        GameObject Noise = Instantiate(prefab, position, transform.rotation);
        Noise.GetComponent<SphereCollider>().radius = radius;
        Noise.GetComponent<NoiseBehaviour>().Level = level;
        Noise.GetComponent<NoiseBehaviour>().LifeSpan = lifeSpan;
        Noise.GetComponent<NoiseBehaviour>().Alive = true;
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

    private void OnTriggerEnter(Collider other)
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
