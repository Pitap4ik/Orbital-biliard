using System;
using UnityEngine;

public class CueController : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    public GameObject Target { get => _target; set => _target = value; }
    private Vector3 _mousePosition;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = GetAngleToTargetInDegrees(mousePosition, Target.transform.position);
            float radius1 = 10f;
            float radius2 = GetDistanceFromPoint(mousePosition, Target.transform.position) - radius1;
            
            // if (GetDistanceFromPoint(mousePosition, Target.transform.position) < radius1){
            //     radius2 = radius1;
            // }

            float x = mousePosition.x - MathF.Sin(angle) * radius2;
            float y = mousePosition.y - MathF.Cos(angle) * radius2;

            Debug.Log($"{MathF.Sin(angle) * radius2}");

            transform.position = new Vector2 (x, y);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public float GetDistance(Vector2 objectPosition){
        return MathF.Sqrt(MathF.Pow(objectPosition.x, 2) + MathF.Pow(objectPosition.y, 2));
    }

    public float GetDistanceFromPoint(Vector2 objectPosition, Vector2 pointPosition){
        return GetDistance(new Vector2(objectPosition.x - pointPosition.x, objectPosition.y - pointPosition.y));
    }

    // public float GetHypothenusBySin(float oppositeQuotinent1, float angle){
    //     return oppositeQuotinent1 / MathF.Sin(angle);
    // }

    void OnMouseDown()
    {
        _mousePosition = Input.mousePosition - GetMousePosition();
    }

    void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
    }

    private void OnMouseUp()
    {
        
    }

    public float GetAngleToTargetInDegrees(Vector2 objectPosition, Vector2 targetPosition){
        float oppositeQuotinent = (objectPosition.y - targetPosition.y) ;
        float adjacentQuotinent = (objectPosition.x - targetPosition.x);
        float angle = 180 / MathF.PI * MathF.Atan(oppositeQuotinent / adjacentQuotinent);
        return angle;
    }

    Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
