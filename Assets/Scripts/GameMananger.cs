using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananger : MonoBehaviour
{
    public static GameMananger instance {  get; private set; }
    [SerializeField] private Rock rockPrefab;
    [SerializeField] private List<Transform> locationRocks = new List<Transform>();
    private float minWait = 10f;
    private float maxWait = 45f;
    private void Start()
    {
        instance = this;
        SpawningRocks();
    }

    private void SpawningRocks()
    {
        for (int i = 0; i < locationRocks.Count; i++)
        {
            if (locationRocks[i].childCount <= 0)
            {
                InstantiateNewRock(locationRocks[i]);
            }
        }
    }

    private void InstantiateNewRock(Transform transform)
    {
        Rock rock = Instantiate(rockPrefab, transform);
        rock.transform.localRotation = Quaternion.Euler(0,UnityEngine.Random.Range(0,180), 0);
    }
    public void GetParentRock(Transform parent)
    {
        StartCoroutine(UpdateNewRock(parent));
    }
    private IEnumerator UpdateNewRock(Transform transform)
    {
        float randomWait = UnityEngine.Random.Range(minWait, maxWait);
        yield return new WaitForSeconds(randomWait);
        Rock rock = Instantiate(rockPrefab, transform);
        rock.transform.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 180), 0);
    }
}
