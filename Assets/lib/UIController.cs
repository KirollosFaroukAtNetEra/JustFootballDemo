public abstract class UIController<M> where M : UIModel
{
    protected M Model;

    public virtual void Setup( M model, object dataObject = null )
    {
        Model = model;
        Model.NotifyOnPropertyChanged();
    }

    protected virtual void Close()
    { }
}