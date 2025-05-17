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
using UnityEngine.Rendering;

public class RCCP_EditorWindows : Editor {

    // Renamed from "Edit RCCP Settings" to "RCCP Settings"
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/RCCP Settings", false, -100)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/RCCP Settings", false, -100)]
    public static void OpenRCCSettings() {
        Selection.activeObject = RCCP_Settings.Instance;
    }

    // Renamed from "Add Main Controller To Vehicle" to "Add Controller To Vehicle"
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Add Controller To Vehicle", false, -85)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Add Controller To Vehicle", false, -85)]
    public static void AddMainControllerToVehicle() {

        if (Selection.activeGameObject != null) {

            if (Selection.gameObjects.Length == 1 && Selection.activeGameObject.scene.name != null && !EditorUtility.IsPersistent(Selection.activeGameObject))
                RCCP_CreateNewVehicle.NewVehicle(Selection.activeGameObject);
            else
                EditorUtility.DisplayDialog("Realistic Car Controller Pro | Selection", "Please select only one vehicle in the scene. Be sure to select root of the vehicle gameobject before adding the main controller", "Care to try again?", "Yesn't");

        }

    }

    #region Setup

    // Renamed folder from "Configure" to "Setup"
    // Ground Materials --> Ground Physics
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Setup/Ground Physics", false, -65)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Setup/Ground Physics", false, -65)]
    public static void OpenGroundMaterialsSettings() {
        Selection.activeObject = RCCP_GroundMaterials.Instance;
    }

    // Changable Wheels --> Wheel Configurations
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Setup/Wheel Configurations", false, -65)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Setup/Wheel Configurations", false, -65)]
    public static void OpenChangableWheelSettings() {
        Selection.activeObject = RCCP_ChangableWheels.Instance;
    }

    // Recorded Clips --> Recordings
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Setup/Recordings", false, -65)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Setup/Recordings", false, -65)]
    public static void OpenRecordSettings() {
        Selection.activeObject = RCCP_Records.Instance;
    }

    // Demo Vehicles --> Demo Vehicles & Prefabs
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Setup/Demo/Demo Vehicles & Prefabs", false, -65)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Setup/Demo/Demo Vehicles & Prefabs", false, -65)]
    public static void OpenDemoVehiclesSettings() {
        Selection.activeObject = RCCP_DemoVehicles.Instance;
    }

#if RCCP_PHOTON && PHOTON_UNITY_NETWORKING
    // Demo Vehicles (Photon) --> Photon Demo Vehicles
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Setup/Photon Demo Vehicles", false, -65)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Setup/Photon Demo Vehicles", false, -65)]
    public static void OpenPhotonDemoVehiclesSettings() {
        Selection.activeObject = RCCP_DemoVehicles_Photon.Instance;
    }
