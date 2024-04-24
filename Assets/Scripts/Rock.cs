using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rock : MonoBehaviour
{
    [SerializeField] private float rockHardness = 5f;
    [SerializeField] private float waitForSeconds = 5f;
    [SerializeField] private int amount = 1;
    [SerializeField] private GameObject UIBar;
    [SerializeField] private GameObject UITextPrefab;
    [SerializeField] private GameObject rockPrefab;
    public bool IsAvailable { get; private set; }

    public void Mining(Worker worker)
    {
        StartCoroutine(BeginMining(worker));
    }

    private IEnumerator BeginMining(Worker worker)
    {
        IsAvailable = true;
        GameObject bar = InstantiateBar();
        Transform barAnim = bar.transform.GetChild(0).transform;
        float miningTime = 0f;
        while (miningTime < rockHardness)
        {
            yield return new WaitForSeconds(waitForSeconds);
            miningTime += worker.SpeedMining;
            float divideBy = worker.SpeedMining / rockHardness;
            barAnim.localScale = new Vector3(barAnim.localScale.x - divideBy, 1, 1);
            TextMiningControl();
            GameMananger.instance.IncreaseRocks(amount);
        }
        Destroy(bar);
        worker.ReturnToFactor();
        OnWorkerFinish();
    }

    private void TextMiningControl()
    {
        GameObject textMining = Instantiate(UITextPrefab, transform);
        textMining.transform.localPosition = Vector3.up; 
        Text getText = textMining.GetComponentInChildren<Text>();
        getText.text = amount.ToString();
    }

    private void OnWorkerFinish()
    {
        GameMananger.instance.GetParentRock(transform, rockPrefab);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }

    private GameObject InstantiateBar()
    {
        Vector3 barPoition = transform.position + Vector3.up;
        return Instantiate(UIBar, barPoition,Quaternion.identity);
    }
}
