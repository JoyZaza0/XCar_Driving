//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright � 2014 - 2025 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCCP_TryGetComponentInChildren {

    public static T Get<T>(Transform transform) {

        T comp;

        for (int i = 0; i < transform.childCount; i++) {

            if (transform.GetChild(i).TryGetComponent<T>(out comp))
                return comp;

        }

        return default;

    }

}
