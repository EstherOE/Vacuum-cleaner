using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShareButton : MonoBehaviour
{
    public Image screenshot_image;

    private bool isTestScreenShot = false;

    void Update()
    {
        //if( Input.GetMouseButtonDown( 0 ) )
        //   StartCoroutine( TakeScreenshotAndShare() );
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        new NativeShare().AddFile( filePath )
            .SetSubject( "Maliyo Games" ).SetText( "Join the Fun, Get the game from here!" ).SetUrl( "https://play.google.com/store/apps/details?id=com.maliyo.whotking" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

    public static IEnumerator TakeSSAndShare(Image m_Image)
    {
        m_Image.gameObject.SetActive(true);

        m_Image.sprite = Resources.Load<Sprite>("ScreenShots/shot " + Random.Range(0, 8));

        yield return null;

        yield return null;

        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");

        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Object.Destroy(ss);

        m_Image.gameObject.SetActive(false);

        new NativeShare().AddFile(filePath).SetSubject("Maliyo Games").SetText("Join the fun here: https://play.google.com/store/apps/details?id=com.maliyo.whotking").Share();
    }

    public void ShareBtn()
    {
        if(!isTestScreenShot)
        {
            StartCoroutine(TakeSSAndShare(screenshot_image));
        }
        else
        {
            StartCoroutine(TakeScreenshotAndShare());
        }
    }
}
