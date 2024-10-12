using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseBehaviour : MonoBehaviour
{
    bool alive = false;
    int level = 1;
    float lifeSpan;
    float radius;

    public void CreateNoise(float radius, int level, float lifeSpan, GameObject prefab, Vector3 position)
    {
        GameObject Noise = Instantiate(prefab, position, transform.rotation);
        Noise.GetComponent<SphereCollider>().radius = radius;
        Noise.GetComponent<NoiseBehaviour>().radius = radius;
        Noise.GetComponent<NoiseBehaviour>().level = level;
        Noise.GetComponent<NoiseBehaviour>().lifeSpan = lifeSpan;
        Noise.GetComponent<NoiseBehaviour>().alive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (alive) StartCoroutine(NoiseLife());
    }

    IEnumerator NoiseLife()
    {
        while (lifeSpan > 0)
        {
            lifeSpan -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && alive)
        {
            other.transform.LookAt(transform.position);
            RaycastHit[] hits = new RaycastHit[5];
            int result = Physics.RaycastNonAlloc(transform.position, other.transform.position - transform.position, hits, radius, 1 << 7);
            Debug.Log("Level: " + level + ", Raycast: " + result);
            
            if (result < level)
            {
                other.GetComponent<ChaserAI>().InvestigateNoise(transform.position);
            }
            Debug.Log(hits[0].transform.name);
        }
    }
}
