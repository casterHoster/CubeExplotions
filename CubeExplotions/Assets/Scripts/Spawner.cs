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

    public UnityAction<Cube> CubeCreated;

    private void Start()
    {
        _cubeSample.transform.localScale = new Vector3(_cubeSize,_cubeSize,_cubeSize);
        _keeper.CubeRemoved += Create;
        Create(_cubeSample);
    }

    private void Create(Cube cube)
    {
        int cubesCount = Random.Range(1, _maxCubesCount);
        cube.transform.localScale = new Vector3(cube.transform.localScale.x / 2, cube.transform.localScale.y / 2, cube.transform.localScale.z / 2);

        for (int i = 0; i < cubesCount; i++)
        {
            int cubeColor = Random.Range(0, _materials.Count);
            Cube newCube = Instantiate(cube, cube.transform.position, Quaternion.identity);
            newCube.SetMaterial(_materials[cubeColor]);
            CubeCreated?.Invoke(newCube);
        }
    }
}
