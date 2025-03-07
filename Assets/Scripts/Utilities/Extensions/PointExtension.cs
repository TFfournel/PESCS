using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point: MonoBehaviour
{
    public Vector3 position;
    public GameObject attachedObject;
    public List<string> tags = new List<string>();
    public Quaternion rotation = Quaternion.identity;
}

public class PointExtension: MonoBehaviour
{
    public static List<Point> ConvertVector3ToPoint(List<Vector3> lAllVector3)
    {
        List<Point> lAllPoints = new List<Point>();
        int lLength = lAllVector3.Count;
        Point lPoint;
        for(int i = 0 ; i < lLength ; i++)
        {
            lPoint = new Point();
            lPoint.position = lAllVector3[i];
            lAllPoints.Add(lPoint);
        }

        return lAllPoints;
    }

    public static List<GameObject> AttachGameObjectToPoint(List<Point> pAllPoint,List<GameObject> lOjbectListPool,Transform pTransform = null)
    {
        List<GameObject> lOjbectList = new List<GameObject>();
        foreach(Point lPoint in pAllPoint)
        {
            GameObject lObject = lOjbectListPool[RandomExtension.RandomInRange(lOjbectListPool.Count)];
            if(lPoint.attachedObject != null)
                Destroy(lPoint.attachedObject);
            if(pTransform == null)
                lPoint.attachedObject = Instantiate(lObject,lPoint.position,lPoint.rotation);
            else
                lPoint.attachedObject = Instantiate(lObject,lPoint.position,lPoint.rotation,pTransform);
        }

        return lOjbectList;
    }

    public static List<GameObject> AttachGameObjectToPoint(List<Point> pAllPoint,GameObject lOjbectListPool,
        Transform pTransform = null)
    {
        List<GameObject> lOjbectList = new List<GameObject>();
        lOjbectList.Add(lOjbectListPool);
        return AttachGameObjectToPoint(pAllPoint,lOjbectList,pTransform);
    }

    public static List<Point> SelectClosePoint(Point lPoint,List<Point> PointToLoook,float pSearcDistance,bool pExcludeClose)
    {
        List<Point> lList = new List<Point>();
        int lLength = PointToLoook.Count;
        Point lPointToCheck;
        float lDistance;
        for(int i = 0 ; i < lLength ; i++)
        {
            lPointToCheck = PointToLoook[i];
            lDistance = VectorExtensions.Direction(lPoint.position,lPointToCheck.position).magnitude;

            if(pExcludeClose ? lDistance < pSearcDistance : lDistance < pSearcDistance)
            {
                lList.Add(lPointToCheck);
            }
        }
        return lList;
    }

    public static List<Point> SelectClosePoint(List<Point> lPoint,float pSearcDistance,bool pExcludeClose)
    {
        List<Point> lList = new List<Point>();
        int lLength = lPoint.Count;
        for(int i = 0 ; i < lLength ; i++)
        {
            lList.AddRange(SelectClosePoint(lPoint[i],lPoint,pSearcDistance,pExcludeClose));
        }
        return lList;
    }
}