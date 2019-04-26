using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MouseDrawing : MonoBehaviour
{
    public GameObject Brush;
    public float BrushSize = 0.1f;
    public Transform LineTransform;
    public Transform palette;
    public RenderTexture RTexture;

    private Mesh _mesh;
    private LineRenderer currentGestureRenderer;
    private int countVertex=0;
    private Vector3 virtualKeyPosition = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        //Mesh renderer
        _mesh = new Mesh();
        _mesh.name = "Line Mesh";
        _mesh.MarkDynamic();    
    }

    // Update is called once per frame
    void FixUpdate()
    {
        RaycastHit hit;
        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Ray, out hit))
        {
            if (hit.transform.tag == "palette")
            {
                virtualKeyPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                Transform tmpGesture = Instantiate(LineTransform) as Transform;
                currentGestureRenderer = tmpGesture.GetComponent<LineRenderer>();
                currentGestureRenderer.SetVertexCount(++countVertex);
                currentGestureRenderer.SetPosition(countVertex - 1, hit.point);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Ray, out hit))
            {
                Transform tmpGesture = Instantiate(LineTransform) as Transform;
                currentGestureRenderer = tmpGesture.GetComponent<LineRenderer>();
                countVertex = 0;              
            }
        }
        if (Input.GetMouseButton(0))
        {
            currentGestureRenderer.SetVertexCount(++countVertex);
            currentGestureRenderer.SetPosition(countVertex - 1, hit.point);
        }
    }

    public void save()
    {
        StartCoroutine(CoSave());    
    }

    public IEnumerator CoSave()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture.active = RTexture;       
        var texture2D = new Texture2D(RTexture.width, RTexture.height);
        texture2D.ReadPixels(new Rect(0, 0, RTexture.width, RTexture.height),0,0);
        texture2D.Apply();

        var data = texture2D.EncodeToPNG();

        File.WriteAllBytes(Application.dataPath + "/Data Gambar/Persegi Panjang/persegiPanjang20.png", data);
    }
}
