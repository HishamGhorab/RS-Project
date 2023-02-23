using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Distance : MonoBehaviour
{
    //draw the distance from yourself to some distance infront of you!

    public Color meleeColor = new Color(1f, 0f, 0f, 0.25f);
    public float meleeRadius = 1;
    
    public Color rangedColor = new Color(1f, 0f, 0f, 0.25f);
    public float rangedRadius = 1;
}

[CustomEditor(typeof(Distance))]
public class DistanceEditor : Editor
{
    public void OnSceneGUI()
    {
        var distance = target as Distance;
        var tp = distance.transform.position;

        // draw the detectopm range
        Handles.color = distance.meleeColor;
        Handles.DrawWireDisc(tp, distance.transform.up, distance.meleeRadius);
        
        GUI.color = distance.meleeColor;
        
        Vector3 meleeDist = new Vector3(tp.x  + distance.meleeRadius, tp.y, tp.z);
        Handles.Label(meleeDist, distance.meleeRadius.ToString("F1"));
        
        
        Handles.color = distance.rangedColor;
        Handles.DrawWireDisc(tp, distance.transform.up, distance.rangedRadius);
        
        GUI.color = distance.rangedColor;

        Vector3 rangedDist = new Vector3(tp.x  + distance.rangedRadius, tp.y, tp.z);
        Handles.Label(rangedDist, distance.rangedRadius.ToString("F1"));
    }
}
