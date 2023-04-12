using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private GameObject _map;

    private void Start()
    {
        _map = GameObject.FindWithTag("Map");
        _map.SetActive(false);
    }

    private void Update()
    {
        ChangeMapActive();
    }

    private void ChangeMapActive()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            _map.SetActive(!_map.activeSelf);
    }
}
