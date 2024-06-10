using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]

public class Explotion : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _force;

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
        List<Rigidbody> reachesObjects = new List<Rigidbody>();

            foreach (Cube cube in cubes)
            {
                if (cube.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    reachesObjects.Add(rigidbody);
                }
            }

        return reachesObjects;
    }
}
