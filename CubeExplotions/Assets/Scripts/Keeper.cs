using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Keeper : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private List<Cube> _cubeList;

    public event UnityAction<Cube> CubeRemoved;

    private void OnEnable()
    {
        _spawner.CubeCreated += AddCube;
    }

    private void OnDisable()
    {
        _spawner.CubeCreated -= AddCube;
    }

    private void Awake()
    {
        _cubeList = new List<Cube>();
    }

    private void Exclude(Cube cube, bool isSplit)
    {
        if (isSplit)
        {
            CubeRemoved?.Invoke(cube);
        }

        _cubeList.Remove(cube);
    }

    private void AddCube(Cube cube)
    {
        _cubeList.Add(cube);
        cube.Pushed += Exclude;
    }
}
