using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Procedural: MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform center;
    public Vector3 size;

    public float density;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    public bool reload = false;

    // Start is called before the first frame update
    private void Start()
    {
        Spawn();
    }

    private void OnValidate()
    {
        if(reload)
        {
            reload = false;
            Spawn();
        }
    }

    private void Spawn()
    {
        Debug.Log("density" + size.magnitude / density);

        List<Point> allPoints = PointExtension.ConvertVector3ToPoint(RandomExtension.RandomAounrd(center.position,size,Mathf.CeilToInt(density * size.magnitude)));
        ListExtension.DestroyAllObjects(spawnedObjects);
        spawnedObjects.Clear();
        ListExtension.DeleteAllChildren(center.gameObject);
        spawnedObjects = PointExtension.AttachGameObjectToPoint(allPoints,blockPrefab,center);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}