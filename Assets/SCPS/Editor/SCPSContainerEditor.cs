using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SCPSContainer))]
public class SCPSContainerEditor : Editor
{

    SCPSContainer wpScript;
    float WaypointRadius=2f;
    private GameObject gameObject;
    public Material material;
    public override void OnInspectorGUI()
    {
       
        wpScript = (SCPSContainer)target;
        //Explanation on what to do
        EditorGUILayout.TextArea("Create Waypoints By Pressing Shift And Left Click");
        //Show the properties of the script
                EditorGUILayout.PropertyField(serializedObject.FindProperty("wp"), new GUIContent("Waypoints"));
                if (GUILayout.Button("Delete Waypoints"))
                {
                    foreach (Transform t in wpScript.wp)
                    {
                        DestroyImmediate(t.gameObject);
                    }
                    wpScript.wp.Clear();
                }
        WaypointRadius= EditorGUILayout.FloatField("Waypoint Radius: ", wpScript.WaypointRadius);
        wpScript.WaypointRadius = WaypointRadius;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SphereMaterial"), new GUIContent("Sphere Material"));
        serializedObject.ApplyModifiedProperties();
        gameObject = wpScript.gameObject;
    }

    public void Waypoints()
    {
        //Loads Waypoints
        wpScript.wp = new List<Transform>();

        Transform[] ChildTransforms = wpScript.transform.GetComponentsInChildren<Transform>();

        foreach (Transform t in ChildTransforms)
        {

            if (t == wpScript.transform)
            {
                //do nothing
            }
            else
            {
                wpScript.wp.Add(t);
            }
        }

    }
    void OnSceneGUI()
    {
        
       
        Event eve = Event.current;
        wpScript = (SCPSContainer)target;
        if (eve != null)
        {
            //Raycast when the use presses shift and left click
            if (eve.isMouse && eve.shift && eve.type == EventType.MouseDown)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    //    GameObject wp = new GameObject("Waypoint " + wpScript.wp.Count.ToString());
                    GameObject wp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    wp.GetComponent<Collider>().enabled = false;
                    wp.GetComponent<Renderer>().enabled = false;
                    wp.name=("Waypoint " + wpScript.wp.Count.ToString());
                    wp.transform.localScale = Vector3.one * WaypointRadius*2f;
                    // disable teh shadow if you want to
                 //   wp.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    wp.GetComponent<Renderer>().material = wpScript.SphereMaterial;
                    wp.transform.position = hit.point;
                    wp.transform.SetParent(wpScript.transform);
                    Waypoints();
                }

            }
            if (gameObject != null)
            {
                Selection.activeGameObject = gameObject;
            }
        }
        Waypoints();
    }


}
