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
        Implement();
        Pushed?.Invoke(_cube);
        Destroy(gameObject);
    }

    private void Implement()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_force, transform.position, _range);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _range);
        List<Rigidbody> reachesObjects = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                reachesObjects.Add(hit.attachedRigidbody);
            }
        }

        return reachesObjects;
    }
}
