using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explotion : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _force;

    private Cube _cube;

    public UnityAction<Cube> Pushed;

    private void Start()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnMouseUpAsButton()
    {
        Pushed?.Invoke(_cube);
    }

    public void Implement(List<Cube> cubes)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(cubes))
        {
            explodableObject.AddExplosionForce(_force, transform.position, _range);
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodableObjects(List<Cube> cubes)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _range);
        List<Rigidbody> reachesObjects = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            foreach (Cube cube in cubes)
            {
                if (hit.attachedRigidbody == cube.GetComponent<Rigidbody>())
                {
                    reachesObjects.Add(hit.attachedRigidbody);
                }
            }
        }

        return reachesObjects;
    }
}
