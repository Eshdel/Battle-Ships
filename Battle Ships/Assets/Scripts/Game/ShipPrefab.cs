using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPrefab : MonoBehaviour
{
    public int Width;
    public int Lenght;
    public int Count;
    public int MaxCount;
    public Ship Ship;

    public void UpdateCount() {
        Count = MaxCount;
    }
}
