using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    bool IsFinished { get; set; }
    void Execute(Action OnComplete);
    void Undo();
}
