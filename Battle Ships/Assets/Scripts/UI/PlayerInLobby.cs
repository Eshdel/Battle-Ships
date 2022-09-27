using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Netcode;
public struct PlayerInLobby:INetworkSerializable, System.IEquatable<PlayerInLobby>
{
    public int Id;
    
    public FixedString32Bytes Name;
                                      
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out Name); 
            reader.ReadValueSafe(out Id); 
        }
        
        else {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(Name); 
            writer.WriteValueSafe(Id); 
        } 
    }

    public bool Equals(PlayerInLobby other)
    {
        return Name == other.Name && Id == other.Id;
    }

    public PlayerInLobby(FixedString32Bytes Name,int id)
    {
        this.Name = Name;
        Id = id;
    }

}
