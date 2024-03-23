using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleUIRock : MonoBehaviour
{
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
        transform.forward = cam.transform.forward;
    }

    public void AnimDestroy()
    {
        Destroy(gameObject);
    }
}
