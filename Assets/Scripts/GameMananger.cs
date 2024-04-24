using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMananger : MonoBehaviour
{
    public static GameMananger instance {  get; private set; }
    [SerializeField] private Text textAmountOfRock;
    private float minWait = 3f;
    private float maxWait = 25f;
    private int amountOfRock;
    private GameObject rockPrefab;
    private void Start()
    {
        instance = this;
    }

    public void GetParentRock(Transform parent, GameObject rock)
    {
        StartCoroutine(UpdateNewRock(parent, rock));
    }
    private IEnumerator UpdateNewRock(Transform transform, GameObject rock)
    {
        Vector3 pos = transform.position;
        Quaternion rotate = transform.rotation;
        float randomWait = UnityEngine.Random.Range(minWait, maxWait);
        yield return new WaitForSeconds(randomWait);
        GameObject newRock = Instantiate(rock, pos, rotate);
    }

    public void IncreaseRocks(int amount)
    {
        amountOfRock += amount;
        textAmountOfRock.text = "Rock: " + amountOfRock;
    }
}
