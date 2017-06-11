using System;
using BrokenEngine.Core;
using ManagedBass;
using System.Collections.Generic;

namespace BrokenEngine.BassAudioPlayer
{
    public class BassPlayer : IAudioPlayer
    {
        public event Action<IAudioPlayer> OnPlay;
        public event Action<IAudioPlayer> OnPause;
        public event Action<IAudioPlayer> OnStop;
        public event Action<IAudioPlayer> OnEnd;
        public event Action<IAudioPlayer> OnStart;
        public event Action<IAudioPlayer> OnLoaded;
        public event Action<IAudioPlayer, string> OnError;

        private const string KEY_CURRENT_SONG = "BassAudioPlayer.CurrentSong";

        private readonly MediaPlayer _mediaPlayer;
        private readonly ISettings _settings;

        public AudioPlayerState State
        {
            get
            {
                switch(_mediaPlayer.State)
                {
                    case PlaybackState.Paused: return AudioPlayerState.Pause;
                    case PlaybackState.Playing: return AudioPlayerState.Play;
                    case PlaybackState.Stopped: return AudioPlayerState.Stop;
                }

                return AudioPlayerState.Unkown;
            }
        }

        public BassPlayer(ISettings settingsProvider)
        {
            _mediaPlayer = new MediaPlayer();
            _settings = settingsProvider;

            _mediaPlayer.MediaLoaded += RaiseOnMediaLoad;
            _mediaPlayer.MediaFailed += RaiseOnError;
            _mediaPlayer.MediaEnded += RaiseOnEnded;
        }

        public TimeSpan GetDuration()
        {
            return _mediaPlayer.Duration;
        }

        public string GetFile()
        {
            return _settings.GetValue(KEY_CURRENT_SONG);
        }

        public int GetPlaybackDevice()
        {
            return _mediaPlayer.Device;
        }

        public List<(int Index, string Description)> GetPlaybackDevices()
        {
            var devices = new List<(int, string)>
            {
                (0, "No Device (MUTE)")
            };

            for (var i = 1; i < Bass.DeviceCount; i++)
            {
                if (!Bass.GetDeviceInfo(i, out var info)) continue;
                devices.Add((i, info.Name));
            }

            return devices;
        }

        public TimeSpan GetPosition()
        {
            return _mediaPlayer.Position;
        }

        public int GetVolume()
        {
            return Convert.ToInt32(Math.Round(_mediaPlayer.Volume * 100, 0));
        }

        public void LoadFile(string filePath)
        {
            _settings.SetValue(KEY_CURRENT_SONG, filePath ?? "");
            _mediaPlayer.LoadAsync(filePath);
        }

        public void Pause()
        {
            if(_mediaPlayer.Pause()) OnPause?.Invoke(this);
        }

        public void Play()
        {
            if (_mediaPlayer.Play())
            {
                if (_mediaPlayer.Position.TotalSeconds < 1) OnStart?.Invoke(this);
                OnPlay?.Invoke(this);
            }
        }

        public bool SetPlaybackDevice(int deviceIndex)
        {
            if(deviceIndex >= 0 && deviceIndex < Bass.DeviceCount)
            {
                _mediaPlayer.Device = deviceIndex;
                return true;
            }

            return false;
        }

        public void SetPosition(TimeSpan position)
        {
            _mediaPlayer.Position = position;
        }

        public void SetVolume(int value)
        {
            if (value < 0) value = 0;
            if (value > 100) value = 100;

            _mediaPlayer.Volume = value / 100.0;
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
            OnStop?.Invoke(this);
        }

        private void RaiseOnMediaLoad(int state)
        {
            OnLoaded?.Invoke(this);
        }

        private void RaiseOnError(object sender, EventArgs e)
        {
            OnError?.Invoke(this, e.ToString());
        }

        private void RaiseOnEnded(object sender, EventArgs e)
        {
            OnEnd?.Invoke(this);
        }
    }
}
