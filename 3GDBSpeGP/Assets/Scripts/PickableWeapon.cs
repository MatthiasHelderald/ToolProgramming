using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class PickableWeapon : MonoBehaviour
{
    public WeaponDataWrapper data;

    private void OnValidate()
    {
        GetComponentInChildren<MeshFilter>().sharedMesh = data.weaponData.model;
    }
}
