using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPath : MonoBehaviour
{
    public AnimationCurve BeazierSpeedCurve;
    public float BeazierSpeed;
    public Color rayColor = Color.white;

    [Header("Pointer Count")][Range(40,100)]
    public int Resolution;
    [Space(10)]

    [Header("Pointer Shpere Radius")][Range(0, 3)]
    public float GizmosSphereRadius;
    [Space(10)]

    public int CutLength;

    public List<Transform> PathObjs = new List<Transform>();


    private Transform[] theArray;

    [HideInInspector]
    public List<Vector3> beazierList = new List<Vector3>();

    //


    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        PathObjs.Clear();

        foreach (Transform path in theArray)
        {
            if (path != transform)
            {
                PathObjs.Add(path);
            }
        }
        if (PathObjs.Count <= 3)
            return;

        beazierList.Clear();

        Vector3 previousDrawPoint = PathObjs[0].position;

        int resolution = Resolution;

        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * 1.0F;

            Vector3 displacement = Utill.GetPointOnBezierCurve(PathObjs[0].position,
                PathObjs[1].position, PathObjs[2].position, PathObjs[3].position, simulationTime);
            Vector3 drawPoint = displacement;
            previousDrawPoint = drawPoint;
            beazierList.Add(previousDrawPoint);
        }

        for (int i = 1; i < beazierList.Count; i++)
        {
            Vector3 startv = beazierList[i - 1];

            Vector3 endv = beazierList[i];

            Gizmos.color = rayColor;

            Gizmos.DrawLine(startv, endv);

            Gizmos.DrawWireSphere(endv, GizmosSphereRadius);
        }
    }
}
