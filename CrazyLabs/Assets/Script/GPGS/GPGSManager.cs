using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSManager : MonoBehaviour
{
    // Play Games Configuration
    private PlayGamesClientConfiguration clientConfiguration;

    // UI Info and Status Texts
    public Text status;
    public Text info;

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
            status.text = "Authenticating";

            // Debug Text
            Debug.Log("# Authenticating...");

            if(code == SignInStatus.Success)
            {
                status.text = "Successfully Authenticated!";
                info.text = "Hello " + Social.localUser.userName;

                // Debug Text
                Debug.Log("# Successfully Authenticated!!");
                Debug.Log("--");
                Debug.Log("# Hello " + Social.localUser.userName);
            }
            else 
            {
                status.text = "Failed to Authenticate!";
                info.text = "Failed due to " + code;

                // Debug Text
                Debug.Log("Failed to Authenticate due to " + code);    
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
        status.text = "# Signed Out Successfully";
        info.text = "";

        // Debug Text
        Debug.Log("# Signed Out Successfully");
    }
}

/**
// - v 11
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
// using UnityEngine.SocialPlatforms;

public class GPGSManager : MonoBehaviour
{
    void Start()
    {
        // Auto SignIn When the Game Starts
       SignIntoGPGSv11();
    }

    // The new method to sign in to GPGS 0.11.01 || using enums
    internal void SignIntoGPGSv11()
    {
        //PlayGamesPlatform.Instance.Authenticate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    // Optional SignIn - using Social Platforms || Not Needed
    // internal void SignIntoGPGSv11Method2()
    // {
    //     PlayGamesPlatform.Activate();
    //     Social.localUser.Authenticate(ProcessAuthentication);
    // }

    // For v0.11.01 Handling Authentication
    internal void ProcessAuthentication(SignInStatus status) {
      if (status == SignInStatus.Success) {
        // Continue with Play Games Services

        // Debug Text
        Debug.Log("# Successfully Authenticated!");
      } else {
        // Disable your integration with Play Games Services or show a login button
        // to ask users to sign-in. Clicking it should call
        // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).

        // Debug Text
        Debug.Log("# Failed to Authenticated!!");
      }
    }

    // Manual Sign In Button
    public void ManualSignInBtn()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

}
**/