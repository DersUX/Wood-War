using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    [SerializeField] private int _maxCapacity = 15;
    [SerializeField] private int _rowsCount = 3;
    [SerializeField] private int _rowCapacity = 5;

    [SerializeField] private float _rowsBetween = 0.5f;
    [SerializeField] private float _itemBetween = 0.2f;

    [SerializeField] private Vector3 _itemScale = new Vector3(0.25f, 1, 0.25f);

    private List<Item> _inventory = new List<Item>();

    public void AddItem(GameObject gameObject)
    {
        if (_inventory.Count <= _maxCapacity)
        {
            int itemRow = 0, itemIndex = 0;

            if (_inventory.Count == 0)
            {
                itemRow = 1;
                itemIndex = 1;
            }
            else
            {
                if (_inventory.Last().Index < _rowCapacity)
                {
                    itemRow = _inventory.Last().Row;
                    itemIndex = _inventory.Last().Index + 1;
                }
                else if (_inventory.Last().Row < _rowsCount)
                {
                    itemRow = _inventory.Last().Row + 1;
                    itemIndex = 1;
                }
            }

            Item newItem = new Item(itemRow, itemIndex, gameObject);
            _inventory.Add(newItem);

            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            gameObject.transform.parent = transform;

            gameObject.transform.localPosition = GetItemPosition(_inventory.IndexOf(newItem));
            gameObject.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            gameObject.transform.localScale = _itemScale;
        }
    }

    private Vector3 GetItemPosition(int index)
    {
        Vector3 position = Vector3.zero;

        position.x = _rowsBetween * _inventory[index].Row;
        position.y = _itemBetween * _inventory[index].Index;

        return position;
    }

    private class Item
    {
        public int Row { get; private set; }
        public int Index { get; private set; }
        public GameObject Object { get; private set; }

        public Item(int row, int index, GameObject gameObject)
        {
            Row = row;
            Index = index;
            Object = gameObject;
        }
    }
}
