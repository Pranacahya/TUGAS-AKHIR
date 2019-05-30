using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.DetectionExamples;
public class EraserScript : MonoBehaviour
{
    [SerializeField]
    PinchDraw pinchDraw;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Contact Fingerbone")
        {
            pinchDraw.DeletAllLine();
        }
    }
    // Start is called before the first frame update
   
}
