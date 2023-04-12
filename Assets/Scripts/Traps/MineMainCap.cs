using System;
using UnityEngine;

public class MineMainCap : MonoBehaviour
{
    public bool capIsOpen;

    private void OnMouseDrag()
    {
        if (capIsOpen)
            MoveWithMose();
    }
    
    private void MoveWithMose()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }
}
