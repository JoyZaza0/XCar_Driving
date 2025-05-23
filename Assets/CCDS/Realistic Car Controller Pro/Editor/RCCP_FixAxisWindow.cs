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

[InitializeOnLoad]
public class RCCP_FixAxisWindow : EditorWindow {

    private GUISkin skin;
    public MeshFilter target;

    private const int windowWidth = 450;
    private const int windowHeight = 250;

    public string savedInstanceLocation = "";

    private Mesh tempMesh;
    private bool saved = false;

    public static void OpenWindow() {

        GetWindow<RCCP_FixAxisWindow>(true);

    }

    private void OnEnable() {

        skin = Resources.Load<GUISkin>("RCCP_Gui");
        titleContent = new GUIContent("Fix Axis And Rotate Mesh Origin");
        maxSize = new Vector2(windowWidth, windowHeight);
        minSize = maxSize;
        saved = false;

    }

    private void OnGUI() {

        GUI.skin = skin;

        if (tempMesh == null)
            tempMesh = (Mesh)Instantiate(target.sharedMesh);

        savedInstanceLocation = "Assets/Realistic Car Controller Pro/Fixed Meshes";

        EditorGUILayout.LabelField("Fixing axis of the " + target.name + ".");
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Model will not be overwritten, new mesh data will be saved as instance.");
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Saved location of the mesh: ");
        EditorGUILayout.LabelField(savedInstanceLocation);
        EditorGUILayout.Space();

        bool fixedRotation = 1 - Mathf.Abs(Quaternion.Dot(target.transform.rotation, target.transform.root.rotation)) < .01f;

        if (!fixedRotation) {

            EditorGUILayout.BeginHorizontal(GUI.skin.box);

            if (GUILayout.Button("Reset Pivot Rotation"))
                target.transform.rotation = target.transform.root.rotation;

            EditorGUILayout.EndHorizontal();

        }

        GUI.enabled = fixedRotation;

        if (!fixedRotation)
            EditorGUILayout.HelpBox("Reset pivot rotation to rotate mesh.", MessageType.Info);

        EditorGUILayout.BeginHorizontal(GUI.skin.box);

        if (GUILayout.Button("Mesh Rotate X")) {

            Vector3[] vertices = tempMesh.vertices;
            Vector3[] newVertices = new Vector3[vertices.Length];
            Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);

            for (int i = 0; i < vertices.Length; i++)
                newVertices[i] = rotation * vertices[i];

            tempMesh.vertices = newVertices;
            tempMesh.RecalculateNormals();
            tempMesh.RecalculateBounds();
            target.mesh = tempMesh;
            EditorUtility.SetDirty(target);

        }

        if (GUILayout.Button("Mesh Rotate Y")) {

            Vector3[] vertices = tempMesh.vertices;
            Vector3[] newVertices = new Vector3[vertices.Length];
            Quaternion rotation = Quaternion.Euler(0f, -90f, 0f);

            for (int i = 0; i < vertices.Length; i++)
                newVertices[i] = rotation * vertices[i];

            tempMesh.vertices = newVertices;
            tempMesh.RecalculateNormals();
            tempMesh.RecalculateBounds();
            target.mesh = tempMesh;
            EditorUtility.SetDirty(target);

        }

        if (GUILayout.Button("Mesh Rotate Z")) {

            Vector3[] vertices = tempMesh.vertices;
            Vector3[] newVertices = new Vector3[vertices.Length];
            Quaternion rotation = Quaternion.Euler(0f, 0f, -90f);

            for (int i = 0; i < vertices.Length; i++)
                newVertices[i] = rotation * vertices[i];

            tempMesh.vertices = newVertices;
            tempMesh.RecalculateNormals();
            tempMesh.RecalculateBounds();
            target.mesh = tempMesh;
            EditorUtility.SetDirty(target);

        }

        EditorGUILayout.EndHorizontal();

        GUI.enabled = true;

        if (GUILayout.Button("Save Mesh & Close")) {

            saved = true;
            Mesh tmp = SaveMesh(target.sharedMesh);
            target.mesh = tmp;
            CheckMeshCollider();
            Close();

        }

    }

    private Mesh SaveMesh(Mesh mesh) {

        Mesh tmp = (Mesh)Instantiate(mesh);

        if (!AssetDatabase.IsValidFolder(savedInstanceLocation + "/" + target.transform.root.name))
            AssetDatabase.CreateFolder("Assets/Realistic Car Controller Pro/Fixed Meshes", target.transform.root.name);

        AssetDatabase.CreateAsset(tmp, savedInstanceLocation + "/" + target.transform.root.name + "/" + mesh.name + ".mesh");
        return tmp;

    }

    private void CheckMeshCollider() {

        MeshCollider mCol = target.GetComponent<MeshCollider>();

        if (mCol)
            mCol.sharedMesh = target.sharedMesh;

    }

    private void OnDisable() {

        target = null;

        int goBack = 0;

        if (!saved)
            goBack = EditorUtility.DisplayDialogComplex("Mesh not saved", "Mesh is not saved yet. You will need to click save mesh button to save fixed mesh.", "Back", "Don't Save", "");

    }

}
