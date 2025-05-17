//----------------------------------------------
//        City Car Driving Simulator
//
// Copyright © 2014 - 2025 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

class CCDS_PreBuildChecker : IPreprocessBuildWithReport {

    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report) {

        Object managerTexture;

        managerTexture = Resources.Load("Editor Icons/CCDS_Banner");

        if (managerTexture)
            managerTexture.hideFlags = HideFlags.None;

    }

}