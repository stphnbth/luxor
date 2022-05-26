using Luxor.DOM.HTML;

namespace Luxor.DOM.Events
{
    public class TrackEvent : Event
    {
        // PRIVATE FIELDS
        private ITrack? _track;

        // CONSTRUCTOR
        public TrackEvent(string type, EventInit? eventInitDict = null, ITrack? track = null) : base(type, eventInitDict) {}
    }

    public enum TextTrackMode
    {
        Disabled,
        Hidden,
        Showing
    }

    public enum TextTrackKind
    {
        Subtitles,
        Captions,
        Descriptions,
        Chapters,
        Metadata
    }

    public enum CanPlayTypeResult
    {
        None,
        Maybe,
        Probably
    }
}
