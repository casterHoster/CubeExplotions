using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _maxCubesCount;
    [SerializeField] private Cube _cubeSample;
    [SerializeField] private float _cubeSize;
    [SerializeField] private List<Material> _materials;

    private void Start()
    {
        var cube = Instantiate(_cubeSample);
        cube.transform.localScale = new Vector3(_cubeSize, _cubeSize, _cubeSize);
        CreateCubes(cube);
    }

    private void CreateCubes(Cube cube)
    {
        int cubesCount = Random.Range(1, _maxCubesCount + 1);
        cube.transform.localScale = new Vector3(cube.transform.localScale.x / 2, cube.transform.localScale.y / 2, cube.transform.localScale.z / 2);
        List<Rigidbody> cubesBodies = new List<Rigidbody>();

        for (int i = 0; i < cubesCount; i++)
        {
            int cubeColor = Random.Range(0, _materials.Count);
            Cube newCube = Instantiate(cube, cube.transform.position, Quaternion.identity);
            newCube.Init(_materials[cubeColor], cube.DecayProbabilityPercent);
            newCube.IsSplit += CreateCubes;

            if (newCube.TryGetComponent(out Rigidbody newCubeBody))
            {
            cubesBodies.Add(newCubeBody);
            }
        }

        cube.Explode(cubesBodies);
    }
}
