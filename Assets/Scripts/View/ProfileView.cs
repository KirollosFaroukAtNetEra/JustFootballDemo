using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileView : UIView<ProfileModel, ProfileController>
{
    public Image ProfileImage;
    public Image ClubIcon;
    public Text ProfileName;
    public Text ClubeName;
    public Text ClubeLeague;
    public Button ChangeNameButton;
    public Button SaveButton;
    public InputField UserNameInputField;
    public Image SettingIcon;
    
    private bool _isMyProfile;
    public bool IsMyProfile
    {
        set
        {
            _isMyProfile = value;
            UpdateProfileSettings();
        }

        get => _isMyProfile;
    }

    private bool _isEditMode;
    public bool IsEditMode
    {
        set
        {
            _isEditMode = value;

            ToggleEditMode();
        }

        get => _isEditMode;
    }

    private void ToggleEditMode()
    {
        UserNameInputField.gameObject.SetActive(_isEditMode);
        ChangeNameButton.gameObject.SetActive(!_isEditMode);
        SettingIcon.gameObject.SetActive( !IsEditMode );
        SaveButton.gameObject.SetActive( _isEditMode );
        if (IsEditMode)
        {
            TouchScreenKeyboard.Open(ProfileName.text);
        }
    }

    private void UpdateProfileSettings()
    {
        ChangeNameButton.interactable = _isMyProfile;
        SettingIcon.gameObject.SetActive(IsMyProfile);
    }

    public override void RegisterDependency()
    {
        base.RegisterDependency();
        Model.ListenOnPropertyChanged("PlayerData", UpdateViewData);
        isLoaded = true;
    }

    private void UpdateViewData()
    {
        if (Model.PlayerData == null)
        {
            return;
        }

        IsMyProfile = Model.IsMyProfile;
        ProfileName.text = Model.PlayerData.username;
        ClubeName.text = Model.PlayerData.club;
        ClubeLeague.text = "";
        if (Model.PlayerData.pictureUrl == "" || Model.PlayerData.clubPictureUrl == "")
        { return; }
        DataManager.Instance.GetSpriteByUrl(Model.PlayerData.pictureUrl, (image) => { if (ProfileImage == null) return; ProfileImage.sprite = image; });
        DataManager.Instance.GetSpriteByUrl(Model.PlayerData.clubPictureUrl, (image) => { if (ClubIcon == null) return;  ClubIcon.sprite = image; });
        LoadingAnimation.SetActive(false);
    }

    public void ChooseNameClicked()
    {
        IsEditMode = !IsEditMode;
    }

    public void UserNameSubmited()
    {
        IsEditMode = !IsEditMode;
        ProfileName.text = UserNameInputField.text;
        Controller.OnChangeNameClicked( UserNameInputField.text );
    }
}
