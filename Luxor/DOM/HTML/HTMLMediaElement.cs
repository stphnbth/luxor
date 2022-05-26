using Luxor.DOM.Events;

namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/media.html#htmlmediaelement
    public class HTMLMediaElement : HTMLElement
    {
        // PRIVATE FIELDS

        // ERROR STATE
        private MediaError? _error;

        // NETWORK STATE
        private string _src;
        // private MediaProvider? _srcObject;
        private string _currentSrc;
        private string? _crossOrigin;
        private ushort _networkState;
        private string _preload;
        private TimeRanges buffered;

        // READY STATE
        private ushort _readyState;
        private bool _seeking;

        // PLAYBACK STATE
        private double _currentTime;
        private double _duration;
        private bool _paused;
        private double _defaultPlaybackRate;
        private double _playbackRate;
        private bool _preservesPitch;
        private TimeRanges _played;
        private TimeRanges _seekable;
        private bool _ended;
        private bool _autoplay;
        private bool _loop;

        // CONTROLS
        private bool _controls;
        private double _volume;
        private bool _muted;
        private bool _defaultMuted;

        // TRACKS
        private AudioTrackList _audioTracks;
        private VideoTrackList _videoTracks;
        private TextTrackList _textTracks;

        // PUBLIC PROPERTIES
        // TODO: HTMLMediaElement

        // CONSTRUCTOR
        public HTMLMediaElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public void load()
        {
            throw new NotImplementedException();
        }

        public CanPlayTypeResult canPlayType(string type)
        {
            throw new NotImplementedException();
        }

        public void FastSeek(double time)
        {
            throw new NotImplementedException();
        }

        public object GetStartDate()
        {
            throw new NotImplementedException();
        }

        public Task Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public TextTrack AddTextTrack(TextTrackKind kind, string label = "", string language = "")
        {
            throw new NotImplementedException();
        }
    }

    // https://html.spec.whatwg.org/multipage/media.html#mediaerror
    public class MediaError
    {
        // PRIVATE FIELDS
        private ushort _code;
        private string _message;
    }

    public interface ITrack
    {
        // PRIVATE FIELDS
        public string Id { get; }
        public string Kind { get; }
        public string Label { get; }
        public string Language { get; }
    }

    public class AudioTrack : ITrack
    {
        // PRIVATE FIELDS
        private string _id;
        private string _kind;
        private string _label;
        private string _language;
        private bool _enabled;

        // PUBLIC PROPERTIES
        public string Id { get => _id; }
        public string Kind { get => _kind; }
        public string Label { get => _label; }
        public string Language { get => _language; }
    }

    public class VideoTrack : ITrack
    {
        // PRIVATE FIELDS
        private string _id;
        private string _kind;
        private string _label;
        private string _language;
        private bool _selected;

        // PUBLIC PROPERTIES
        public string Id { get => _id; }
        public string Kind { get => _kind; }
        public string Label { get => _label; }
        public string Language { get => _language; }
    }

    public class TextTrack : EventTarget, ITrack
    {
        // PRIVATE FIELDS
        private string _id;
        private string _kind;
        private string _label;
        private string _language;
        private string _inBandMetadataTrackDispatchType;
        private TextTrackMode _mode;
        private TextTrackCueList? _cues;
        private TextTrackCueList? activeCues;
        private EventHandler _oncuechange;

        // PUBLIC PROPERTIES
        public string Id { get => _id; }
        public string Kind { get => _kind; }
        public string Label { get => _label; }
        public string Language { get => _language; }

        // PUBLIC METHODS
        public void addCue(TextTrackCue cue)
        {
            throw new NotImplementedException();
        }

        public void removeCue(TextTrackCue cue)
        {
            throw new NotImplementedException();
        }
    }

    // https://html.spec.whatwg.org/multipage/media.html#audiotracklist
    public class AudioTrackList : EventTarget
    {
        // PRIVATE FIELDS
        private ulong _length;
        private EventHandler _onchange;
        private EventHandler _onaddtrack;
        private EventHandler _onremovetrack;

        // PUBLIC METHODS
        public AudioTrack GetTrackById(string id)
        {
            throw new NotImplementedException();
        }
    }

    public class VideoTrackList : EventTarget
    {
        // PRIVATE FIELDS
        private ulong _length;
        private ulong _selectedIndex;
        private EventHandler _onchange;
        private EventHandler _onaddtrack;
        private EventHandler _onremovetrack;

        // PUBLIC METHODS
        public VideoTrackList GetTrackById(string id)
        {
            throw new NotImplementedException();
        }
    }

    public class TextTrackList : EventTarget
    {
        // PRIVATE FIELDS
        private ulong _length;
        private EventHandler _onchange;
        private EventHandler _onaddtrack;
        private EventHandler _onremovetrack;

        // PUBLIC METHODS
        public TextTrack? GetTrackById(string id)
        {
            throw new NotImplementedException();
        }
    }

    public class TextTrackCueList
    {
        // PRIVATE FIELDS
        private ulong _length;

        // PUBLIC METHODS
        public TextTrackCueList GetCueById(string id)
        {
            throw new NotImplementedException();
        }
    }

    public class TextTrackCue : EventTarget
    {
        // PRIVATE FIELDS
        private TextTrack? _track;
        private string _id;
        private double _startTime;
        private double _endTime;
        private bool _pauseOnExit;
        private EventHandler _onenter;
        private EventHandler _onexit;
    }

    public class TimeRanges
    {
        // PRIVATE FIELDS
        private ulong _length;

        // PUBLIC METHODS
        public double Start(ulong index)
        {
            throw new NotImplementedException();
        }

        public double End(ulong index)
        {
            throw new NotImplementedException();
        }
    }
}

