using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SystemDisabler : MonoBehaviour
{
    public bool disableSpawnerSystem = false;
    void Start()
    {
        if(disableSpawnerSystem){
        disable();
        }
    }
    private void disable()
    {
        World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<SpawnerSystem>().Enabled = false;
    }
}
