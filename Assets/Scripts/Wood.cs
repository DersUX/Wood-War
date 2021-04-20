using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wood : MonoBehaviour
{
    [SerializeField] private GameObject _lumberPrefab;
    [SerializeField] private int _lumberCount = 4;
    [SerializeField] private float _lumbersBetween = 2f;
    [SerializeField] private float _lumbersDropSpeed = 2f;
    [SerializeField] private float _maxHealth = 100f;

    private float _health;
    private float _requiredDamage;
    private float _currentRequiredDamage = 0f;

    private bool _isLumberFalls = false;

    private List<GameObject> _lumbers = new List<GameObject>();

    private void Start()
    {
        _health = _maxHealth;
        _requiredDamage = _maxHealth / _lumberCount;

        for (int i = 0; i < _lumberCount; i++)
        {
            GameObject spawned = Instantiate(_lumberPrefab, transform);

            if (i > 0)
                spawned.transform.position = GetNewLumberPosition(spawned.transform.position, i);

            _lumbers.Add(spawned);
        }
    }

    private void Update()
    {
        if (_isLumberFalls)
        {
            if (_lumbers[1].transform.position.y == _lumbersBetween)
                _isLumberFalls = false;

            for (int i = 1; i < _lumbers.Count; i++)
            {
                Vector3 currentPosition = _lumbers[i].transform.position;

                _lumbers[i].transform.position = Vector3.MoveTowards(currentPosition, GetNewLumberPosition(currentPosition, i), Time.deltaTime * _lumbersDropSpeed);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _currentRequiredDamage += damage;

        if (_health <= 0)
            Destroy(gameObject);

        if (_currentRequiredDamage >= _requiredDamage)
        {
            _currentRequiredDamage = 0;
            LumberFelling();
        }
    }

    private void LumberFelling()
    {
        if (_lumbers.Count > 1)
        {
            if (_lumbers[1].TryGetComponent<Rigidbody>(out Rigidbody lumber))
            {
                _lumbers[1].AddComponent<Lumber>();

                lumber.isKinematic = false;
                lumber.AddForce(new Vector3(Random.Range(100f, 500f), Random.Range(100f, 500f)));

                _lumbers.RemoveAt(1);

                _isLumberFalls = true;
            }
        }
    }

    private Vector3 GetNewLumberPosition(Vector3 position, int index)
    {
        position.y = _lumbersBetween * index;
        return position;
    }
}
