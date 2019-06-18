using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonPressed : MonoBehaviour
{
    public TextGenerator tG;
    public TextMeshPro myName;
    public void MyAction()
    {
        SetText(this.gameObject.transform.name);
    }

    private void SetText(string name)
    {
        tG.SetText(name);
    }
}
