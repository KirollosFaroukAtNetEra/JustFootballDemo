using System;

public interface ICommand
{
    bool IsFinished { get; set; }
    void Execute( Action onComplete );
    void Undo();
}
