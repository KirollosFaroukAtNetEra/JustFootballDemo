using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClubsView : UIView<ClubsModel, ClubsController>
{
    public RectTransform ClubsScrollContent;
    public GameObject ClubPrefab;
    public List<ClubItem> ClubsList;
    public ClubsScroll clubsScroll;

    void OnEnable()
    {
        Events.instance.AddListener<ClubDataUpdated>(OnClubDataUpdated);
    }

    void OnDisable()
    {
        Events.instance.RemoveListener<ClubDataUpdated>(OnClubDataUpdated);
    }

    private void OnClubDataUpdated(ClubDataUpdated e)
    {
        for (int i = 0; i < clubsScroll.ActiveElements.Count; i++)
        {
            clubsScroll.ActiveElements[i].Updatedata();
        }
    }

    public override void RegisterDependency()
    {
        base.RegisterDependency();
        Model.ListenOnPropertyChanged("ClubsList", ClubsListChanged);
        isLoaded = true;
    }
    private void ClubsListChanged()
    {
        Debug.Log("Clubs List Count > " + Model.ClubsList.Length);
        if ( Model.ClubsList.Length > 0 && clubsScroll != null)
        {
            clubsScroll.Initialize(Model.ClubsList.ToList());
            LoadingAnimation.SetActive(false);
        }
        for (int i = 0; i < clubsScroll.ActiveElements.Count; i++)
        {
            HandleClubItemData(clubsScroll.ActiveElements[i]);
        }

    }
    private void HandleClubItemData(ClubItem clubGameObject)
    {
        clubGameObject.clubButton.onClick.RemoveAllListeners();
        clubGameObject.clubButton.onClick.AddListener(() => { OnClubClicked(clubGameObject.Data.id); });
    }
    private void OnClubClicked(string clubID)
    {
        Controller.OnClubItemClicked(clubID);
    }
}
