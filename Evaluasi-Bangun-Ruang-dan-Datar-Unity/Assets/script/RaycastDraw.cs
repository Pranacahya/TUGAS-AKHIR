using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDraw : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pos;
    public GameObject target;
    private GameObject line;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;
    public static Vector3 hitPos = Vector3.zero;

    void Start()
    {
        //LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        //lineRenderer.positionCount = lengthOfLineRenderer;

        //// A simple 2 color gradient with a fixed alpha of 1.0f.
        //float alpha = 1.0f;
        //Gradient gradient = new Gradient();
        //gradient.SetKeys(
        //    new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
        //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        //);
        //lineRenderer.colorGradient = gradient;
    }
    //private void Update()
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        Debug.Log(Input.mousePosition);
    //    }
    //}

    //void FixedUpdate()
    //{
    //    LineRenderer lineRenderer = GetComponent<LineRenderer>();
    //    var t = Time.time;
    //    Color color = new Color(0, 1, 0);
    //    RaycastHit hit;
    //    RaycastHit[] hits;
    //    Debug.DrawLine(pos.transform.position, target.transform.position, color);
    //    if (Physics.Raycast(pos.transform.position, target.transform.position, out hit))
    //    {
    //        if (hit.collider.name == "palette")
    //        {

    //            print("hit at : " + hit.point);
    //            DrawLine(transform.position, hit.point, color);
    //        }
    //        Debug.DrawLine(transform.position, -Vector3.up, color);

    //    }
    //    hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);
    //    for (int i = 0; i < hits.Length; i++)
    //    {
    //        RaycastHit hitz = hits[i];
    //        Renderer rend = hitz.transform.GetComponent<Renderer>();

    //        if (hitz.collider.name == "palette")
    //        {
    //            hitPos = hitz.point;
    //            Debug.Log(hitPos);
    //            Debug.Log("MASOK");
    //            Change the material of all hit colliders
    //            to use a transparent shader.

    //        }
    //    }

    //    for (int i = 0; i < lengthOfLineRenderer; i++)
    //    {
    //        lineRenderer.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f));
    //    }
    //}

    //void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    //{
    //    GameObject myLine = new GameObject();
    //    myLine.transform.position = start;
    //    myLine.AddComponent<LineRenderer>();
    //    LineRenderer lr = myLine.GetComponent<LineRenderer>();
    //    lr.material = new Material(Shader.Find("Particles/Default-Material"));
    //    lr.SetColors(color, color);
    //    lr.SetWidth(0.1f, 0.1f);
    //    lr.SetPosition(0, start);
    //    lr.SetPosition(1, end);
    //    lr.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    //    GameObject.Destroy(myLine, duration);
    //}

}
