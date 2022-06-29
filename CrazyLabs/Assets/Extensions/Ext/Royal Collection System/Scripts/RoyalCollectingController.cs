using DG.Tweening;
using Papae.UnitySDK;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RoyalCollectingController : MonoBehaviour
{

    /// <summary>
    /// RoyalCollectionSystem Package
    /// </summary>



    // Play collecting sound at begining of the animation or at the end
    public RoyalCollectingAnimation.PLAY_SOUND_MODE _playSoundMode;
    public RoyalCollectingAnimation.EXPANSION_MODE _expansionMode = RoyalCollectingAnimation.EXPANSION_MODE.Going_Up;
    // The emission rate in seconds
    public float emissionRate = 0.2f;
    // The tranform component of the item displayer
    public Transform itemDisplayer;
    // The position where to pop the items
    public Transform popPosition;
    // The prefab of the items to instanciate
    public GameObject itemPrefab;

    public TextMeshProUGUI DisplayText;

    [Header("AUDIO")]
    [SerializeField] AudioClip coinCollect;
    [SerializeField] AudioClip chestOpen;
    [SerializeField] AudioClip chestClose;

    // Instance of this class
    [HideInInspector]
    public static RoyalCollectingController _instance;

    // This is a list of instanciated _itemPrefab 
    private List<RoyalCollectingAnimation> _itemList = new List<RoyalCollectingAnimation>();
    // Reference to the AudioSource component
    //private AudioSource _audioSource;
    float pitch = 1;
    int m_quantity = 0;
    public event Action OnCoinEnd = delegate { };
    [HideInInspector] public UnityEvent OnCoinEndEvent;
    void Awake()
    {
        // Setting instance
        _instance = this;
        //_audioSource = GetComponent<AudioSource>();
    }

    // Collect some items with animation
    public void CollectItem(int quantity)
    {
        m_quantity = quantity;
        if (DisplayText != null)
        {
            DisplayText.rectTransform.DOScale(0, 0);
            DisplayText.text = "+" + quantity;
          //  AudioManager.Instance.PlayOneShot(chestOpen);
            DisplayText.rectTransform.DOScale(1f, .4f).SetEase(Ease.OutBounce).SetDelay(2.45f);
        }
    }
    public void _CollectItem()
    {

        StartCoroutine(PopItems(m_quantity));
    }
    // Collect some items with animation at a fixed position
    public void CollectItemAtPosition(int quantity, Vector3 position)
    {
        // Set the position
        popPosition.position = position;
        StartCoroutine(PopItems(quantity));
    }

    // Here we pop all the necessary items
    IEnumerator PopItems(int quantity)
    {
        //if (DisplayText != null)
        //{
        //    DisplayText.rectTransform.DOScale(0, 0);
        //    DisplayText.text = "+" + quantity;
        //    AudioManager.Instance.PlayOneShot(chestOpen);
        //    yield return new WaitForSeconds(1f);
        //    DisplayText.rectTransform.DOScale(1f, .4f).SetEase(Ease.OutBounce);

        //}

        //yield return new WaitForSeconds(2.1f);
        WaitForSeconds delay = new WaitForSeconds(emissionRate);
        int originalQuantity = quantity;
        int currencyAdded = itemPrefab.GetComponent<RoyalCollectingAnimation>().CurrencyAdded;
        quantity /= itemPrefab.GetComponent<RoyalCollectingAnimation>().CurrencyAdded;
       // CurrencyManager.AddCoins(originalQuantity % itemPrefab.GetComponent<RoyalCollectingAnimation>().CurrencyAdded);

        if (quantity > 50)
        {
            int mul = quantity / 50;
            quantity /= mul;
            itemPrefab.GetComponent<RoyalCollectingAnimation>().CurrencyAdded *= mul;
        }
        //quantity /= 10;
        for (int i = 0; i < quantity; i++)
        {
            RoyalCollectingAnimation animation = null;
            if (i < _itemList.Count)
            {
                if (!_itemList[i]._animationRunning)
                {
                    // A free object has been found in pool, so we reuse it
                    animation = _itemList[i];
                }
            }
            if (animation == null)
            {
                // No free object has been found in pool, so we instantiate a new one
                GameObject go = Instantiate(itemPrefab) as GameObject;
                animation = go.GetComponent<RoyalCollectingAnimation>();
                _itemList.Add(animation);
                Destroy(go, 2.2f);//Remove go Object
            }
            if (!itemDisplayer || itemDisplayer == null)
            {
              //  itemDisplayer = FindObjectOfType<CoinDisplay>().transform.parent.parent.GetChild(1);
            }

            // Initialize object
            animation.Initialize(itemDisplayer, popPosition, Vector3.zero, Vector3.one, _playSoundMode, _expansionMode, this);
            // Start animation
            animation.StartAnimation();
            //yield return delay;
        }
        itemDisplayer.DOScale(1, 1.8f);
        DisplayText.rectTransform.DOScale(0, .95f).SetEase(Ease.InBounce);
        yield return new WaitForSeconds(1);
    //    AudioManager.Instance.PlayOneShot(chestClose, Vector2.zero, .5f);
        yield return new WaitForSeconds(1);
        itemPrefab.GetComponent<RoyalCollectingAnimation>().CurrencyAdded = currencyAdded;
        OnCoinEnd?.Invoke();
        OnCoinEndEvent?.Invoke();
    }

    // Play the collecting sound
    public void PlayCollectingSound()
    {
      //  AudioManager.Instance.PlayOneShot(coinCollect, new Vector2(0, 0), 1, pitch);
        pitch += .025f;
        pitch = Mathf.Clamp(pitch, 0, 1.5f);
    }
}
