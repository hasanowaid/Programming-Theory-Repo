using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class WorkerController : MonoBehaviour
{
    protected NavMeshAgent agent;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float speedMining = 1f;
    [SerializeField] protected float amountMining = 1f;
    [SerializeField] protected float workDistance = 5f;
    [SerializeField] protected float checkDistance = 10f;
    protected GameObject workerSelected;
    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    public void OnFire()
    {
        ClickMousePosition();
    }

    protected virtual void ClickMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.CompareTag("Worker")) 
                UnselectedWorker();
            if (hit.collider.CompareTag("Rock"))
            {
                agent.isStopped = false;
                agent.destination = hit.point;
                ChoosingRock(hit.collider.GetComponent<Rock>());
            }
        }
    }
    public abstract void ChoosingRock(Rock rock);
    protected abstract void UnselectedWorker();
}
