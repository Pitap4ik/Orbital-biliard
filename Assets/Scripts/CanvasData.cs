using UnityEngine;

[CreateAssetMenu(fileName = "CanvasData", menuName = "Scriptable Objects/CanvasData")]
public class CanvasData : ScriptableObject
{
    [Range(0f, 2f)]
    public float kValue;
}