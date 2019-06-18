using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using TMPro;
using System.Linq;
using System.Collections;
using Debug = UnityEngine.Debug;

namespace Leap.Unity.DetectionExamples
{
    public class PinchDraw : MonoBehaviour
    {
        //public HandsCollider handCol = new HandsCollider();
        [Tooltip("Each pinch detector can draw one line at a time.")]

        private LinkedList<Vector3> points;

        private IDrawable _drawAble;

        [SerializeField]
        TMP_Text tmWin;

        //Fungsi pinch detectir dari leap motion
        [SerializeField]
        private PinchDetector[] _pinchDetectors;

        private  LineScoring lineScore;

        //Material untuk garis
        [SerializeField]
        private Material _material;

        //Warna garis
        [SerializeField]
        private Color _drawColor = Color.white;

        //Refresh rate menggambar garis
        [SerializeField]
        private float _smoothingDelay = 0.01f;

        //jari-jari lingkaran garis
        [SerializeField]
        private float _drawRadius = 0.002f;

        //?????????
        [SerializeField]
        private int _drawResolution = 8;

        //?????????
        [SerializeField]
        private float _minSegmentLength = 0.005f;

        //GameObject palette untuk menggambar
        [SerializeField]
        private GameObject _palette;

        [SerializeField]
        ScoringDisplay scorePopUp = new ScoringDisplay();

        public TextMeshPro myName;
        public GameObject whiteBoard;
        public GameObject crosshair;
        public GameObject sphere;
        public GameObject sphere2;
        public ExamDisplayer examDisp;
        public GameObject soalUI;
        public GameObject leftHand;


        //private List<float[]> pointsToCheck = new List<float[]>();
        private bool selesaiBool;
        string shape;
        int tempScore;
        List<int> undoScore = new List<int>();
        int totalGambar;
        private bool shaped = false;
        private List<GameObject> ballPoint = new List<GameObject>();
        private LineHelper lineHelper;
        private List<float[]> pointsToCheck = new List<float[]>();
        private bool drawingStatus;
        private bool release = false;
        private string jawaban;
        private SoalClass presentSoal;
        private float[] Angle;
        private List<Vector2> arrayPoint = new List<Vector2>();
        private GameObject mySphere;
        private GameObject mySphere2;
        private Rigidbody rb;
        private int soalCounter;
        private Mesh _meshHelper;
        private DrawState[] _drawStates;
        private Vector3 helpPosStart;
        private Vector3 helpPosEnd;
        private Vector3 startPoint = Vector3.zero;
        private Vector2 releasePos;
        private RaycastDraw RD = new RaycastDraw();
        private bool isHold = false;
        private bool isRelease = false;
        private List<GameObject> prepareToUndo = new List<GameObject>();
        //helperline attribute
        public Transform LineTransform;
        private LineRenderer currentLineRender;
        public RenderTexture RTexture;
        private int countVertex = 0;
        void Start()
        {
            selesaiBool = false;
            tmWin.gameObject.SetActive(false);
            totalGambar = 0;
            tempScore = 0;
            soalCounter = 0;
            myName.text = PresentUser.Name;
            lineHelper = this.GetComponent<LineHelper>();
            lineScore = new LineScoring();
            drawingStatus = false;
            //Cek apakah leapmotion membaaca adanya fungsi pinch
            _drawStates = new DrawState[_pinchDetectors.Length];
            for (int i = 0; i < _pinchDetectors.Length; i++)
            {
                _drawStates[i] = new DrawState(this);
            }
            Debug.Log(PresentUser.Name);
        }

        public void Update()
        {
            if(leftHand.activeSelf)
            {
                soalUI.SetActive(true);
            }
            else
            {
                soalUI.SetActive(false);
            }
        }


        //Line configruation
        public float DrawRadius
        {
            get
            {
                return _drawRadius;
            }
            set
            {
                _drawRadius = value;
            }
        }

        public static Stopwatch stopWatch = new Stopwatch();
        private Mesh _mesh;

