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

public class RCCP_InitLoad {

    [InitializeOnLoadMethod]
    public static void InitOnLoad() {

        EditorApplication.delayCall += EditorDelayedUpdate;
        EditorApplication.playModeStateChanged += EditorPlayModeChanged;

    }

    public static void EditorDelayedUpdate() {

        RCCP_Installation.CheckProjectLayers();

        CheckSymbols();

        RCCP_Installation.CheckMissingWheelSlipParticles();

        RCCP_DemoScenes.Instance.GetPaths();

#if RCCP_PHOTON
        RCCP_DemoScenes_Photon.Instance.GetPaths();
#endif

#if BCG_ENTEREXIT
        BCG_DemoScenes.Instance.GetPaths();
#endif

#if RCCP_MIRROR
        RCCP_DemoScenes_Mirror.Instance.GetPaths();
#endif

    }

    public static void EditorPlayModeChanged(PlayModeStateChange state) {

        if (state == PlayModeStateChange.ExitingEditMode)
            CheckRP();

    }

    public static void CheckSymbols() {

        bool hasKey = false;

#if BCG_RCCP && !RCCP_DEMO

        if (!RCCP_DemoContent.Instance.dontAskDemoContent) {

            RCCP_DemoContent.Instance.dontAskDemoContent = true;

            bool importDemoAssets = EditorUtility.DisplayDialog("Realistic Car Controller Pro | Demo Assets", "Do you want to import demo assets such as vehicles, city, environment, scenes, etc...? You can import them later from the welcome window (Tools --> BCG --> RCCP --> Welcome Window).", "Import Demo Assets", "No");

            if (importDemoAssets)
                AssetDatabase.ImportPackage(RCCP_AddonPackages.Instance.GetAssetPath(RCCP_AddonPackages.Instance.demoPackage), true);

            EditorUtility.SetDirty(RCCP_DemoContent.Instance);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

#endif

#if BCG_RCCP
        hasKey = true;
#endif

        if (!hasKey) {

            RCCP_SetScriptingSymbol.SetEnabled("BCG_RCCP", true);

            //EditorUtility.DisplayDialog("Realistic Car Controller Pro | Regards from BoneCracker Games", "Thank you for purchasing and using Realistic Car Controller Pro. Please read the documentation before use. Also check out the online documentation for updated info. Have fun :)", "Let's get started!");
            //EditorUtility.DisplayDialog("Realistic Car Controller Pro | Input System", "RCC Pro is using new input system as default. But you can switch to the old input system later if you want. Make sure your project has Input System installed through the Package Manager now. It should be installed if you have installed dependencies while importing the package. If you haven't installed dependencies, no worries. You can install Input System from the Package Manager (Window --> Package Manager). More info can be found in the documentation.", "Ok");

            //RCCP_WelcomeWindow.OpenWindow();

            //EditorApplication.delayCall += () => {

            //    RCCP_Installation.CheckAllLayers();

            //};

        }

    }

    public static void CheckRP() {

        Shader checkURP = null;

        if (UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline && UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline.defaultShader)
            checkURP = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline.defaultShader;

        if (checkURP != null && checkURP.name == "Universal Render Pipeline/Lit") {

#if !BCG_URP

            RCCP_SetScriptingSymbol.SetEnabled("BCG_URP", true);

            int selection = EditorUtility.DisplayDialogComplex("Converting Shaders", "Please read ''RCC Pro - Universal RP (URP)'' to install and configure URP to your project first. All demo materials need to be converted to URP shader.", "Open DemoMaterials", "Cancel", "");

            if (selection == 0) {

                EditorApplication.ExitPlaymode();

                if (RCCP_DemoContent.Instance.builtinShadersContent != null) {

                    Selection.activeObject = RCCP_DemoMaterials.Instance;
                    FileUtil.DeleteFileOrDirectory(RCCP_GetAssetPath.GetAssetPath(RCCP_DemoContent.Instance.builtinShadersContent));
                    AssetDatabase.Refresh();

                }

                if (RCCP_DemoContent.Instance.URPShaderPackage != null) {

                    Selection.activeObject = RCCP_DemoMaterials.Instance;
                    AssetDatabase.ImportPackage(RCCP_GetAssetPath.GetAssetPath(RCCP_DemoContent.Instance.URPShaderPackage), true);
                    AssetDatabase.Refresh();

                }

            }

#endif

        } else {

#if BCG_URP

            RCCP_SetScriptingSymbol.SetEnabled("BCG_URP", false);

            int selection = EditorUtility.DisplayDialogComplex("Converting Shaders", "Please read ''RCC Pro - Universal RP (URP)'' to remove URP from your project first. All demo materials need to be converted back to builtin shader (Standard).", "Open DemoMaterials", "Cancel", "");

            if (selection == 0) {

                EditorApplication.ExitPlaymode();

                if (RCCP_DemoContent.Instance.URPShadersContent != null) {

                    Selection.activeObject = RCCP_DemoMaterials.Instance;
                    FileUtil.DeleteFileOrDirectory(RCCP_GetAssetPath.GetAssetPath(RCCP_DemoContent.Instance.URPShadersContent));
                    AssetDatabase.Refresh();

                }

                if (RCCP_DemoContent.Instance.builtinShaderPackage != null) {

                    Selection.activeObject = RCCP_DemoMaterials.Instance;
                    AssetDatabase.ImportPackage(RCCP_GetAssetPath.GetAssetPath(RCCP_DemoContent.Instance.builtinShaderPackage), true);
                    AssetDatabase.Refresh();

                }

            }

#endif

        }

    }

}
