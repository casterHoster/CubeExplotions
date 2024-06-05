using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Keeper : MonoBehaviour
{
    [SerializeField] private Spawner spawner;

    private List<Cube> _cubeList;

    public UnityAction<Cube> CubeRemoved;

    private void OnEnable()
    {
        spawner.CubeCreated += AddCube;
    }

    private void OnDisable()
    {
        spawner.CubeCreated -= AddCube;
    }

    private void Awake()
    {
        _cubeList = new List<Cube>();
    }

    private void Exclude(Cube cube)
    {
        CubeRemoved?.Invoke(cube);
        _cubeList.Remove(cube);
        cube.Explode();
    }

    private void AddCube(Cube cube)
    {
        _cubeList.Add(cube);
        Explotion explotion = cube.GetExplotion();
        explotion.Pushed += Exclude;
    }
}
