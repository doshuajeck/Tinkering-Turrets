using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private float initialSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        initialSpeed = agent.speed;
        target = GameObject.FindGameObjectWithTag("Goal").transform;
        Invoke("Move", 0.1f);
    }

    public IEnumerator SlowEffect()
    {
        agent.speed = initialSpeed / 2;
        yield return new WaitForSeconds(3f);
        agent.speed = initialSpeed;
    }
    private void Move()
    {
        agent.SetDestination(target.position);
    }
}
