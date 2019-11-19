using System;

public abstract class Command : ICommand
{
    public bool IsFinished { get; set; }

    public virtual void Execute( Action onComplete = null )
    { }

    public virtual void Undo()
    { }
}
