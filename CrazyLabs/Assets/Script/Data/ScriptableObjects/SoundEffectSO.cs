using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
//using Utils;
using Random = UnityEngine.Random;



[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
public class SoundEffectSO : ScriptableObject
{
    #region config
    [BoxGroup("config/Audios")]
    public AudioClip clip;
    [BoxGroup("config/Audios")]
    public AudioClip[] clips;

    public bool isLoop;

    [MinMaxSlider(0.0f, 1.0f)]
    [BoxGroup("config/controls")]
    public Vector2 volume = new Vector2(0.5f, 0.5f);

    [BoxGroup("config/controls")]
    [MinMaxSlider(0, 4)]
    public Vector2 pitch = new Vector2(1, 1);
    [Space]
    [BoxGroup("config")]
    [SerializeField] private SoundClipPlayOrder playOrder;
    [BoxGroup("config")]
    [SerializeField] private AudioMixerGroup audioMixer;

    //  [DisplayAsString] [BoxGroup("config")] [SerializeField]
    [SerializeField, ReadOnly] private int playIndex = 0;
    Timer playIndexResetTimer;
    #endregion

    #region PreviewCode

#if UNITY_EDITOR
    private AudioSource previewer;

    private void OnEnable()
    {
        previewer = EditorUtility
            .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
                typeof(AudioSource))
            .GetComponent<AudioSource>();
           
    }

    private void OnDisable()
    {
        DestroyImmediate(previewer.gameObject);
    }

    [Button("Play")]
    private void PlayPreview()
    {
        Play(previewer);
    }


    [Button("Stop")]
    private void StopPreview()
    {
        previewer.Stop();
    }
#endif
    #endregion

    public AudioSource Play(AudioSource audioSourceParam = null)
    {
      //  if(isLoop)
        //{
          // clip.AudioSource.loop = true;
        //}
        
        if (clip == null && clips.Length == 0)
        {
            //  this.LogWarning($"Missing sound clips for {name}");
            return null;
        }

        if (playIndexResetTimer != null)
        {
            playIndexResetTimer.Cancel();
        }
       
        var source = audioSourceParam;
        if (source == null)
        {
            var _obj = new GameObject("Sound", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        if (audioMixer != null)
            source.outputAudioMixerGroup = audioMixer;
        // set source config:
        source.clip = clips.Length > 0 ? GetAudioClip() : clip;
        source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);
        source.loop = isLoop;

        source.Play();

#if UNITY_EDITOR
        if (source != previewer)
        {
            Destroy(source.gameObject, source.clip.length / source.pitch);
        }
#else
                Destroy(source.gameObject, source.clip.length / source.pitch);
#endif
        return source;
    }
     public AudioSource Stop(AudioSource audioSourceParam = null)
    {
      //  if(isLoop)
        //{
          // clip.AudioSource.loop = true;
        //}
        
        if (clip == null && clips.Length == 0)
        {
            //  this.LogWarning($"Missing sound clips for {name}");
            return null;
        }

      //  if (playIndexResetTimer != null)
        //{
          //  playIndexResetTimer.Cancel();
       // }
       
        var source = audioSourceParam;
        if (source == null)
        {
            var _obj = new GameObject("Sound", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        if (audioMixer != null)
            source.outputAudioMixerGroup = audioMixer;
        /*// set source config:
        source.clip = clips.Length > 0 ? GetAudioClip() : clip;
        source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);
        source.loop = isLoop;
*/
        source.Stop();

#if UNITY_EDITOR
        if (source != previewer)
        {
            Destroy(source.gameObject, source.clip.length / source.pitch);
        }
#else
                Destroy(source.gameObject, source.clip.length / source.pitch);
#endif
        return source;
    }

    private AudioClip GetAudioClip()
    {
        // get current clip
        var clip = clips[playIndex >= clips.Length ? 0 : playIndex];

        // find next clip
        switch (playOrder)
        {
            case SoundClipPlayOrder.in_order:
                playIndex = (playIndex + 1) % clips.Length;
                if (Application.isPlaying)
                    playIndexResetTimer = Timer.Register(8f, () => { playIndex = 0; }, useRealTime: true);
                break;
            case SoundClipPlayOrder.random:
                playIndex = Random.Range(0, clips.Length);
                break;
            case SoundClipPlayOrder.reverse:
                playIndex = (playIndex + clips.Length - 1) % clips.Length;
                break;
        }

        // return clip
        return clip;
    }
    public void PlayClip(AudioClip _clip)
    {
        clip = _clip;
        Play(null);
    }

    public void PlaySO()
    {
        Play(null);
    }
   
    enum SoundClipPlayOrder
    {
        random,
        in_order,
        reverse
    }
}
