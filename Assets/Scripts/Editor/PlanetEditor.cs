using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;
    Editor shapeEditor;
    Editor colorEditor;
    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.GeneratePlanet();
            }
        }
        if (GUILayout.Button("Generate"))
        {
            planet.GeneratePlanet();
        }
        DrawSettingEditor(planet.shapeSettings,planet.OnShapeSettingChanged,ref planet.shapeSettingfoldOut,ref shapeEditor);
        DrawSettingEditor(planet.colorSettings, planet.OnColorSettingChanged, ref planet.colorSettingfoldOut, ref colorEditor);
    }

    void DrawSettingEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings == null)
            return;
        foldout = EditorGUILayout.InspectorTitlebar(foldout, editor);

        using(var check = new EditorGUI.ChangeCheckScope())
        {
            if (foldout)
            {
                CreateCachedEditor(settings, null, ref editor);
                editor.OnInspectorGUI();
                if (check.changed)
                {
                    if (onSettingsUpdated != null)
                    {
                        onSettingsUpdated();
                    }
                }
            }
        }
    }
    private void OnEnable()
    {
        planet = (Planet)target;
    }
}
