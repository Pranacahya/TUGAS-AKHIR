using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshProRule : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = mainCamera.transform.rotation;
    }
}
