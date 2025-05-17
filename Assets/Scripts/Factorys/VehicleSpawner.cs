using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] private VehicleConfig[] vehiclesConfig;

    public List<ACCDS_Vehicle> GetVehicles(Transform parent = null)
    {
        List<ACCDS_Vehicle> spawnedVehicles = new List<ACCDS_Vehicle>();

        for (int i = 0; i < vehiclesConfig.Length; i++)
        {
            var spawnPoint = vehiclesConfig[i].SpawnPoint;

            ACCDS_Vehicle vehicle;
            var loadedVehicle = Resources.Load<ACCDS_Vehicle>(vehiclesConfig[i].Path);

            if (parent != null)
                vehicle = Instantiate(loadedVehicle, spawnPoint.position, spawnPoint.rotation, parent);
            else
                vehicle = Instantiate(loadedVehicle, spawnPoint.position, spawnPoint.rotation);


            if (vehicle != null)
            {
                UpdateVehicles(vehicle, vehiclesConfig[i]);
                spawnedVehicles.Add(vehicle);
                // Resources.UnloadAsset(loadedVehicle);
            }

        }

        return spawnedVehicles;
    }

    private void UpdateVehicles(ACCDS_Vehicle vehicle, VehicleConfig vehicleConfig)
    {
        RCCP_Engine engine = vehicle.GetComponentInChildren<RCCP_Engine>(true);
        RCCP_Stability stability = vehicle.GetComponentInChildren<RCCP_Stability>(true);
        RCCP_Differential differential = vehicle.GetComponentInChildren<RCCP_Differential>(true);

        if (engine)
            engine.maximumTorqueAsNM = vehicleConfig.engineTorque;

        if (stability)
        {
            stability.tractionHelperStrength = vehicleConfig.handling * .5f;
            stability.steerHelperStrength = vehicleConfig.handling * .5f;
        }

        if (differential)
        {
            differential.finalDriveRatio = Mathf.Lerp(5.31f, 2.8f, Mathf.InverseLerp(160f, 380f, vehicleConfig.speed));
        }
    }
}

[System.Serializable]
public class VehicleConfig
{
    public string Path;
    public Transform SpawnPoint;

    [Space()][Range(100f, 1000f)] public float engineTorque = 350f;
    [Range(0f, 1f)] public float handling = .1f;
    [Range(160f, 380f)] public float speed = 240f;
}