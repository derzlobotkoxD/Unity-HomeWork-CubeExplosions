using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : MonoBehaviour
{
    private int _maximumCubesSpawn = 6;
    private int _minimumCubesSpawn = 2;

    public event UnityAction<List<Cube>> Created;

    private void Awake()
    {
        Cube[] cubes = FindObjectsByType<Cube>(FindObjectsSortMode.None);

        foreach (Cube cube in cubes)
            cube.Destroyed += Create;
    }

    private void Create(Cube cube)
    {
        cube.Destroyed -= Create;

        int countCubes = Random.Range(_minimumCubesSpawn, _maximumCubesSpawn);
        List<Cube> cubes = new List<Cube>();


        Vector3 scale = cube.transform.localScale / 2f;
        float changeToSplit = cube.ChangeToSplit / 2f;

        for (int i = 0; i < countCubes; i++)
        {
            GameObject newObject = Instantiate(cube.gameObject, cube.transform.position, Quaternion.identity);
            Cube newCube = newObject.GetComponent<Cube>();

            newCube.Initialization(scale, changeToSplit);

            newCube.Destroyed += Create;
            cubes.Add(newCube);
        }

        Created?.Invoke(cubes);
    }
}