using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour, IClickable
{
    private float _maximumChange = 100f;
    private float _minimumChange = 0f;

    public event UnityAction<Cube> Destroyed;

    public float ChangeToSplit { get; private set; } = 100f;

    public void Initialization(Vector3 scale, float change)
    {
        transform.localScale = scale;
        ChangeToSplit = change;
    }

    public void OnClick() =>
        TrySplite();

    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void TrySplite()
    {
        float change = Random.Range(_minimumChange, _maximumChange);

        if (change <= ChangeToSplit)
            Destroyed?.Invoke(this);

        Destroy(gameObject);
    }
}