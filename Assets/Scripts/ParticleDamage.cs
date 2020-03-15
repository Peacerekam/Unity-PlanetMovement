using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        if (numCollisionEvents > 0) {
            if (other.CompareTag("Planet")) {
                Instantiate(explosionPrefab, collisionEvents[0].intersection, Quaternion.identity);
            } else if (other.CompareTag("Player")) {
                other.GetComponent<PlayerBehaviour>().KillPlayer();
            }
        }

    }
}
