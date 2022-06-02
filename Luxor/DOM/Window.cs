using Luxor.DOM.Events;
using EventHandler = Luxor.DOM.Events.EventHandler;

namespace Luxor.DOM
{
    public class Window : EventTarget, IGlobalEventHandlers, IWindowEventHandlers
    {
        // PRIVATE FIELDS
        private WindowProxy _window { get; }
        private WindowProxy _self;
        private Document _document;
        private string _name;
        private Location _location;
        private History _history;
        private CustomElementRegistry _customElements;
        private BarProp _locationbar;
        private BarProp _menubar;
        private BarProp _personalbar;
        private BarProp _scrollbars;
        private BarProp _statusbar;
        private BarProp _toolbar;
        private string _status;
        private bool _closed;
        private WindowProxy _frames;
        private ulong _length;
        private WindowProxy? _top;
        private WindowProxy _opener;
        private WindowProxy? _parent;
        private Element? _frameElement;
        private Navigator _navigator;
        private Navigator _clientInformation;
        private bool _originAgentCluster;

        // PUBLIC METHODS
        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Focus()
        {
            throw new NotImplementedException();
        }

        public void Blur()
        {
            throw new NotImplementedException();
        }

        WindowProxy? Open(string url = "", string target = "_blank", string features = "")
        {
            throw new NotImplementedException();
        }

        public void Alert()
        {
            throw new NotImplementedException();
        }

        public void Alert(string message)
        {
            throw new NotImplementedException();
        }

        public bool Confirm(string message = "")
        {
            throw new NotImplementedException();
        }

        public string? Prompt(string message = "", string def = "")
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void PostMessage(string message, string targetOrigin, List<object> transfer)
        {
            throw new NotImplementedException();
        }

        public void PostMessage(string message, WindowPostMessageOptions options) => PostMessage(message, options.targetOrigin, options.transfer);
    }

    public sealed class WindowProxy
    {
        private readonly Window _window;
        private readonly Func<Window> _get;

        public WindowProxy(Window window)
        {
            _window = window;
            _get = () => _window;
        }
    }

    readonly public struct BarProp
    {
        public bool Visible { get; }
    }

    public struct WindowPostMessageOptions
    {
        public string targetOrigin = "/";
        public List<object> transfer = new List<object>();
        public WindowPostMessageOptions() {}
    }

    public interface IGlobalEventHandlers
    {
        public EventHandler onabort { get; }
        public EventHandler onauxclick { get; }
        public EventHandler onbeforematch { get; }
        public EventHandler onblur { get; }
        public EventHandler oncancel { get; }
        public EventHandler oncanplay { get; }
        public EventHandler oncanplaythrough { get; }
        public EventHandler onchange { get; }
        public EventHandler onclick { get; }
        public EventHandler onclose { get; }
        public EventHandler oncontextlost { get; }
        public EventHandler oncontextmenu { get; }
        public EventHandler oncontextrestored { get; }
        public EventHandler oncuechange { get; }
        public EventHandler ondblclick { get; }
        public EventHandler ondrag { get; }
        public EventHandler ondragend { get; }
        public EventHandler ondragenter { get; }
        public EventHandler ondragleave { get; }
        public EventHandler ondragover { get; }
        public EventHandler ondragstart { get; }
        public EventHandler ondrop { get; }
        public EventHandler ondurationchange { get; }
        public EventHandler onemptied { get; }
        public EventHandler onended { get; }
        public OnErrorEventHandler onerror { get; }
        public EventHandler onfocus { get; }
        public EventHandler onformdata { get; }
        public EventHandler oninput { get; }
        public EventHandler oninvalid { get; }
        public EventHandler onkeydown { get; }
        public EventHandler onkeypress { get; }
        public EventHandler onkeyup { get; }
        public EventHandler onload { get; }
        public EventHandler onloadeddata { get; }
        public EventHandler onloadedmetadata { get; }
        public EventHandler onloadstart { get; }
        public EventHandler onmousedown { get; }
        public EventHandler onmouseenter { get; }
        public EventHandler onmouseleave { get; }
        public EventHandler onmousemove { get; }
        public EventHandler onmouseout { get; }
        public EventHandler onmouseover { get; }
        public EventHandler onmouseup { get; }
        public EventHandler onpause { get; }
        public EventHandler onplay { get; }
        public EventHandler onplaying { get; }
        public EventHandler onprogress { get; }
        public EventHandler onratechange { get; }
        public EventHandler onreset { get; }
        public EventHandler onresize { get; }
        public EventHandler onscroll { get; }
        public EventHandler onsecuritypolicyviolation { get; }
        public EventHandler onseeked { get; }
        public EventHandler onseeking { get; }
        public EventHandler onselect { get; }
        public EventHandler onslotchange { get; }
        public EventHandler onstalled { get; }
        public EventHandler onsubmit { get; }
        public EventHandler onsuspend { get; }
        public EventHandler ontimeupdate { get; }
        public EventHandler ontoggle { get; }
        public EventHandler onvolumechange { get; }
        public EventHandler onwaiting { get; }
        public EventHandler onwebkitanimationend { get; }
        public EventHandler onwebkitanimationiteration { get; }
        public EventHandler onwebkitanimationstart { get; }
        public EventHandler onwebkittransitionend { get; }
        public EventHandler onwheel { get; }
    }

    public interface IWindowEventHandlers
    {
        public EventHandler onafterprint { get; }
        public EventHandler onbeforeprint { get; }
        public OnBeforeUnloadEventHandler onbeforeunload { get; }
        public EventHandler onhashchange { get; }
        public EventHandler onlanguagechange { get; }
        public EventHandler onmessage { get; }
        public EventHandler onmessageerror { get; }
        public EventHandler onoffline { get; }
        public EventHandler ononline { get; }
        public EventHandler onpagehide { get; }
        public EventHandler onpageshow { get; }
        public EventHandler onpopstate { get; }
        public EventHandler onrejectionhandled { get; }
        public EventHandler onstorage { get; }
        public EventHandler onunhandledrejection { get; }
        public EventHandler onunload { get; }
    }
    
    public interface IDocumentAndElementEventHandlers
    {
        public EventHandler oncopy { get; }
        public EventHandler oncut { get; }
        public EventHandler onpaste { get; }
    }
}