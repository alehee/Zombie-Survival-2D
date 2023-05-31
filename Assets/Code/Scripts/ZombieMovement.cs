using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    public float speed = 1f;
    SpriteRenderer spriteRenderer;
    float horizontalMovement;

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        spriteRenderer = gameObject.transform.Find("Model").gameObject.GetComponent<SpriteRenderer>();
        horizontalMovement = gameObject.transform.position.x;
    }

    void Update()
    {
        agent.SetDestination(target.position);

        float newHorizontalMovement = gameObject.transform.position.x;
        if (newHorizontalMovement > horizontalMovement)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
        horizontalMovement = newHorizontalMovement;
    }
}
