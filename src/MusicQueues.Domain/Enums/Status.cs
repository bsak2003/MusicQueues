namespace MusicQueues.Domain.Enums
{
    public enum Status
    {
        Created, // Queue's MediaPlayer requires further setup
        Authenticated, // Queue's MediaPlayer can be started, but now it's disconnected and does not imply any platform status
        Stopped, // Queue's MediaPlayer was playing music, but it was stopped (i.e. platform's local queue cleared and playback stopped) and now is disconnected
        OnHold, // Queue's MediaPlayer is temporarily disconnected from the platform, music playback left intact
        Paused, // Queue's MediaPlayer is connected to the platform, but music playback is paused
        Playing // Queue's MediaPlayer is connected and music is playing correctly
    }
}