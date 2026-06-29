using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // 1. REQUIRED FOR SCENE TRACKING

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        OPENDOOR,
        PICKUP,
        DIE,
        EAT,
        GOALREACHED,
        Music_Menu,
        Music_Level1, // 2. Add your level tracks here
        Music_Level2
    }

    [System.Serializable]
    public class Sound
    {
        public SoundType Type;
        public AudioClip Clip;

        [Range(0f, 1f)]
        public float Volume = 1f;

        [HideInInspector]
        public AudioSource Source;
    }

    public bool IsSoundEnabled = true;
    public static AudioManager Instance;
    public Sound[] AllSounds;

    private Dictionary<SoundType, Sound> _soundDictionary = new Dictionary<SoundType, Sound>();
    public AudioSource MusicSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Move dictionary initialization here so it only happens once
            foreach (var s in AllSounds)
            {
                _soundDictionary[s.Type] = s;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 3. LISTEN FOR SCENE CHANGES
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 4. AUTOMATICALLY PLAY MUSIC BASED ON THE SCENE NAME
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeMusic(SoundType.Music_Menu);
    }

    public void Play(SoundType type)
    {
        if (!_soundDictionary.TryGetValue(type, out Sound s))
        {
            Debug.LogWarning($"Sound type {type} not found!");
            return;
        }

        var soundObj = new GameObject($"Sound_{type}");
        var audioSrc = soundObj.AddComponent<AudioSource>();

        audioSrc.clip = s.Clip;
        audioSrc.volume = IsSoundEnabled ? s.Volume : 0f;

        audioSrc.Play();
        Destroy(soundObj, s.Clip.length);
    }

    public void ChangeMusic(SoundType type)
    {
        if (!_soundDictionary.TryGetValue(type, out Sound track))
        {
            Debug.LogWarning($"Music track {type} not found!");
            return;
        }

        if (MusicSource == null)
        {
            var container = new GameObject("SoundTrackObj");
            // Make the music loop object a child of the AudioManager so it stays organized
            container.transform.SetParent(this.transform);
            MusicSource = container.AddComponent<AudioSource>();
            MusicSource.loop = true;
        }

        // Prevent restarting the track if it is already playing
        if (MusicSource.clip == track.Clip && MusicSource.isPlaying)
        {
            return;
        }

        MusicSource.clip = track.Clip;
        MusicSource.volume = IsSoundEnabled ? track.Volume : 0f;
        MusicSource.Play();
    }
}