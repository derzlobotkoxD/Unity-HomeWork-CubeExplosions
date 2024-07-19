using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Camera _camera;

    [Range(0, 100)][SerializeField] private float _maxDistence = 35f;
    [Range(0, 5)][SerializeField] private float _radius = 1f;

    private Ray _ray;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Cast();
    }

    private void Cast()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(_ray.origin, _ray.direction * _maxDistence, Color.red);

        if (Physics.Raycast(_ray, out RaycastHit hit, _maxDistence, _mask))
            if (hit.collider.TryGetComponent<IClickable>(out IClickable clickable))
                clickable.OnClick();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, _mask))
            Gizmos.DrawSphere(hit.point, _radius);
    }
}
