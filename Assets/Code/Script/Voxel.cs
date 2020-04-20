using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Voxel : MonoBehaviour
{
    public int x, y;
    public Vector3 topOffset;
    private GameObject scenarioProp;

    void Awake()
    {
        topOffset = transform.up * 0.5f;
    }

    private void OnMouseDown()
    {
        EventManager.Instance.DispatchEvent(this);
    }

    public void SpawnScenarioProp(GameObject prop)
    {
        scenarioProp = Instantiate(prop, transform);
        scenarioProp.transform.localPosition = topOffset;
    }

    public void DestroyScenarioProp()
    {
        Destroy(scenarioProp);
    }

    public void SpawnAt(GameObject obj) 
    {
        Instantiate(obj, transform.position + topOffset, Quaternion.identity);
    }

    public void SnapTo(GameObject obj)
    {
        obj.transform.position = transform.position + topOffset;
    }
}
