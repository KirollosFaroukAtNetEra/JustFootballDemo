using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ICommand
{
    private bool _isFinished;
    public bool IsFinished { get => _isFinished; set => _isFinished = value; }

    public virtual void Execute(Action OnComplete = null)
    {

    }

    public virtual void Undo()
    {
    }
}
