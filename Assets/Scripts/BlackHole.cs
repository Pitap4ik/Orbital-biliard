using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public Transform Transform { get; private set; }
    [SerializeField] private Vector3 _rotationSpeed;

    void Start()
    {
        Transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Transform.Rotate(_rotationSpeed * Time.deltaTime);
    }
}
