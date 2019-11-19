public abstract class UIView<M, C> : UIViewBase
    where M : UIModel,new ()
    where C : UIController<M>, new()
{
    public M Model;
    protected C Controller;

    public override void SetupView(object dataObject=null)
    {
        base.SetupView(dataObject);
        Controller = new C();
        Model = Model ?? new M(); 
        RegisterDependency();
        Controller.Setup(Model, dataObject);
        ShowView();
    }

    public override void RegisterDependency()
    {
        base.RegisterDependency();
        Model.ListenOnPropertyChanged(Model.DataLoadedObserverName, DataLoaded);
    }

    public abstract void DataLoaded();
}