#endif

    // Demo Scenes --> Demo Scenes & Levels
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Setup/Demo/Demo Scenes & Levels", false, -65)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Setup/Demo/Demo Scenes & Levels", false, -65)]
    public static void OpenDemoScenesSettings() {
        Selection.activeObject = RCCP_DemoScenes.Instance;
    }

    // Demo Materials --> Demo Materials & Shaders
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Setup/Demo/Demo Materials & Shaders", false, -65)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Setup/Demo/Demo Materials & Shaders", false, -65)]
    public static void OpenDemoMaterialsSettings() {
        Selection.activeObject = RCCP_DemoMaterials.Instance;
    }
    #endregion

    #region Scene Managers

    // Renamed folder from "Create/Managers" to "Create/Scene Managers"
    // Add RCCP Scene Manager To Scene --> Add Scene Manager
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Create/Scene Managers/Add Scene Manager", false, -50)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Create/Scene Managers/Add Scene Manager", false, -50)]
    public static void CreateRCCPSceneManager() {
        Selection.activeObject = RCCP_SceneManager.Instance;
    }

    // Add RCCP Skidmarks Manager To Scene --> Add Skidmarks Manager
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Create/Scene Managers/Add Skidmarks Manager", false, -50)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Create/Scene Managers/Add Skidmarks Manager", false, -50)]
    public static void CreateRCCPSkidmarksManager() {
        Selection.activeObject = RCCP_SkidmarksManager.Instance;
    }

    #endregion

    // Kept "Create/Scene" as is, but simplified item names:
    // Add RCCP Camera To Scene --> Add RCCP Camera
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Create/Scene/Add RCCP Camera", false, -50)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Create/Scene/Add RCCP Camera", false, -50)]
    public static void CreateRCCCamera() {

        if (FindFirstObjectByType<RCCP_Camera>(FindObjectsInactive.Include)) {

            EditorUtility.DisplayDialog("Realistic Car Controller Pro | Scene has RCCP Camera already!", "Scene has RCCP Camera already!", "Close");
            Selection.activeGameObject = FindFirstObjectByType<RCCP_Camera>(FindObjectsInactive.Include).gameObject;

        } else {

            GameObject cam = Instantiate(RCCP_Settings.Instance.RCCPMainCamera.gameObject);
            cam.name = RCCP_Settings.Instance.RCCPMainCamera.name;
            Selection.activeGameObject = cam.gameObject;

        }

    }

    // Add RCCP UI Canvas To Scene --> Add UI Canvas
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Create/Scene/Add UI Canvas", false, -50)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Create/Scene/Add UI Canvas", false, -50)]
    public static void CreateRCCUICanvas() {

        if (FindFirstObjectByType<RCCP_UIManager>(FindObjectsInactive.Include)) {

            EditorUtility.DisplayDialog("Realistic Car Controller Pro | Scene has RCCP UI Canvas already!", "Scene has RCCP UI Canvas already!", "Close");
            Selection.activeGameObject = FindFirstObjectByType<RCCP_UIManager>(FindObjectsInactive.Include).gameObject;

        } else {

            GameObject cam = Instantiate(RCCP_Settings.Instance.RCCPCanvas.gameObject);
            cam.name = RCCP_Settings.Instance.RCCPCanvas.name;
            Selection.activeGameObject = cam.gameObject;

        }

    }

    // Add AI Waypoints Container To Scene --> Add AI Waypoints
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Create/Scene/Add AI Waypoints", false, -50)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Create/Scene/Add AI Waypoints", false, -50)]
    public static void CreateRCCAIWaypointManager() {

        GameObject wpContainer = new GameObject("RCCP_AI_WaypointsContainer");
        wpContainer.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        wpContainer.AddComponent<RCCP_AIWaypointsContainer>();
        Selection.activeGameObject = wpContainer;

    }

    // Add AI Brake Zones Container To Scene --> Add AI Brake Zones
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Create/Scene/Add AI Brake Zones", false, -50)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Create/Scene/Add AI Brake Zones", false, -50)]
    public static void CreateRCCAIBrakeManager() {

        GameObject bzContainer = new GameObject("RCCP_AI_BrakeZonesContainer");
        bzContainer.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        bzContainer.AddComponent<RCCP_AIBrakeZonesContainer>();
        Selection.activeGameObject = bzContainer;

    }

    // Renamed "URP" folder to "Render Pipeline"
    // Renamed "To URP" subfolder to "URP"
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 1] Import URP Shaders", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 1] Import URP Shaders", false, 0)]
    public static void URPPackage() {

        EditorUtility.DisplayDialog("Realistic Car Controller Pro | Importing URP Shaders", "URP shaders will be imported and builtin custom shaders will be removed.", "Proceed");

        AssetDatabase.ImportPackage(RCCP_AddonPackages.Instance.GetAssetPath(RCCP_DemoContent.Instance.URPShaderPackage), true);
        AssetDatabase.importPackageCompleted += CompletedURPPackageImport;

    }

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 2] Convert All Demo Materials To URP", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 2] Convert All Demo Materials To URP", false, 0)]
    public static void URP() {

        EditorUtility.DisplayDialog("Realistic Car Controller Pro | Converting All Demo Materials To URP", "All demo materials will be selected in your project now. After that, you'll need to convert them to URP shaders while they have been selected.\n\nYou can convert them from the Edit --> Render Pipeline --> Universal Render Pipeline --> Convert Selected Materials.", "Select All Demo Materials");

        List<UnityEngine.Object> objects = new List<UnityEngine.Object>();

        for (int i = 0; i < RCCP_DemoMaterials.Instance.demoMaterials.Length; i++) {

            if (RCCP_DemoMaterials.Instance.demoMaterials[i] != null && RCCP_DemoMaterials.Instance.demoMaterials[i].material != null)
                objects.Add(RCCP_DemoMaterials.Instance.demoMaterials[i].material);

        }

        UnityEngine.Object[] orderedObjects = new UnityEngine.Object[objects.Count];

        for (int i = 0; i < orderedObjects.Length; i++)
            orderedObjects[i] = objects[i];

        Selection.objects = orderedObjects;

    }

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 3] Convert All Demo Vehicle Body Materials To URP", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 3] Convert All Demo Vehicle Body Materials To URP", false, 0)]
    public static void URPBodyShader() {

        EditorUtility.DisplayDialog("Realistic Car Controller Pro | Converting All Demo Body Materials To URP", "Shaders of the demo vehicles will be converted to URP shader named ''RCCP Car Body Shader URP''.", "Convert");

        for (int i = 0; i < RCCP_DemoMaterials.Instance.vehicleBodyMaterials.Length; i++)
            RCCP_DemoMaterials.Instance.vehicleBodyMaterials[i].shader = Shader.Find("RCCP Car Body Shader URP");

        for (int i = 0; i < RCCP_DemoMaterials.Instance.wheelBlurMaterials.Length; i++)
            RCCP_DemoMaterials.Instance.wheelBlurMaterials[i].shader = Shader.Find("RCCP_WheelBlur_URP");

    }

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 4] Remove Builtin Shaders", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 4] Remove Builtin Shaders", false, 0)]
    public static void RemoveBuiltinShader() {

        if (EditorUtility.DisplayDialog("Realistic Car Controller Pro | Removing Builtin Shaders", "Builtin shaders will be removed, otherwise you'll have warnings on your console. You can import them again from welcome window of the RCCP (Tools --> BCG --> RCCP --> Welcome Window)'.", "Remove Builtin Shaders", "Cancel")) {

            FileUtil.DeleteFileOrDirectory(RCCP_GetAssetPath.GetAssetPath(RCCP_DemoContent.Instance.builtinShadersContent));
            AssetDatabase.Refresh();

        }

    }

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 5] Convert Built-In Lens Flares To SRP", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/URP/[Step 5] Convert Built-In Lens Flares To SRP", false, 0)]
    public static void ConvertBuiltInFlaresToSRP() {

        // Find all prefab assets in the project
        string[] allPrefabGuids = AssetDatabase.FindAssets("t:GameObject");

        int convertedCount = 0;
        int totalFlareCount = 0;

#if BCG_URP
        foreach (string guid in allPrefabGuids) {

            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefabRoot = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (prefabRoot == null)
                continue;

            // Get all built-in LensFlare components in this prefab (including children)
            LensFlare[] oldFlares = prefabRoot.GetComponentsInChildren<LensFlare>(true);
            List<GameObject> oldFlaresGO = new List<GameObject>();

            if (oldFlares.Length == 0)
                continue;

            totalFlareCount += oldFlares.Length;

            bool madeChange = false;
            foreach (LensFlare oldFlare in oldFlares) {

                oldFlaresGO.Add(oldFlare.gameObject);

                // Remove the old built-in LensFlare
                Undo.DestroyObjectImmediate(oldFlare);

                madeChange = true;

            }

            // If we removed or added components, we need to save the changes back to the prefab asset
            if (madeChange) {

                for (int i = 0; i < oldFlaresGO.Count; i++) {

                    LensFlareComponentSRP SRP = oldFlaresGO[i].AddComponent<LensFlareComponentSRP>();
                    SRP.lensFlareData = RCCP_Settings.Instance.lensFlareData as LensFlareDataSRP;
                    SRP.intensity = 0f;
                    SRP.scale = 1f;
                    SRP.attenuationByLightShape = false;
                    SRP.maxAttenuationDistance = 200f;

                }

                // Make sure we're modifying the *prefab asset* itself
                PrefabUtility.SavePrefabAsset(prefabRoot);
                convertedCount++;

            }

        }

#endif

        if (convertedCount > 0 || totalFlareCount > 0) {

            AssetDatabase.SaveAssets();
            Debug.Log($"Converted lens flares in {convertedCount} prefabs, total old flares removed: {totalFlareCount}");

        }

    }

    // Renamed "URP/To Builtin" to "Render Pipeline/Builtin"
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/Builtin/[Step 1] Import Builtin Shaders", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/Builtin/[Step 1] Import Builtin Shaders", false, 0)]
    public static void BuiltinPackage() {

        EditorUtility.DisplayDialog("Realistic Car Controller Pro | Importing Builtin Shaders", "Builtin shaders will be imported and builtin custom URP shaders will be removed.", "Proceed");

        AssetDatabase.ImportPackage(RCCP_AddonPackages.Instance.GetAssetPath(RCCP_DemoContent.Instance.builtinShaderPackage), true);
        AssetDatabase.importPackageCompleted += CompletedBuiltinPackageImport;

    }

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/Builtin/[Step 2] Convert All Demo Materials To Builtin", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/Builtin/[Step 2] Convert All Demo Materials To Builtin", false, 0)]
    public static void Builtin() {

        EditorUtility.DisplayDialog("Realistic Car Controller Pro | Converting All Demo Materials To builtin", "All demo materials will be converted to default shaders.", "Convert");

        for (int i = 0; i < RCCP_DemoMaterials.Instance.demoMaterials.Length; i++) {

            if (RCCP_DemoMaterials.Instance.demoMaterials[i] != null && RCCP_DemoMaterials.Instance.demoMaterials[i].material != null) {

                if (RCCP_DemoMaterials.Instance.demoMaterials[i].DefaultShader != null && RCCP_DemoMaterials.Instance.demoMaterials[i].DefaultShader != "")
                    RCCP_DemoMaterials.Instance.demoMaterials[i].material.shader = Shader.Find(RCCP_DemoMaterials.Instance.demoMaterials[i].DefaultShader);

            }

        }

    }

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/Builtin/[Step 3] Convert All Demo Vehicle Body Materials To Builtin", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/Builtin/[Step 3] Convert All Demo Vehicle Body Materials To Builtin", false, 0)]
    public static void BuiltinBodyShader() {

        EditorUtility.DisplayDialog("Realistic Car Controller Pro | Converting All Demo Body Materials To Builtin", "Shaders of the demo vehicles will be converted to builtin shader named ''RCCP Car Body Shader''.", "Convert");

        for (int i = 0; i < RCCP_DemoMaterials.Instance.vehicleBodyMaterials.Length; i++)
            RCCP_DemoMaterials.Instance.vehicleBodyMaterials[i].shader = Shader.Find("RCCP Car Body Shader");

        for (int i = 0; i < RCCP_DemoMaterials.Instance.wheelBlurMaterials.Length; i++)
            RCCP_DemoMaterials.Instance.wheelBlurMaterials[i].shader = Shader.Find("RCCP_WheelBlur");

    }

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/Builtin/[Step 4] Remove URP Shaders", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Render Pipeline/Builtin/[Step 4] Remove URP Shaders", false, 0)]
    public static void RemoveURPShader() {

        if (EditorUtility.DisplayDialog("Realistic Car Controller Pro | Removing URP Shaders", "URP shaders will be removed, otherwise you'll have warnings on your console. You can import them again from welcome window of the RCCP (Tools --> BCG --> RCCP --> Welcome Window)'.", "Remove URP Shaders", "Cancel")) {

            FileUtil.DeleteFileOrDirectory(RCCP_GetAssetPath.GetAssetPath(RCCP_DemoContent.Instance.URPShadersContent));
            AssetDatabase.Refresh();

        }

    }

    #region Help
    // Renamed "Help" to "Documentation & Support"
    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller Pro/Documentation & Support", false, 0)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller Pro/Documentation & Support", false, 0)]
    public static void Help() {

        EditorUtility.DisplayDialog("Realistic Car Controller Pro | Contact", "Please include your invoice number while sending a contact form. I usually respond within a business day.", "Close");

        string url = "https://www.bonecrackergames.com/contact/";
        Application.OpenURL(url);

    }
    #endregion Help

    private static void CompletedURPPackageImport(string packageName) {

        FileUtil.DeleteFileOrDirectory(RCCP_GetAssetPath.GetAssetPath(RCCP_DemoContent.Instance.builtinShadersContent));
        AssetDatabase.Refresh();

    }

    private static void CompletedBuiltinPackageImport(string packageName) {

        FileUtil.DeleteFileOrDirectory(RCCP_GetAssetPath.GetAssetPath(RCCP_DemoContent.Instance.URPShadersContent));
        AssetDatabase.Refresh();

    }

}
