using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(WeaponData))]
public class WeaponDataEditor : Editor 
{
    WeaponData weaponData;
    string[] weaponSubtypes;
    int selectedWeaponSubtype;

    private void OnEnable()
    {
        weaponData = (WeaponData)target;

        System.Type baseType = typeof(Weapon);
        List<System.Type> subTypes = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => baseType.IsAssignableFrom(p) && p != baseType)
            .ToList();

        List<string> subTypesString = subTypes.Select(t => t.Name)
            .ToList();
        subTypesString.Insert(0, "None"); // Add a "None" option for no subtype
        weaponSubtypes = subTypesString.ToArray();

        selectedWeaponSubtype = Math.Max(0, Array.IndexOf(weaponSubtypes, weaponData.behaviour));
    }

    public override void OnInspectorGUI()
    {
        selectedWeaponSubtype = EditorGUILayout.Popup("Behaviour", Math.Max(0,selectedWeaponSubtype), weaponSubtypes);

        if (selectedWeaponSubtype > 0) // If a subtype is selected
        {
            weaponData.behaviour = weaponSubtypes[selectedWeaponSubtype].ToString();
            EditorUtility.SetDirty(weaponData);
            DrawDefaultInspector(); // Draw the default inspector for the selected subtype
        }
    }
}
