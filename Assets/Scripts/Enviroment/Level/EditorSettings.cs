using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EditorSettings", order = 1)]
public class EditorSettings : ScriptableObject
{
    public GameObject endObjectPrefab;

    [Header("Gizmos")]
    [SerializeField]public float gizmoYOffset = 0;
    [SerializeField]public float gizmoScale = 1;
}