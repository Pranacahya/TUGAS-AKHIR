using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickedProblem : MonoBehaviour
{
    public GameObject myGO;
    public void Activate()
    {
        myGO.SetActive(true);
    }

    public void Deactivate()
    {
        myGO.SetActive(false);
    }
}
