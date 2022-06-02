namespace Luxor.DOM.Events
{
    public class Event
    {
        // PUBLIC FIELDS
        private string _type;
        private EventTarget? _target;
        private EventTarget? _currentTarget;
        private EventPhase _eventPhase;
        private bool _bubbles;
        private bool _cancelable;
        private bool _defaultPrevented;
        private bool _composed;
        private bool _isTrusted;
        private double _timeStamp;

        private EventTarget? _relatedTarget;
        private List<EventTarget?> _touchTargetList;
        private List<PathObj> _path;

        private bool _stopPropagation;
        private bool _stopImmediatePropagation;
        private bool _canceled;
        private bool _inPassiveListener;
        private bool _initialized;
        private bool _dispatch;

        // PUBLIC PROPERTIES
        public string Type { get => _type; }
        public EventTarget? Target { get => _target; }
        public EventTarget? CurrentTarget { get => _currentTarget; }
        public EventPhase EventPhase { get => _eventPhase; }
        public bool Bubbles { get => _bubbles; }
        public bool Cancelable { get => _cancelable; }
        public bool DefaultPrevented { get => _defaultPrevented; }
        public bool Composed { get => _composed; }
        public bool IsTrusted { get => _isTrusted; }
        public double TimeStamp { get => _timeStamp; }

        // PROTECTED PROPERTIES
        protected EventTarget? RelatedTarget { get => _relatedTarget; set => _relatedTarget = value; }
        protected List<EventTarget?> TouchTargetList { get => _touchTargetList; set => _touchTargetList = value; }
        protected List<PathObj> Path { get => _path; set => _path = value; }
        protected bool StopProp { get => _stopPropagation; set => _stopPropagation = value; }
        protected bool StopImmProp { get => _stopImmediatePropagation; set => _stopImmediatePropagation = value; }
        protected bool Canceled { get => _canceled; set => _canceled = value; }
        protected bool InPassiveListener { get => _inPassiveListener; set => _inPassiveListener = value; }
        protected bool Initialized { get => _initialized; set => _initialized = value; }
        protected bool Dispatch { get => _dispatch; set => _dispatch = value; }


        // CONSTRUCTORS
        public Event(string type) : this(type, false, false) {}
        public Event (string type, EventInit init) : this(type, init.bubbles, init.cancelable) {}

        protected Event(string type, bool bubbles, bool cancelable)
        {
            _initialized = true;
            _stopPropagation = false;
            _stopImmediatePropagation = false;
            _canceled = false;
            _isTrusted = false;
            _target = null;

            _touchTargetList = new List<EventTarget?>();
            _path = new List<PathObj>();

            _type = type;
            _bubbles = bubbles;
            _cancelable = cancelable;
        }

        // PUBLIC METHODS
        public List<EventTarget?> ComposedPath()
        {
            // 1
            List<EventTarget?> composedPath = new List<EventTarget?>();

            // 2
            List<PathObj> path = Path;

            // 3
            if (path.Count == 0) { return composedPath; }

            // 4
            EventTarget? currentTarget = CurrentTarget;

            // 5
            composedPath.Add(currentTarget);

            // 6
            int currentTargetIndex = 0;

            // 7
            int currentTargetHiddenSubtreeLevel = 0;

            // 8
            int index = path.Count - 1;

            // 9
            while (index >= 0)
            {
                // 9.1
                if (path[index].rootOfClosedTree) { currentTargetHiddenSubtreeLevel++; }

                // 9.2
                if (path[index].invocationTarget == currentTarget)
                {
                    currentTargetIndex = index;
                    break;
                }

                // 9.3
                if (path[index].slotInClosedTree) { currentTargetHiddenSubtreeLevel--; }

                // 9.4
                index--;
            }

            // 10
            int currentHiddenLevel = currentTargetHiddenSubtreeLevel;
            int maxHiddenLevel = currentTargetHiddenSubtreeLevel;

            // 11d
            index =  currentTargetIndex - 1;

            // 12
            while (index >= 0)
            {
                //12.1
                if (path[index].rootOfClosedTree) { currentHiddenLevel++; }

                //12.2
                if (currentHiddenLevel <= maxHiddenLevel) { composedPath.Insert(0, path[index].invocationTarget); }

                //12.3
                if (path[index].slotInClosedTree)
                {
                    // 12.3.1
                    currentHiddenLevel--;

                    // 12.3.2
                    if (currentHiddenLevel < maxHiddenLevel) { maxHiddenLevel = currentHiddenLevel; }
                }

                //12.4
                index--;
            }

            // 13
            currentHiddenLevel = maxHiddenLevel = currentTargetHiddenSubtreeLevel;

            // 14
            index = currentTargetIndex + 1;

            // 15
            while (index < path.Count)
            {
                // 15.1
                if (path[index].slotInClosedTree) { currentHiddenLevel++; }

                // 15.2
                if (currentHiddenLevel <= maxHiddenLevel) { composedPath.Add(path[index].invocationTarget); }

                // 15.3
                if (path[index].rootOfClosedTree)
                {
                    // 15.3.1
                    currentHiddenLevel--;

                    // 15.3.2
                    if (currentHiddenLevel < maxHiddenLevel) { maxHiddenLevel = currentHiddenLevel; }
                }

                // 15.4
                index++;
            }

            // 16
            return composedPath;
        }

        public void StopPropagation()
        {
            StopProp = true;
        }

        public void StopImmediatePropagation()
        {
            StopProp = true;
            StopImmProp = true;
        }

        public void PreventDefault()
        {
            SetCanceled(this);
        }

        // HELPER METHODS
        private void SetCanceled(Event e)
        {
            if (e.Cancelable && !e.InPassiveListener)
            {
                Canceled = true;
            }
        }
    }

    public delegate void EventHandler(object? sender, Event e);
    public delegate void OnErrorEventHandler(object? sender, Event e, Exception err);
    public delegate void OnBeforeUnloadEventHandler(object? sender, Event e);

    public struct EventInit
    {
        public bool bubbles = false;
        public bool cancelable = false;
        public bool composed = false;
        public EventInit() {}
    }

    public struct PathObj
    {
        public EventTarget invocationTarget;
        public bool invocationTargetInShadowTree;
        public EventTarget? shadowAdjustedTarget;
        public EventTarget? relatedTarget;
        public List<EventTarget?> touchTargetList;
        public bool rootOfClosedTree;
        public bool slotInClosedTree;
    }

    public enum EventPhase
    {
        None,
        CapturingPhase,
        AtTarget,
        BubblingPhase,
    }
}