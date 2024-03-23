using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rock : MonoBehaviour
{
    [SerializeField] private float rockHardness = 5f;
    [SerializeField] private float waitForSeconds = 5f;
    [SerializeField] private float speedAlpha = 2f;
    [SerializeField] private Transform UIMining;
    [SerializeField] private GameObject UIBar;
    [SerializeField] private GameObject UITextPrefab;
    public bool IsAvailable { get; private set; }
    private void Start()
    {
        UIBarControl(false);
    }
    public void Mining(Worker worker)
    {
        StartCoroutine(BeginMining(worker));
    }

    private IEnumerator BeginMining(Worker worker)
    {
        IsAvailable = true;
        UIBarControl(true);
        float miningTime = 0f;
        while (miningTime < rockHardness)
        {
            yield return new WaitForSeconds(waitForSeconds);
            miningTime += worker.SpeedMining;
            float divideBy = worker.SpeedMining / rockHardness;
            UIMining.localScale = new Vector3(UIMining.localScale.x - divideBy, 1, 1);
            TextMiningControl(worker.AmountMining);
        }
        worker.ReturnToFactor();
        OnWorkerFinish();
    }

    private void TextMiningControl(float amountMining)
    {
        GameObject textMining = Instantiate(UITextPrefab, transform);
        textMining.transform.localPosition = Vector3.up; 
        Text getText = textMining.GetComponentInChildren<Text>();
        getText.text = amountMining.ToString();
    }

    private void OnWorkerFinish()
    {
        Transform ParentPosition = transform.parent;
        GameMananger.instance.GetParentRock(ParentPosition);
        UIBarControl(false);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }

    private void UIBarControl(bool active)
    {
        UIBar.gameObject.SetActive(active);
    }
}
