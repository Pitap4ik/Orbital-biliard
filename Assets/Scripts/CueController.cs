using System;
using UnityEngine;
using MathematicalExtensions;

enum CueMode{
    Inactive, Waiting, Calibring, Powering
}

public class CueController : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _offset;
    [SerializeField] private CueMode _currentMode;
    [SerializeField] private float _strikingVelocity;
    private CanvasController _canvasController;
    public GameObject Target { get => _target; set => _target = value; }
    public float Offset { get => _offset; set => _offset = value; }
    internal CueMode CurrentMode { get => _currentMode; private set => _currentMode = value; }
    public float StrikingVelocity { get => _strikingVelocity; set => _strikingVelocity = value; }
    public Vector3 defaultPosition;
    private Vector3 _mousePosition;
    private float _baseZAngle;

    void Start()
    {
        defaultPosition = transform.position;
        _canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        _baseZAngle = transform.eulerAngles.z;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q)){
            switch (CurrentMode){
                case CueMode.Waiting:
                    _canvasController.PausePhysics();
                    CurrentMode = CueMode.Calibring;
                    break;
                case CueMode.Calibring:
                    StrikeTarget();
                    _canvasController.UnpausePhysics();
                    transform.position = defaultPosition;
                    CurrentMode = CueMode.Waiting;
                    break;
                // case CueMode.Powering:
                //     StrikeTarget();
                //     _canvasController.UnpausePhysics();
                //     CurrentMode = CueMode.Waiting;
                //     break;
            }
        }

        switch (CurrentMode){
            case CueMode.Calibring:
                AdjustTransformToTarget();
                break;
        }
    }

    void StrikeTarget(){
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = Target.transform.position;
        float strikingAngle = Trigonometry.GetAngleToTargetByAtan2(targetPosition, currentPosition);
        float velocityX = MathF.Cos(strikingAngle) * StrikingVelocity;
        float velocityY = MathF.Sin(strikingAngle) * StrikingVelocity;

        Target.gameObject.GetComponent<PlanetController>().Velocity = new Vector2(velocityX, velocityY);
        Debug.Log($"{Trigonometry.ConvertToDegrees(strikingAngle)}, {new Vector2(velocityX, velocityY)}");
    }

    void AdjustTransformToTarget(){
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPosition = Target.transform.position;
        float angleDegrees = _baseZAngle + Trigonometry.ConvertToDegrees(Trigonometry.GetAngleToTargetByAtan2(mousePosition, targetPosition));

        transform.position = GetOffsetPositionBetweenPoints(mousePosition, targetPosition, Offset);
        transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
    }

    Vector2 GetOffsetPositionBetweenPoints(Vector2 pointerPosition, Vector2 targetPosition, float offset){
        float angleRadians = Trigonometry.GetAngleToTargetByAtan2(pointerPosition, targetPosition);
        float radius = Trigonometry.GetHypothenusBySin(pointerPosition.y - targetPosition.y, angleRadians);
        float hypothenus = radius > 0 ? radius - offset: radius + offset;
        float x = pointerPosition.x - MathF.Cos(angleRadians) * hypothenus;
        float y = pointerPosition.y - MathF.Sin(angleRadians) * hypothenus;
        return new Vector2(x, y);
    }

    Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