        void OnValidate()
        {
            _drawRadius = Mathf.Max(0, _drawRadius);
            _drawResolution = Mathf.Clamp(_drawResolution, 3, 24);
            _minSegmentLength = Mathf.Max(0, _minSegmentLength);
        }
        void Awake()
        {
            points = new LinkedList<Vector3>();
           
            //drawAble = new  TriDrawable();
            if (_pinchDetectors.Length == 0)
            {
                UnityEngine.Debug.LogWarning("No pinch detectors were specified!  PinchDraw can not draw any lines without PinchDetectors.");
            }
        }

        private IEnumerator ActivateOnHold()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);
                isHold = false;
            }
        }

        private IEnumerator ActivateRelease()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                release= false;
            }
        }


        void FixedUpdate()
        {
            Vector2 tempPos;
            if(examDisp.jumlahSoal <0 && selesaiBool == false)
            {
                Selesai();
                selesaiBool = true;
            }
            else if(selesaiBool == false)
            {
                for (int i = 0; i < _pinchDetectors.Length; i++)
                {
                    var detector = _pinchDetectors[i];
                    var drawState = _drawStates[i];
                    if (detector.DidStartHold && isHold == false)
                    {
                        isHold = true;
                        StartCoroutine(ActivateOnHold());
                        Rigidbody sphereRB;
                        Rigidbody sphereRB2;

                        //Jika pengguna mulai menggambar
                        if (drawingStatus == false || ballPoint.Count == 0)
                        {
                            Vector2 pos = new Vector2((Mathf.Round(detector.transform.position.x * 10f) / 10f) + 1f, (Mathf.Round(detector.transform.position.y * 10f) / 10f) - 0.4f);
                            arrayPoint.Add(pos);
                            float[] tempPos12 = { (Mathf.Round(detector.transform.position.x * 10f) / 10f) + 1f, (Mathf.Round(detector.transform.position.y * 10f) / 10f) - 0.4f };
                            pointsToCheck.Add(tempPos12);
                            drawingStatus = true;
                            //Bola bantu inisiasi
                            mySphere2 = Instantiate(sphere2, new Vector3(detector.transform.position.x, detector.transform.position.y, _palette.transform.position.z), Quaternion.identity);
                            mySphere2.AddComponent<PositionsDetector>();
                            mySphere2.AddComponent<Rigidbody>();
                            mySphere2.tag = "sphere";
                            sphereRB2 = mySphere2.GetComponent<Rigidbody>();
                            sphereRB2.freezeRotation = true;
                            sphereRB2.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
                                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                            Vector3 spherePos = new Vector3(Mathf.Round(detector.transform.position.x * 10f) / 10f, Mathf.Round(detector.transform.position.y * 10f) / 10f, Mathf.Round(_palette.transform.position.z * 10f) / 10f);
                            mySphere2.transform.position = spherePos;
                            ballPoint.Add(mySphere2);
                        }
                        
                        //Bola bantu
                        mySphere = Instantiate(sphere, new Vector3(detector.transform.position.x, detector.transform.position.y, _palette.transform.position.z), Quaternion.identity);
                        mySphere.AddComponent<PositionsDetector>();
                        mySphere.AddComponent<Rigidbody>();
                        mySphere.tag = "sphere";
                        sphereRB = mySphere.GetComponent<Rigidbody>();
                        sphereRB.freezeRotation = true;
                        sphereRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
                            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        ballPoint.Add(mySphere);
                        points.Clear();
                        points.AddLast(detector.Position);
                        GameObject temp = drawState.BeginNewLine();
                        prepareToUndo.Add(temp);
                    }
                    if (detector.IsHolding )
                    {
                        float[] tempPos12 = { detector.transform.position.x + 1f, detector.transform.position.y - 0.4f };
                        pointsToCheck.Add(tempPos12);
                        Vector3 spherePos = new Vector3(Mathf.Round(detector.transform.position.x * 10f) / 10f, Mathf.Round(detector.transform.position.y * 10f) / 10f, Mathf.Round(_palette.transform.position.z * 10f) / 10f);

                        //Mengubah posisi bola bantu
                        mySphere.transform.position = spherePos;

                        Vector3 linePosition = new Vector3(detector.Position.x, detector.Position.y, _palette.transform.position.z);
                        drawState.UpdateLine(linePosition);
                        points.AddLast(detector.Position);
                    }
                    if (detector.DidRelease && release == false)
                    {
                        release = true;
                        StartCoroutine(ActivateRelease());
                        soalCounter++;
                        float[] tempPos12 = { (Mathf.Round(detector.transform.position.x * 10f) / 10f) + 1f, (Mathf.Round(detector.transform.position.y * 10f) / 10f) - 0.4f };
                        pointsToCheck.Add(tempPos12);
                        Vector2 releasePosFixed;
                        isRelease = true;
                        releasePos = new Vector2((Mathf.Round(detector.transform.position.x * 10f) / 10f) + 1f, (Mathf.Round(detector.transform.position.y * 10f) / 10f) - 0.4f);
                        helpPosEnd = new Vector3(Mathf.Round(detector.transform.position.x * 10f) / 10f, Mathf.Round(detector.transform.position.y * 10f) / 10f, Mathf.Round(_palette.transform.position.z * 10f) / 10f);
                        tempPos = new Vector2(helpPosEnd.x + 1f, helpPosEnd.y - 0.4f);
                        points.AddLast(detector.Position);
                        drawState.FinishLine();

                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;
                        lineScore.SetFunction(pointsToCheck[0][0], pointsToCheck[0][1], pointsToCheck[pointsToCheck.Count - 1][0], pointsToCheck[pointsToCheck.Count - 1][1]);
                        tempScore += lineScore.Scoring(pointsToCheck);
                        undoScore.Add(lineScore.Scoring(pointsToCheck));
                        scorePopUp.ShowScore(lineScore.Scoring(pointsToCheck).ToString());
                        pointsToCheck.Clear();
                        releasePosFixed = new Vector2((Mathf.Round(detector.transform.position.x * 10f) / 10f) + 1f, (Mathf.Round(detector.transform.position.y * 10f) / 10f) - 0.4f);

                        //Jika pengguna berhasil membuat gambar
                        if (arrayPoint[0] == releasePosFixed)
                        {
                            UnityEngine.Debug.Log("Shaped");
                            AngleGenerator();
                            shaped = true;
                        }
                        else if (releasePosFixed != arrayPoint[arrayPoint.Count - 1])
                        {
                            arrayPoint.Add(releasePosFixed);
                        }
                    }
                }
            }
            //LineRenderer lineHelper = gameObject.AddComponent<LineRenderer>()   
            
        }

        public void Selesai()
        {
            tmWin.gameObject.SetActive(true);
            tmWin.text = PresentUser.Name + "\ntelah berhasil!\n Score : " + lineScore.TotalScoring();
            PresentUser.DatarScore = lineScore.TotalScoring();
            
        }
        private void ShapeRecognition(float[] point, int length)
        {
            string jawaban;
            shape = "";
            //Segiempat
            if(length == 4)
            {
                if(point[0] == 90 && point[1] == 90 && point[2] == 90 && point[3] == 90)
                {
                    shape = "Persegi";
                }
            }
            //Segitiga
            else if (length == 3)
            {
                shape = "Segitiga";
            }
            jawaban = examDisp.GetAnswer();
            if (String.Compare(examDisp.GetAnswer(), shape) == 0)
            {
                totalGambar += 1;
                scorePopUp.ShowScore("Score gambar " + tempScore / length);
                //Debug.Log("Total score :" + lineScore.TotalScoring());
                Debug.Log("Jawaban anda benar yaitu " + jawaban);
                examDisp.ShowExam();
            }
            else
            {
                Debug.Log("Jawaban bukan " + shape + ", yang benar adalah : " + jawaban);
            }
        }

        public void DeletAllLine()
        {
            //Destroy all sphere game object
            foreach(int i in undoScore)
            {
                Debug.Log(i);
            }
            //GameObject[] sphere = GameObject.FindGameObjectsWithTag("sphere");
            for( int i = ballPoint.Count-1; i >= 0; i--)
            {
                Destroy(ballPoint[i]);
                ballPoint.RemoveAt(i);
            }

            //foreach (GameObject go in sphere)
            //{
            //    Destroy(go);
            //}

            //Destroy all line game object
            GameObject[] line = GameObject.FindGameObjectsWithTag("line");
            foreach (GameObject go in line)
            {
                Destroy(go);
            }
            for (int i = 0; i < undoScore.Count; i++)
            {
                undoScore.RemoveAt(i);
            }
            for (int i = 0; i < prepareToUndo.Count; i ++)
            {
                prepareToUndo.RemoveAt(i);
            }
            drawingStatus = false;
            arrayPoint.Clear();
            lineHelper.DeleteLine();
            shaped = false;
        }

        private void AngleGenerator()
        {
            Debug.Log(arrayPoint.Count);
            int[] temp = new int[arrayPoint.Count];
            int a;
            int b;
            float[] tempArray = new float[arrayPoint.Count];
            //Mencari verteks dari garis yang dibuat
            for(int i = 0; i < arrayPoint.Count; i ++)
            {
                a = (i + 1) % arrayPoint.Count;
                b = (i + 2) % arrayPoint.Count;
                Vector2 direction1 = arrayPoint[i] - arrayPoint[a];
                Vector2 direction2 = arrayPoint[b] - arrayPoint[a];
                tempArray[i] = Vector2.Angle(direction1, direction2);

            }
            ShapeRecognition(tempArray, tempArray.Length);
        }

        public void Undo()
        {
            if(undoScore.Count > 0)
            {
                tempScore -= undoScore[undoScore.Count - 1];
                undoScore.RemoveLast();
                //Debug.Log("tempScore " +tempScore);
            }
            if (shaped == true)
            {
                DeletAllLine();
            }
            else if (prepareToUndo.Count >= 1)
            {
                Debug.Log("array point " +arrayPoint.Count);
                Destroy(prepareToUndo[prepareToUndo.Count - 1]);
                arrayPoint.RemoveLast();
                Destroy(ballPoint[ballPoint.Count - 1]);
                lineHelper.DeleteLine();
                ballPoint.RemoveLast();
                if (prepareToUndo.Count == 1)
                {
                    Destroy(ballPoint[ballPoint.Count -1]);
                    ballPoint.RemoveLast();
                    arrayPoint.Clear();
                    shaped = false;
                }
                prepareToUndo.RemoveLast();
            }
           
        }
        [System.Serializable]
        private class DrawState
        {
            private List<Vector3> _vertices = new List<Vector3>();
            private List<int> _tris = new List<int>();
            private List<Vector2> _uvs = new List<Vector2>();
            private List<Color> _colors = new List<Color>();
            //private GameObject prepareToUndo;
            private PinchDraw _parent;

            private int _rings = 0;

            private Vector3 _prevRing0 = Vector3.zero;
            private Vector3 _prevRing1 = Vector3.zero;

            private Vector3 _prevNormal0 = Vector3.zero;
            public GameObject pos;
            public GameObject target;
            private Mesh _mesh;
            private GameObject tempGO;
            private SmoothedVector3 _smoothedPosition;
            private Vector3 endPoint = Vector3.zero;


            public DrawState(PinchDraw parent)
            {
                _parent = parent;
                _smoothedPosition = new SmoothedVector3();
                _smoothedPosition.delay = parent._smoothingDelay;
                _smoothedPosition.reset = true;
            }



            public GameObject BeginNewLine()
            {
                stopWatch.Reset();
                stopWatch.Start();
                _rings = 0;
                _vertices.Clear();
                _tris.Clear();
                _uvs.Clear();
                _colors.Clear();

                _smoothedPosition.reset = true;

                _mesh = new Mesh();

                _mesh.name = "Line Mesh";
                _mesh.MarkDynamic();


                GameObject lineObj = new GameObject("Line Object");              
                lineObj.tag = "line";
                lineObj.transform.position = Vector3.zero;
                //lineObj.transform.position = _palette.transform.position;
                lineObj.transform.rotation = Quaternion.identity;
                lineObj.transform.localScale = Vector3.one;
                lineObj.AddComponent<MeshFilter>().mesh = _mesh;
                Mesh smesh = lineObj.GetComponent<MeshFilter>().sharedMesh;
                lineObj.AddComponent<MeshRenderer>().sharedMaterial = _parent._material;
                return lineObj;
            }

            public void UpdateLine(Vector3 position)
            {
                _smoothedPosition.Update(position, Time.deltaTime);

                bool shouldAdd = false;

                shouldAdd |= _vertices.Count == 0;
                shouldAdd |= Vector3.Distance(_prevRing0, _smoothedPosition.value) >= _parent._minSegmentLength;

                if (shouldAdd)
                {
                    addRing(_smoothedPosition.value);
                    updateMesh();
                }
            }



            public void FinishLine()
            {

                _mesh.UploadMeshData(false);
                //StraightLineNew();   
            }

        

            private void updateMesh()
            {
                _mesh.SetVertices(_vertices);
                _mesh.SetColors(_colors);
                _mesh.SetUVs(0, _uvs);
                _mesh.SetIndices(_tris.ToArray(), MeshTopology.Triangles, 0);
                _mesh.RecalculateBounds();
                _mesh.RecalculateNormals();
            }

            private void addRing(Vector3 ringPosition)
            {
                _rings++;

                if (_rings == 1)
                {
                    addVertexRing();
                    addVertexRing();
                    addTriSegment();
                }

                addVertexRing();
                addTriSegment();

                Vector3 ringNormal = Vector3.zero;
                if (_rings == 2)
                {
                    Vector3 direction = ringPosition - _prevRing0;
                    float angleToUp = Vector3.Angle(direction, Vector3.up);

                    if (angleToUp < 10 || angleToUp > 170)
                    {
                        ringNormal = Vector3.Cross(direction, Vector3.right);
                    }
                    else
                    {
                        ringNormal = Vector3.Cross(direction, Vector3.up);
                    }

                    ringNormal = ringNormal.normalized;

                    _prevNormal0 = ringNormal;
                }
                else if (_rings > 2)
                {
                    Vector3 prevPerp = Vector3.Cross(_prevRing0 - _prevRing1, _prevNormal0);
                    ringNormal = Vector3.Cross(prevPerp, ringPosition - _prevRing0).normalized;
                }

                if (_rings == 2)
                {
                    updateRingVerts(0,
                                    _prevRing0,
                                    ringPosition - _prevRing1,
                                    _prevNormal0,
                                    0);
                }

                if (_rings >= 2)
                {
                    updateRingVerts(_vertices.Count - _parent._drawResolution,
                                    ringPosition,
                                    ringPosition - _prevRing0,
                                    ringNormal,
                                    0);
                    updateRingVerts(_vertices.Count - _parent._drawResolution * 2,
                                    ringPosition,
                                    ringPosition - _prevRing0,
                                    ringNormal,
                                    1);
                    updateRingVerts(_vertices.Count - _parent._drawResolution * 3,
                                    _prevRing0,
                                    ringPosition - _prevRing1,
                                    _prevNormal0,
                                    1);
                }

                _prevRing1 = _prevRing0;
                _prevRing0 = ringPosition;

                _prevNormal0 = ringNormal;
            }

            private void addVertexRing()
            {
                for (int i = 0; i < _parent._drawResolution; i++)
                {
                    _vertices.Add(Vector3.zero);  //Dummy vertex, is updated later
                    _uvs.Add(new Vector2(i / (_parent._drawResolution - 1.0f), 0));
                    _colors.Add(_parent._drawColor);
                }
            }

            //public GameObject GetUndoObj()
            //{
            //    return prepareToUndo;
            //}
            //Connects the most recently added vertex ring to the one before it
            private void addTriSegment()
            {
                for (int i = 0; i < _parent._drawResolution; i++)
                {
                    int i0 = _vertices.Count - 1 - i;
                    int i1 = _vertices.Count - 1 - ((i + 1) % _parent._drawResolution);

                    _tris.Add(i0);
                    _tris.Add(i1 - _parent._drawResolution);
                    _tris.Add(i0 - _parent._drawResolution);

                    _tris.Add(i0);
                    _tris.Add(i1);
                    _tris.Add(i1 - _parent._drawResolution);
                }
            }

            private void updateRingVerts(int offset, Vector3 ringPosition, Vector3 direction, Vector3 normal, float radiusScale)
            {
                direction = direction.normalized;
                normal = normal.normalized;

                for (int i = 0; i < _parent._drawResolution; i++)
                {
                    float angle = 360.0f * (i / (float)(_parent._drawResolution));
                    Quaternion rotator = Quaternion.AngleAxis(angle, direction);
                    Vector3 ringSpoke = rotator * normal * _parent._drawRadius * radiusScale;
                    _vertices[offset + i] = ringPosition + ringSpoke;
                }
            }
        }
    }
}
