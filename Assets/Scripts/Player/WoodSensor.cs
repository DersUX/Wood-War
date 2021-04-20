using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WoodSensor : MonoBehaviour
{
    [SerializeField] private LayerMask _detectsOnLayers;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private int _maxVisibleObjects = 5;
    [SerializeField] private float _updateInterval = 1f;

    private readonly List<Wood> _visibleWoods = new List<Wood>();
    private readonly List<GameObject> _visibleLumbers = new List<GameObject>();
    private float _currentTime = 0f;

    public List<Wood> VisibleWoods => _visibleWoods;
    public List<GameObject> VisibleLumbers => _visibleLumbers;

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _updateInterval)
        {
            GetVisibleObjects();
            _currentTime = 0f;
        }
    }

    private void GetVisibleObjects()
    {
        _visibleWoods.Clear();
        _visibleLumbers.Clear();

        Collider[] hitColliders = new Collider[_maxVisibleObjects];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, hitColliders, _detectsOnLayers);

        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i].TryGetComponent(out Wood wood))
                _visibleWoods.Add(wood);

            if (hitColliders[i].TryGetComponent(out Lumber lubmer))
                _visibleLumbers.Add(hitColliders[i].gameObject);
        }
    }

    public GameObject TryGetNearestVisibleLumber()
    {
        GameObject result = null;

        if (VisibleLumbers.Count != 0)
        {
            result = VisibleLumbers.OrderBy(a => Vector3.Distance(a.transform.position, transform.position)).First();

            Destroy(result.GetComponent<Lumber>());
            _visibleLumbers.Clear();
        }

        return result;
    }
}
