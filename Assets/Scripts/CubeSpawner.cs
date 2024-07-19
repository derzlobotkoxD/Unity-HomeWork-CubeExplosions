using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private int _countStartCubes = 3;
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _startSpawnPoint;

    private int _maximumCubesSpawn = 6;
    private int _minimumCubesSpawn = 2;
    private int _factorScale = 2;
    private int _factorChangeToSplit = 2;

    private List<Cube> _cubes = new List<Cube>();

    public event UnityAction<List<Cube>, Vector3> Created;

    private void Awake()
    {
        for (int i = 0; i < _countStartCubes; i++)
            SpawnNewCube(_prefab, _startSpawnPoint.position);
    }

    private void CreateReducedCubes(Cube cube)
    {
        cube.Splitted -= CreateReducedCubes;

        int countCubes = Random.Range(_minimumCubesSpawn, _maximumCubesSpawn);
        List<Cube> cubes = new List<Cube>();

        Vector3 scale = cube.transform.localScale / _factorScale;
        float changeToSplit = cube.ChangeToSplit / _factorChangeToSplit;

        for (int i = 0; i < countCubes; i++)
        {
            Cube newCube = SpawnNewCube(cube, cube.transform.position);
            newCube.Initialization(scale, changeToSplit);

            cubes.Add(newCube);
        }

        Created?.Invoke(cubes, cube.transform.position);
    }

    private Cube SpawnNewCube(Cube cube, Vector3 startPosition)
    {
        Vector3 position = startPosition + Random.onUnitSphere * cube.transform.localScale.x;

        if (Physics.Linecast(startPosition, position, out RaycastHit hitInfo, _mask))
            position = hitInfo.point;

        Cube newCube = Instantiate(cube, position, Quaternion.identity);
        newCube.Splitted += CreateReducedCubes;
        _cubes.Add(newCube);

        return newCube;
    }

    private void OnDestroy()
    {
        foreach (var cube in _cubes)
            cube.Splitted -= CreateReducedCubes;
    }
}