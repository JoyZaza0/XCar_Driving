//----------------------------------------------
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

[CustomEditor(typeof(RCCP_DemoMaterials))]
public class RCCP_DemoMaterialsEditor : Editor {

    RCCP_DemoMaterials prop;
    GUISkin skin;

    private void OnEnable() {

        skin = Resources.Load<GUISkin>("RCCP_Gui");

    }

    public override void OnInspectorGUI() {

        prop = (RCCP_DemoMaterials)target;
        serializedObject.Update();
        GUI.skin = skin;

        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("To URP Shaders", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField("Step 1", EditorStyles.boldLabel);

        if (GUILayout.Button("Select All Demo Materials\nFor Converting To URP\n(Except vehicle body materials)"))
            RCCP_EditorWindows.URP();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField("Step 2", EditorStyles.boldLabel);

        if (GUILayout.Button("Convert All Demo Vehicle Body Shaders\nTo URP Shaders"))
            RCCP_EditorWindows.URPBodyShader();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField("Step 3", EditorStyles.boldLabel);

        if (GUILayout.Button("Convert All LensFlares To URP"))
            RCCP_EditorWindows.ConvertBuiltInFlaresToSRP();

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("To Builtin Shaders", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField("Step 1", EditorStyles.boldLabel);

        if (GUILayout.Button("Select All Demo Materials\nFor Converting To Builtin\n(Except vehicle body materials)"))
            RCCP_EditorWindows.Builtin();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField("Step 2", EditorStyles.boldLabel);

        if (GUILayout.Button("Convert All Demo Vehicle Body Shaders\nTo Builtin Shaders"))
            RCCP_EditorWindows.BuiltinBodyShader();

        EditorGUILayout.EndVertical();

        EditorGUILayout.Separator();

        if (GUILayout.Button("Get Default Shaders")) {

            for (int i = 0; i < prop.demoMaterials.Length; i++) {

                if (prop.demoMaterials[i] != null && prop.demoMaterials[i].material != null)
                    Debug.Log(prop.demoMaterials[i].DefaultShader);

            }

        }

        EditorGUILayout.Separator();
        GUILayout.FlexibleSpace();

        EditorGUILayout.LabelField("Developed by Ekrem Bugra Ozdoganlar\nBoneCracker Games", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(50f));

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

    }

}
