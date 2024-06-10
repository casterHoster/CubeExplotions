using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _maxCubesCount;
    [SerializeField] private Cube _cubeSample;
    [SerializeField] private Keeper _keeper;
    [SerializeField] private float _cubeSize;
    [SerializeField] private List<Material> _materials;

    private int _maxProbability = 100;

    public event UnityAction<Cube> CubeCreated;

    private void Start()
    {
        _cubeSample.transform.localScale = new Vector3(_cubeSize, _cubeSize, _cubeSize);
        _keeper.CubeRemoved += Create;
        Create(_cubeSample);
    }

    private void Create(Cube cube)
    {
        if (CalculateCreation(cube.DecayProbability))
        {
            int cubesCount = Random.Range(1, _maxCubesCount + 1);
            cube.transform.localScale = new Vector3(cube.transform.localScale.x / 2, cube.transform.localScale.y / 2, cube.transform.localScale.z / 2);
            List<Cube> cubes = new List<Cube>();

            for (int i = 0; i < cubesCount; i++)
            {
                int cubeColor = Random.Range(0, _materials.Count);
                Cube newCube = Instantiate(cube, cube.transform.position, Quaternion.identity);
                newCube.Init(_materials[cubeColor]);
                CubeCreated?.Invoke(newCube);
                cubes.Add(newCube);
            }

            cube.AddLittleCubes(cubes);
        }
    }

    private bool CalculateCreation(int currentProbability)
    {
        int probabilityCreate = Random.Range(0, _maxProbability + 1);
        return probabilityCreate < currentProbability;
    }
}
