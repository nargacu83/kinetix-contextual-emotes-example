using UnityEngine;
using UnityEngine.Events;
using Kinetix;
using Kinetix.UI;
using Kinetix.UI.EmoteWheel;

public class DemoManager : MonoBehaviour
{
    [Header("UI")]
    public DialogueBox DialogueBox;

    [Header("Kinetix")]
    [SerializeField] private string virtualWorldKey;
    [SerializeField] private string userId;
    [SerializeField] private ContextualEmoteSO playerContextsSO;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private Animator other;

    private void Awake()
    {
        KinetixCore.OnInitialized += OnInitialize;
        KinetixCore.Initialize(new KinetixCoreConfiguration()
        {
            VirtualWorldKey = virtualWorldKey,

            // Enable this, if you want the SDK to handle the animator
            PlayAutomaticallyAnimationOnAnimators = true,

            EmoteContexts = playerContextsSO,

            // Enable this, if you want to help us and share general informations of how users use our SDK.
            EnableAnalytics = true,

            // Uncomment this if you want to log the behaviour of the SDK for debug purposes
            //ShowLogs = true
        });

        // Hide dialogue by default
        DialogueBox.gameObject.SetActive(false);
    }

    private void OnInitialize()
    {
        //Initialise the UI package here
        KinetixUIEmoteWheel.Initialize(new KinetixUIEmoteWheelConfiguration()
        {
            // Set the base language
            baseLanguage = SystemLanguage.English,

            // Set the enabled categories
            enabledCategories = new[] {
                EKinetixUICategory.EMOTE_SELECTOR,
                EKinetixUICategory.INVENTORY,
                EKinetixUICategory.CONTEXT
            },
        });

        //Register local player to recieve animation
        KinetixCore.Animation.RegisterLocalPlayerAnimator(myAnimator);

        if (!string.IsNullOrWhiteSpace(userId))
        {
            // Connect to the given account
            KinetixCore.Account.ConnectAccount(userId, OnAccountConnected);
        }
    }

    private void OnAccountConnected()
    {
        KinetixCore.Account.AssociateEmotesToUser("4d59081f-5850-4b7f-ac90-247209490b12");
        KinetixCore.Account.AssociateEmotesToUser("fb081b06-a51a-48a2-ab69-4a184949b264");
        KinetixCore.Account.AssociateEmotesToUser("8873b14b-7732-46f8-a934-90e45594a7bc");
        KinetixCore.Account.AssociateEmotesToUser("601663f9-902a-4bee-b70c-f3263d55cced");

        KinetixCore.Account.AssociateEmotesToUser("d228a057-6409-4560-afd0-19c804b30b84");
        KinetixCore.Account.AssociateEmotesToUser("bd6749e5-ac29-46e4-aae2-bb1496d04cbb");
        KinetixCore.Account.AssociateEmotesToUser("7a6d483e-ebdc-4efd-badb-12a2e210e618");

    }

    /// <summary>
    /// Show or Hide emote wheel
    /// </summary>
    public void ToggleEmoteWheel()
    {
        if (KinetixUI.IsShown)
        {
            KinetixUI.HideAll();
        }
        else
        {
            KinetixUI.Show();
        }
    }
}