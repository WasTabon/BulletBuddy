using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    [HideInInspector] public List<Vector3> eventsPosition;
    public Action listGrown; 
}
