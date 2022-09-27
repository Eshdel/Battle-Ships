using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BattleField : NetworkBehaviour
{
    public readonly uint Size = 10;

    public NetworkList<bool> attackedAreasList = new NetworkList<bool>();

    public NetworkList<bool> fillAreasList= new NetworkList<bool>();
    
    public Vector2 Offset;

    [SerializeField] private GameObject FieldPrefab;
    
    public bool DestroyWithSpawner;   
    
    private GameObject m_PrefabInstance;
    
    private NetworkObject m_SpawnedNetworkObject;
    
    public int Index(int row, int column)
    {
        return (int)(row + column + row * (Size - 1));
    }

    public void InitalizeAreas()
    {
        for (int i = 0; i < Size * Size; i++)
        {
            attackedAreasList.Add(false);
            fillAreasList.Add(false);
        }
    }

    public bool PlaceObject(int row, int column, int lenght, int width)
    {
        //false error
        //true success
        Debug.Log($"First Postion {row} ,{column}");
        
        if (CheckOnFreeArea(row,column,lenght,width))
        {
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    fillAreasList[Index(row + i, column + j)] = true;
                }
            }

            return true;
        }

        return false;
    }

    public bool AttackArea(int vertical, int horizontal)
    {
        //TODO Get access attack only for not attack area
        //false miss
        //true hit
        if (attackedAreasList[Index(horizontal, vertical)])
            throw new ArgumentException("This area has already been attacked");

        attackedAreasList[Index(horizontal, vertical)] = true;
        return fillAreasList[Index(horizontal, vertical)];
    }

    public bool TransformObject(int vertical, int horizontal, int newVertical, int newHorizontal, int lenght, int width)
    {
        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!fillAreasList[Index(horizontal + j, vertical + i)])
                    return false;
            }
        }


        if (!CheckOnFreeArea(newVertical, newHorizontal, lenght, width))
            return false;
        

        //Set values
        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                fillAreasList[Index(newHorizontal + j, newVertical + i)] = true;
            }
        }

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                fillAreasList[Index(horizontal + j, vertical + i)] = false;
            }
        }

        return true;
    }

    public bool CheckOnFreeArea(int row, int column, int lenght, int width)
    {

        if (row + lenght >= Size || column + width >= Size || column < 0 || row < 0)
            return false;
        
        for (int i = 0; i < lenght + 2; i++)
        {
            for (int j = 0; j < width + 2; j++)
            {
                var tRow = row - 1 + i;
                var tColumn = column - 1 + j;
                
                if (tColumn >= 0 && tRow >= 0)
                {   
                    Debug.Log("tRow " + tRow);
                    Debug.Log("tClouwn " + tColumn);
                    Debug.Log(fillAreasList[Index(tRow, tColumn)]);
                    if (fillAreasList[Index(tRow, tColumn)])
                    {
                        return false;
                    }
                }
                    
            }
        }

        return true;
    }
    
    public void CreateField(Player player)
    {
        var obj = Instantiate(FieldPrefab,new Vector3(Offset.x,0,Offset.y),Quaternion.identity);
        obj.AddComponent<OwnerField>().player = player;
        obj.GetComponent<NetworkObject>().Spawn();
        
    }
    
    public void DestroyField()
    {
        Destroy(FieldPrefab);
    }
}

