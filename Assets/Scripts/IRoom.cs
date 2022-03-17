using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IRoom : MonoBehaviour
{
    public abstract void BuildRoom(Vector3 origin, List<int> dimensions);
}
