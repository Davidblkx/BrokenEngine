using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokenEngine.Core
{
    /// <summary>
    /// Controls audio playback
    /// Set devices
    /// play/stop files
    /// </summary>
    public interface IAudioPlayer
    {
        /// <summary>
        /// Plays file in memory
        /// </summary>
        /// <returns></returns>
        void Play();
        void Pause();
        void Stop();

        AudioPlayerState State { get; }

        /// <summary>
        /// Get current selected device index
        /// </summary>
        /// <returns></returns>
        int GetPlaybackDevice();

        /// <summary>
        /// Set playback device to use
        /// </summary>
        /// <param name="DeviceIndex">Index of playback device</param>
        /// <returns></returns>
        bool SetPlaybackDevice(int deviceIndex);

        /// <summary>
        /// Get list of avaible playback devices
        /// </summary>
        /// <returns></returns>
        List<(int Index, string Description)> GetPlaybackDevices();

        /// <summary>
        /// Return volume in percentage
        /// </summary>
        /// <returns></returns>
        int GetVolume();
        /// <summary>
        /// Set current playback volume
        /// </summary>
        /// <param name="value">volume value in percentage</param>
        void SetVolume(int value);

        /// <summary>
        /// Set file to be played
        /// </summary>
        /// <param name="filePath"></param>
        void LoadFile(string filePath);

        /// <summary>
        /// Get current file
        /// </summary>
        /// <returns></returns>
        string GetFile();

        /// <summary>
        /// Get current file duration
        /// </summary>
        /// <returns></returns>
        TimeSpan GetDuration();
        /// <summary>
        /// Get playback position
        /// </summary>
        /// <returns></returns>
        TimeSpan GetPosition();
        /// <summary>
        /// Set playback position
        /// </summary>
        void SetPosition(TimeSpan position);

        /// <summary>
        /// Play is called
        /// </summary>
        event Action<IAudioPlayer> OnPlay;
        /// <summary>
        /// Pause is called
        /// </summary>
        event Action<IAudioPlayer> OnPause;
        /// <summary>
        /// Stop is called
        /// </summary>
        event Action<IAudioPlayer> OnStop;
        /// <summary>
        /// Playback ends
        /// </summary>
        event Action<IAudioPlayer> OnEnd;
        /// <summary>
        /// Playback stops
        /// </summary>
        event Action<IAudioPlayer> OnStart;
        /// <summary>
        /// File loaded and ready to play
        /// </summary>
        event Action<IAudioPlayer> OnLoaded;
        /// <summary>
        /// Error loading file or on playback faill
        /// </summary>
        event Action<IAudioPlayer, string> OnError;
    }

    public enum AudioPlayerState
    {
        Unkown = 0,
        Stop = 1,
        Play = 2,
        Pause = 3
    }
}
