using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickedProblem : MonoBehaviour
{
    
    [SerializeField]
    private GameObject myGO;

    [SerializeField]
    private GameObject eraserGO;

    [SerializeField]
    private GameObject handRB;

    public void Activate()
    {
        myGO.SetActive(true);
    }

    public void Deactivate()
    {
        myGO.SetActive(false);
    }

    private void Update()
    {
        if(handRB.activeSelf)
        {
            eraserGO.SetActive(true);
        }
        else
        {
            eraserGO.SetActive(false);
        }
    }
}
