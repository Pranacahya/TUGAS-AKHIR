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
            lineHelper.startWidth = 0.008f;
            lineHelper.endWidth = 0.008f;
            if (_pinchDetectors.Length == 0)
            {
                UnityEngine.Debug.LogWarning("No pinch detectors were specified!  PinchDraw can not draw any lines without PinchDetectors.");
            }
        }
        public void DeleteLine()
        {
            lineHelper.positionCount = 0;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            
            for (int i = 0; i < _pinchDetectors.Length; i++)
            {
                var detector = _pinchDetectors[i];
                if (detector.DidStartHold)
                {
                    startPos = new Vector3(Mathf.Round(detector.transform.position.x * 10f) / 10f, Mathf.Round(detector.transform.position.y * 10f) / 10f, pallete.transform.position.z - 0.01f);
                    //startPis.z = pallete.transform.position.z - 0.01f;
                }

                if(detector.DidRelease)
                {
                    endPos = new Vector3(Mathf.Round(detector.transform.position.x * 10f) / 10f, Mathf.Round(detector.transform.position.y * 10f) / 10f, pallete.transform.position.z - 0.01f);
                    lineHelper.positionCount = 2;
                    lineHelper.SetPosition(0, startPos);
                    lineHelper.SetPosition(1, endPos);
                }
            }
        }
    }
}

