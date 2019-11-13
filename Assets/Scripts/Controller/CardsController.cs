using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsController : UIController<CardsModel>
{
    public override void Setup(CardsModel model, object dataObject)
    {
        base.Setup(model);
        Model.RequestCards();
    }
    public void OnCardItemClicked(string cardID)
    {
        Debug.Log("CardClicked " + cardID);
       
        ViewsManager.Instance.OpenView( ViewType.ProfileView , cardID);
    }
    public override void RetryLoadData()
    {
        Model.RequestCards();
    }

}
