using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class AISensor : MonoBehaviour
{
    [SerializeField] private float _Distance = 10f;
    [SerializeField] private float _Angle = 30f;
    [SerializeField] private float _Height = 1f;
    [SerializeField] private int _Segment = 10;
    [SerializeField] private Color _Color = new Color();
    [SerializeField] private List<GameObject> _Objects = new List<GameObject>();
    private Mesh _Mesh;

    Collider[] _Colliders = new Collider[10];
    public LayerMask _Layers;

    [SerializeField] private int _ScanFrequency = 30;
    int _Count;
    float _ScanInterval;
    float _ScanTimer;
    
    void Start()
    {
        _ScanInterval = 1.0f /_ScanFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        _ScanTimer -= Time.deltaTime;
        if (_ScanTimer < 0 )
        {
            _ScanTimer += _ScanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        _Count = Physics.OverlapSphereNonAlloc(transform.position, _Distance, _Colliders, _Layers, QueryTriggerInteraction.Collide);
        
        _Objects.Clear();
        for (int i = 0; i < _Count; i++)
        {
            GameObject lObj = _Colliders[i].gameObject;
            if (IsInSight(lObj))
            {
                _Objects.Add(lObj);
            }
        }
    }

    private bool IsInSight(GameObject pObject)
    {
        Vector3 lOrigin = transform.position;
        Vector3 lDest = pObject.transform.position;
        Vector3 lDirection = lOrigin - lDest;
        
        if (lDirection.y < 0 || lDirection.y> _Height) return false;

        lDirection.y = 0;
        float lDeltaAnge = Vector3.Angle(lDirection, transform.forward);
        if (lDeltaAnge > _Angle) return false;

        return true;

    }

    private Mesh CreateWedgeMesh()
    {
        Mesh lMesh = new Mesh();

        int lNumTriangle = (_Segment*4)+2+2;
        int lNumVertices = lNumTriangle * 3;

        Vector3[] lVertices= new Vector3[lNumVertices];
        int[] lTriangles = new int[lNumVertices];

        Vector3 lBottomCenter = Vector3.zero + Vector3.forward/2;
        Vector3 lBottomLeft = Quaternion.Euler(0,-_Angle,0) * Vector3.forward * _Distance + Vector3.forward/2; 
        Vector3 lBottomRight = Quaternion.Euler(0,_Angle,0) * Vector3.forward * _Distance + Vector3.forward/2; 

        Vector3 lTopCenter = lBottomCenter + Vector3.up * _Height;
        Vector3 lTopLeft = lBottomLeft + Vector3.up * _Height;
        Vector3 lTopRight = lBottomRight + Vector3.up * _Height;

        int lVert = 0;

        //Left side
        lVertices[lVert++] = lBottomCenter;
        lVertices[lVert++] = lBottomLeft;
        lVertices[lVert++] = lTopLeft;

        lVertices[lVert++] = lTopLeft;
        lVertices[lVert++] = lTopCenter;
        lVertices[lVert++] = lBottomCenter;
        
        //Right side
        lVertices[lVert++] = lBottomCenter;
        lVertices[lVert++] = lTopCenter;
        lVertices[lVert++] = lTopRight;

        lVertices[lVert++] = lTopRight;
        lVertices[lVert++] = lBottomRight;
        lVertices[lVert++] = lBottomCenter;

        float lCurrentAngle = -_Angle;
        float lDeltaAngle = (_Angle * 2) / _Segment;
        for (int i = 0; i < _Segment; i++)
        {
            lBottomLeft = Quaternion.Euler(0, lCurrentAngle, 0) * Vector3.forward * _Distance + Vector3.forward/2;
            lBottomRight = Quaternion.Euler(0, lCurrentAngle+lDeltaAngle, 0) * Vector3.forward * _Distance + Vector3.forward/2;

            lTopLeft = lBottomLeft + Vector3.up * _Height;
            lTopRight = lBottomRight + Vector3.up * _Height;

            //Far side
            lVertices[lVert++] = lBottomLeft;
            lVertices[lVert++] = lBottomRight;
            lVertices[lVert++] = lTopRight;

            lVertices[lVert++] = lTopRight;
            lVertices[lVert++] = lTopLeft;
            lVertices[lVert++] = lBottomLeft;

            //Top side
            lVertices[lVert++] = lTopCenter;
            lVertices[lVert++] = lTopLeft;
            lVertices[lVert++] = lTopRight;


            //Down side
            lVertices[lVert++] = lBottomCenter;
            lVertices[lVert++] = lBottomRight;
            lVertices[lVert++] = lBottomLeft;

            lCurrentAngle += lDeltaAngle;
        }
        

        for (int vertex = 0; vertex<lNumVertices; vertex++)
        {
            lTriangles[vertex] = vertex;
        }

        lMesh.vertices = lVertices;
        lMesh.triangles = lTriangles;
        lMesh.RecalculateNormals();

        return lMesh;
    }
    private void OnValidate()
    {
        _Mesh = CreateWedgeMesh();
        _ScanInterval = 1.0f / _ScanFrequency;
    }
    private void OnDrawGizmos()
    {
        if (_Mesh)
        {
            Gizmos.color = _Color;
            Gizmos.DrawMesh(_Mesh, transform.position, transform.rotation);
        }
        for (int i = 0; i < _Count; i++)
        {
            Gizmos.DrawSphere(_Colliders[i].transform.position, 2f);
        }

        GUI.color = Color.green;
        foreach (var collider in _Objects)
        {
            Gizmos.DrawSphere(collider.transform.position, 2f);
        }
    }
}
