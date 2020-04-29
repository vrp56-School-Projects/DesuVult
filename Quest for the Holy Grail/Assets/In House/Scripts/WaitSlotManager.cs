using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSlotManager : MonoBehaviour
{
    private List<GameObject> _slots;
    [SerializeField]
    private int count = 4;
    [SerializeField]
    public float distance = 1.5f;
    [SerializeField]
    private float degrees = 360f;


    // Start is called before the first frame update
    void Start()
    {
        _slots = new List<GameObject>();
        for (int i = 0; i < count; ++i)
        {
            _slots.Add(null);
        }
    }

    public Vector3 GetSlotPosition(int index)
    {
        float degreesPerIndex = degrees / count;
        Vector3 pos = transform.localPosition;
        Vector3 offset = new Vector3(0f, 0f, distance);
        return pos + (Quaternion.Euler(new Vector3(0f, degreesPerIndex * index, 0f)) * offset);
    }

    public int Reserve(GameObject attacker)
    {
        Vector3 bestPosition = transform.position;
        Vector3 offset = (attacker.transform.position - bestPosition).normalized * distance;
        bestPosition += offset;
        int bestSlot = -1;
        float bestDist = 99999f;
        for (int index = 0; index < _slots.Count; ++index)
        {
            if (_slots[index] != null)
                continue;
            var dist = (GetSlotPosition(index) - bestPosition).sqrMagnitude;
            if (dist < bestDist)
            {
                bestSlot = index;
                bestDist = dist;
            }
        }
        if (bestSlot != -1)
            _slots[bestSlot] = attacker;
        return bestSlot;
    }

    public void Release(int slot)
    {
        _slots[slot] = null;
    }

    void OnDrawGizmosSelected()
    {
        for (int index = 0; index < count; ++index)
        {
            if (_slots == null || _slots.Count <= index || _slots[index] == null)
                Gizmos.color = Color.yellow;
            else
                Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetSlotPosition(index), 0.5f);
        }
    }
}
