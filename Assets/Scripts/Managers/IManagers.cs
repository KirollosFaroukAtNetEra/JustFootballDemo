using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManagers
{
    bool IsReady { get; set; }
    void Initialize();
}
