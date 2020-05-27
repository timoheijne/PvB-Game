using System;
using UnityEngine;
using UnityEngine.UI;

public class BlockInventory : UnityEngine.MonoBehaviour
{
    [SerializeField]
    private Transform _contentBase;

    [SerializeField]
    private RectTransform Workspace;

    // STEP 1: Load all blocks
    // STEP 2: Instantiate all in scroll view
    // STEP 3: Attach InventoryBlockInstantiator & Pass Prefab
    // STEP 4: Disable all other scripts

    private void Start()
    {
        LoadBlocks();
    }

    private void LoadBlocks()
    {
        InventoryItem[] _items = Resources.LoadAll<InventoryItem>("Visual Scripting Blocks");
        foreach (InventoryItem _item in _items)
        {
            GameObject _blockObject = Instantiate(_item.Prefab, _contentBase);
            MonoBehaviour[] _scripts = _blockObject.GetComponents<MonoBehaviour>();
            foreach(MonoBehaviour _script in _scripts)
            {
                if (_script.GetType() != typeof(Image))
                {
                    Destroy(_script);
                }
            }

            InventoryBlockInstantiator _blockInstantiator = _blockObject.AddComponent<InventoryBlockInstantiator>();
            _blockInstantiator.SetPrefab(_item.Prefab);
            _blockInstantiator.Workspace = Workspace;

            TutorialReferenceObject _referenceObject = _blockObject.AddComponent<TutorialReferenceObject>();
            _referenceObject.ReferenceID = $"VSB-{_item.ItemName}";
        }
    }
}