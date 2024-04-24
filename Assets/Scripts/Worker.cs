using System;
using System.Collections;
using UnityEngine;

public class Worker : WorkerController
{
    [SerializeField] private GameObject iconSelected;
    private Vector3 currentPosition;
    private bool selected = false;
    public bool IsWorking { get; private set; }
    public float SpeedMining => speedMining;

    public override void Start()
    {
        currentPosition = transform.position;
        base.Start();
    }
    private void OnMouseDown()
    {
        iconSelected.SetActive(true);
        selected = !selected;
    }
    protected override void UnselectedWorker()
    {
        if (selected)
        {
            iconSelected.SetActive(false);
            selected = false;
        }
    }
    protected override void ClickMousePosition()
    {
        if(selected)
        {
            base.ClickMousePosition();
        }
    }

    private bool UpdateDistance(Vector3 point, float distance)
    {
        float magntude = Vector3.Magnitude(point - transform.position);
        return magntude < distance;
    }

    public void ReturnToFactor()
    {
        animator.SetBool("Mining", false);
        selected = false;
        agent.isStopped = false;
        agent.destination = currentPosition;
    }

    public override void ChoosingRock(Rock rock)
    {
        iconSelected.SetActive(false);
        StartCoroutine(CheckDistiance(rock));
    }

    private IEnumerator CheckDistiance(Rock rock)
    {

        while (!UpdateDistance(rock.transform.position, checkDistance)) yield return null;
        if (rock.IsAvailable)
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(2f);
            ReturnToFactor();
        }
        else
        {
            while (!UpdateDistance(rock.transform.position, workDistance)) yield return null;
            animator.SetBool("Mining", true);
            agent.isStopped = true;
            rock.Mining(this);
        }
    }
}


