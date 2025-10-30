using UnityEngine;
using System;

public enum SoundType
{
    //Player Movemento 
    WALK,
    Speaker, 
    Door,
    Button,
    Lamellow,Lamellow2,Lamellow3,lamellow4,
    Hint,
    FoundKey,

}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource AudioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
        AudioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        //instance.AudioSource.PlayOneShot(randomClip, volume);

    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {

            soundList[i].name = names[i];

        }


    }
#endif

    [Serializable]
    public struct SoundList
    {
        public AudioClip[] Sounds { get => sounds; }
        [HideInInspector] public string name;
        [SerializeField] private AudioClip[] sounds;



    }
}
