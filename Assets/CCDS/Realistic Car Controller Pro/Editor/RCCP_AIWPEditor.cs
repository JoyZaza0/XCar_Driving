﻿//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2025 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(RCCP_AIWaypointsContainer))]
public class RCCP_AIWPEditor : Editor {

    RCCP_AIWaypointsContainer wpScript;
    GUISkin skin;

    private void OnEnable() {

        skin = Resources.Load<GUISkin>("RCCP_Gui");

    }

    public override void OnInspectorGUI() {

        wpScript = (RCCP_AIWaypointsContainer)target;
        serializedObject.Update();
        GUI.skin = skin;

        EditorGUILayout.HelpBox("Create Waypoints By Shift + Left Mouse Button On Your Road", MessageType.Info);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("waypoints"), new GUIContent("Waypoints", "Waypoints"), true);

        foreach (Transform item in wpScript.transform) {

            if (item != wpScript.transform && item.gameObject.GetComponent<RCCP_Waypoint>() == null)
                item.gameObject.AddComponent<RCCP_Waypoint>();

        }

        if (GUILayout.Button("Delete Waypoints")) {

            foreach (RCCP_Waypoint t in wpScript.waypoints)
                DestroyImmediate(t.gameObject);

            wpScript.waypoints.Clear();
            EditorUtility.SetDirty(wpScript);

        }

        if (GUILayout.Button("Invert")) {

            Invert();
            EditorUtility.SetDirty(wpScript);

        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(wpScript);

    }

    private void OnSceneGUI() {

        Event e = Event.current;
        wpScript = (RCCP_AIWaypointsContainer)target;

        if (e != null) {

            if (e.isMouse && e.shift && e.type == EventType.MouseDown) {

                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit = new RaycastHit();

                int controlId = GUIUtility.GetControlID(FocusType.Passive);

                // Tell the UI your event is the main one to use, it override the selection in  the scene view
                GUIUtility.hotControl = controlId;
                // Don't forget to use the event
                Event.current.Use();

                if (Physics.Raycast(ray, out hit, 5000.0f)) {

                    Vector3 newTilePosition = hit.point;

                    GameObject wp = new GameObject("Waypoint " + wpScript.waypoints.Count.ToString());
                    wp.AddComponent<RCCP_Waypoint>();
                    wp.transform.position = newTilePosition;
                    wp.transform.SetParent(wpScript.transform);

                    wpScript.GetAllWaypoints();

                    EditorUtility.SetDirty(wpScript);

                }

            }

        }

	    wpScript.GetAllWaypoints();
	    //Check();

    }

    public void Invert() {

        List<RCCP_Waypoint> waypoints = new List<RCCP_Waypoint>();
        wpScript.GetAllWaypoints();

        for (int i = 0; i < wpScript.waypoints.Count; i++) {

            if (wpScript.waypoints[i] != null)
                waypoints.Add(wpScript.waypoints[i]);

        }

        // Sort children in reverse order
        waypoints.Reverse();

        for (int i = 0; i < waypoints.Count; i++)
            waypoints[i].transform.SetSiblingIndex(i);

        wpScript.waypoints = waypoints;

    }
    
	public void Check()
	{
		wpScript.GetAllWaypoints();
		List<RCCP_Waypoint> newList = new List<RCCP_Waypoint>();
		HashSet<Vector3> set = new HashSet<Vector3>();
		
		foreach (var item in wpScript.waypoints)
		{
			if(set.Add(item.transform.position))
			{
				newList.Add(item);
			}
		}
		
		wpScript.waypoints = newList;
		
		for (int i = 0; i < wpScript.waypoints.Count; i++) 
		{
			wpScript.waypoints[i].gameObject.name = "Waypoint " + i;
		}
	}

}
