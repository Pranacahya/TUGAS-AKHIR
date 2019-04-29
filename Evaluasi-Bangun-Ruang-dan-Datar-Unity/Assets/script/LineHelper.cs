using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity.DetectionExamples
{

    public class LineHelper : MonoBehaviour
    {

        [SerializeField]
        private PinchDetector[] _pinchDetectors;

        [SerializeField]
        private Transform pallete;

        private Vector3 startPos;
        private Vector3 endPos;
        private Vector3[] positions;
        private int counter = 2;
        LineRenderer lineHelper;
        // Start is called before the first frame update
        void Awake()
        {

            lineHelper = this.gameObject.AddComponent<LineRenderer>();
            lineHelper.SetWidth(0.008f, 0.008f);
            if (_pinchDetectors.Length == 0)
            {
                UnityEngine.Debug.LogWarning("No pinch detectors were specified!  PinchDraw can not draw any lines without PinchDetectors.");
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
            for (int i = 0; i < _pinchDetectors.Length; i++)
            {
                var detector = _pinchDetectors[i];
                if (detector.DidStartHold)
                {
                    startPos = detector.Position;
                    startPos.z = pallete.transform.position.z - 0.01f;
                }

                if(detector.DidRelease)
                {
                    endPos = detector.Position;
                    endPos.z = pallete.transform.position.z - 0.01f;
                    lineHelper.positionCount = 2;
                    //positions[0] = startPos;
                    //positions[1] = endPos;
                    //lineHelper.positionCount = positions.Length;
                    lineHelper.SetPosition(0, startPos);
                    lineHelper.SetPosition(1, endPos);
                }
            }
        }
    }
}

