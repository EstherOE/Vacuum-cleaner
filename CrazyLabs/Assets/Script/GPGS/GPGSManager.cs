using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSManager : MonoBehaviour
{
    // Play Games Configuration
    private PlayGamesClientConfiguration clientConfiguration;

    void Start()
    {
        ConfigureGPGS();
        SignIntoGPGS(SignInInteractivity.CanPromptOnce, clientConfiguration);
    }

    internal void ConfigureGPGS()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
    }

    internal void SignIntoGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) => 
        {
            // Debug Text
            Debug.Log("<color=green> Play Games Authenticating...</color>");

            if(code == SignInStatus.Success)
            {
                // Debug Text
                Debug.Log("<color=green> Successfully Authenticated!! </color>");
                Debug.Log("--");
                Debug.Log("<color=blue> Hello " + Social.localUser.userName + "</color>");
            }
            else 
            {
                // Debug Text
                Debug.Log("<color=red> Failed to Authenticate due to " + code + "</color>");    
            }
        });
    }

    // Sign In Button
    public void BasicSignInBtn()
    {
        SignIntoGPGS(SignInInteractivity.CanPromptAlways, clientConfiguration);
    }

    // Sign Out Button
    public void SignOutBtn()
    {
        PlayGamesPlatform.Instance.SignOut();

        // Debug Text
        Debug.Log("<color=green> Signed Out Successfully </color>");
    }
}