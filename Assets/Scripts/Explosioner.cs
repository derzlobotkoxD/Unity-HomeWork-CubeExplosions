using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private float _force = 1000;

    private void OnEnable()
    {
        _spawner.Created += Explode;
    }

    private void OnDisable()
    {
        _spawner.Created -= Explode;
    }

    private void Explode(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

            cube.transform.Rotate(Vector3.up * Random.Range(0, 360f));
            rigidbody.AddForce(cube.transform.forward * _force, ForceMode.Force);
        }
    }
}