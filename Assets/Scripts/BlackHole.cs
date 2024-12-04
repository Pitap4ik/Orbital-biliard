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

    void Func1(int param1, int param2){}

    void Func1(float param1, float param2){}
}
