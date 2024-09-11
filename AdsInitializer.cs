using UnityEngine;
using UnityEngine.Advertisements;
 
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    public RewardedAds rewardedAds;

    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    public string _adUnitIdFake = null; // This will remain null for unsupported platforms
 
    void Awake()
    {
        InitializeAds();

        #if UNITY_IOS
        _adUnitIdFake = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitIdFake = _androidAdUnitId;
#endif

    rewardedAds.LoadAd();
    }
 
    public void InitializeAds()
    {
       /* _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;*/
        
        _gameId = _iOSGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }
 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        rewardedAds.LoadAd();
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}