using System;
using UnityEngine;

public class MineCap : MonoBehaviour
{
    [SerializeField] private MineMainCap _mineMainCap;

    private void Update()
    {
        OpenCap();
    }

    private void OnMouseDrag()
    {
        RotateCap();
    }
    
    private void RotateCap()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
 
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (rotationZ < 0f)
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    private void OpenCap()
    {
        if (transform.rotation.eulerAngles.z <= 185f && transform.rotation.eulerAngles.z >= 180f && _mineMainCap.capIsOpen == false)
            _mineMainCap.capIsOpen = true;
    }
}
