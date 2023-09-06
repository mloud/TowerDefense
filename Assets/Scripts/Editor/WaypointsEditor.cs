using OneDay.TowerDefense.Base.Systems;
using UnityEditor;
using UnityEngine;

namespace OneDay.TowerDefense.CustomEditor
{
    [UnityEditor.CustomEditor(typeof(WaypointSystem))]
    public class WaypointsEditor : Editor
    {
        private WaypointSystem WaypointSystem => target as WaypointSystem;

        private GUIStyle TextStyle { get; set; }
        private Vector3 TextOffset => new(0.35f, -0.35f, 0);

        private float LastClickTime { get; set; }
        private float DoubleTapTimeThreshold => 0.4f;
        
        private void OnEnable()
        {
            if (TextStyle == null)
            {
                TextStyle = new GUIStyle();
                TextStyle.fontStyle = FontStyle.Bold;
                TextStyle.fontSize = 20;
                TextStyle.normal.textColor = Color.white;
            }
        }

        private void OnSceneGUI()
        {
            Handles.color = Color.yellow;
            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < WaypointSystem.WaypointsCount; i++)
            {
                var waypointPosition = WaypointSystem.GetWaypointPosition(i);
                var newWaypointPosition = Handles.FreeMoveHandle(
                    waypointPosition, 
                    Quaternion.identity, 
                    0.6f, 
                    new Vector3(0.3f, 0.3f, 0.3f), 
                    Handles.SphereHandleCap);

                Handles.Label(
                    waypointPosition + TextOffset,  
                    (i+ 1).ToString(),
                    TextStyle);
                
                bool hasChanged = EditorGUI.EndChangeCheck();

                if (hasChanged)
                {
                    Undo.RecordObject(target, "WaypointsEditor - Free Move Handle");
                    WaypointSystem.SetWaypointPosition(i, newWaypointPosition, true);
                }
            }
        }
    }
}
