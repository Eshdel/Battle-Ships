using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public struct Ship : INetworkSerializable, System.IEquatable<Ship>
{
    public uint Lenght;

    public uint Life;

    public uint Width;

    public uint Size => Lenght * Width;
    
    
    public Vector2 Coordinates;
    
    public Ship(int lenght, int width, int life,Vector2 coordinates)
    {
        Lenght = (uint) lenght;
        Width = (uint) width;
        Life = (uint) lenght * (uint) width;
        Coordinates = coordinates;
    }

    public void Rotate()
    {
        (Lenght, Width) = (Width, Lenght);
    }
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            // The complex value type handles its own de-serialization
            // Now de-serialize the non-complex value type properties
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out Lenght);
            reader.ReadValueSafe(out Life);
            reader.ReadValueSafe(out Width);
            reader.ReadValueSafe(out Coordinates);
        }
        else
        {
            // Now serialize the non-complex value type properties
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(Lenght);
            writer.WriteValueSafe(Life);
            writer.WriteValueSafe(Width);
            writer.WriteValueSafe(Coordinates);
        }
    }

    public bool Equals(Ship other)
    {
        return other.Coordinates == Coordinates && other.Lenght == Lenght && other.Width == Width;
    }
}
