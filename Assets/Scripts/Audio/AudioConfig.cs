using System.Linq;
using UnityEngine;

public enum AudioType : byte
{
    CorrectAnswer,
    IncorrectAnswer,
    GameOver,
    BackgroundMusic,
    NewRecord
}

[System.Serializable]
public class AudioTrack
{
    public AudioType Type;
    public AudioClip Clip;
}

[CreateAssetMenu(fileName = nameof(AudioConfig), menuName = "Create/Create Audio Config", order = 1)]
public class AudioConfig : ScriptableObject
{
    public AudioTrack[] AudioTracks;

    public AudioClip GetClipByType(AudioType type)
    {
        var clip = AudioTracks.FirstOrDefault(a => a.Type == type);

        return clip?.Clip;
    }
}