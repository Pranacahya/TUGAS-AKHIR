using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.DetectionExamples;
using Leap.Unity;

public class EraserScript : MonoBehaviour
{
    [SerializeField]
    PinchDraw pinchDraw;

    [SerializeField]
    PinchDetector pd;

    private bool isOff = true;

    IEnumerator timer()
    {
        isOff = false;
        yield return new WaitForSeconds(1f);
        isOff = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (isOff)
        {
            if (this.gameObject.name == "Sphere" && collision.gameObject.name == "Contact Palm Bone" && !pd.IsActive)
            {
                Debug.Log("erase");
                pinchDraw.DeletAllLine();
                StartCoroutine(timer());
            }

            if (this.gameObject.name == "UndoSphere" && collision.gameObject.name == "Contact Palm Bone" && !pd.IsActive)
            {
                Debug.Log("undo");
                pinchDraw.Undo();
                StartCoroutine(timer());
            }
        }
        
    }
}
