using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _targetedActor;
    public List<GameObject> SpawnedGameObjects { get; private set; }
    public Transform Transform { get; private set; }
    public GameObject TargetedActor { get { return _targetedActor; } private set { _targetedActor = value; } }

    private void Start()
    {
        Transform = GetComponent<Transform>();
        SpawnedGameObjects = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            Spawn(TargetedActor, Transform);
        }

        if (Input.GetKeyUp(KeyCode.Delete))
        {
            DespawnAll();
        }
    }

    private void Spawn(GameObject targetedActor, Transform transform)
    {
        var newActor = Instantiate(targetedActor, new Vector3(transform.position.x, transform.position.y, 1f), transform.rotation);
        newActor.SetActive(true);
        newActor.GetComponent<SampleActorManager>().VelocityMultiplier = Random.Range(-10f, 10f);
        SpawnedGameObjects.Add(newActor);
        Debug.Log("Actor was spawned");
    }

    private void DespawnAll()
    {
        foreach (GameObject gameObject in SpawnedGameObjects) { Destroy(gameObject); }
    }
}
