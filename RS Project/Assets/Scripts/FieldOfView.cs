using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Range")]
    public float viewRadius;
    [Range(0,360)] public float viewAngle;
    
    [Header("Search masks")]
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        //StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    
    public Transform[] FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);    
                }
            }
        }

        return visibleTargets.ToArray();
    }
    
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        //converts angle into a global angle
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        
        return new Vector3(
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
    
}
