using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WoodSensor))]
public class Player : MonoBehaviour
{
    [SerializeField] private WoodSensor _sensor;
    [SerializeField] private PlayerBag _bag;
    [SerializeField] private float _hitDelay = 1f;
    [SerializeField] private float _damage = 25f;

    private float _currentTime = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0) && _sensor.VisibleWoods.Count > 0)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _hitDelay)
            {
                foreach (var wood in _sensor.VisibleWoods)
                    wood.TakeDamage(_damage);

                _currentTime = 0f;
            }
        }

        if (_sensor.VisibleLumbers.Count > 0)
            _bag.AddItem(_sensor.TryGetNearestVisibleLumber());
    }
}
