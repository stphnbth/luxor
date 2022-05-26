using Luxor.DOM.HTML;
using Luxor.DOM.XHR;

namespace Luxor.DOM
{
    // https://html.spec.whatwg.org/multipage/custom-elements.html#elementinternals
    public class ElementInternals : ARIAMixin, IValidityState
    {
        // PRIVATE FIELDS
        private ShadowRoot? _shadowRoot;
        private HTMLFormElement? _form;
        private List<Node> _labels;

        // IValidityState Implementation
        private bool _willValidate;
        private ValidityState _validity;
        private string _validationMessage;

        public bool WillValidate { get => _willValidate; set => _willValidate = value; }
        public ValidityState Validity { get => _validity; set => _validity = value; }
        public string ValidationMessage { get => _validationMessage; set => _validationMessage = value; }

        // PUBLIC PROPERTIES
        public ShadowRoot? ShadowRoot { get => _shadowRoot; }
        public HTMLFormElement? Form { get => _form; }
        public List<Node> Labels { get => _labels; }

        // CONSTRUCTOR

        // PUBLIC METHODS
        public void SetFormValue(FormData? value, FormData? state)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class ARIAMixin
    {
        public string? Role { get; set; }
	    public string? AriaAtomic { get; set; }
	    public string? AriaAutoComplete { get; set; }
	    public string? AriaBusy { get; set; }
	    public string? AriaChecked { get; set; }
	    public string? AriaColCount { get; set; }
	    public string? AriaColIndex { get; set; }
	    public string? AriaColIndexText { get; set; }
	    public string? AriaColSpan { get; set; }
	    public string? AriaCurrent { get; set; }
	    public string? AriaDescription { get; set; }
	    public string? AriaDisabled { get; set; }
	    public string? AriaExpanded { get; set; }
	    public string? AriaHasPopup { get; set; }
	    public string? AriaHidden { get; set; }
	    public string? AriaInvalid { get; set; }
	    public string? AriaKeyShortcuts { get; set; }
	    public string? AriaLabel { get; set; }
	    public string? AriaLevel { get; set; }
	    public string? AriaLive { get; set; }
	    public string? AriaModal { get; set; }
	    public string? AriaMultiLine { get; set; }
	    public string? AriaMultiSelectable { get; set; }
	    public string? AriaOrientation { get; set; }
	    public string? AriaPlaceholder { get; set; }
	    public string? AriaPosInSet { get; set; }
	    public string? AriaPressed { get; set; }
	    public string? AriaReadOnly { get; set; }
	    public string? AriaRequired { get; set; }
	    public string? AriaRoleDescription { get; set; }
	    public string? AriaRowCount { get; set; }
	    public string? AriaRowIndex { get; set; }
	    public string? AriaRowIndexText { get; set; }
	    public string? AriaRowSpan { get; set; }
	    public string? AriaSelected { get; set; }
	    public string? AriaSetSize { get; set; }
	    public string? AriaSort { get; set; }
	    public string? AriaValueMax { get; set; }
	    public string? AriaValueMin { get; set; }
	    public string? AriaValueNow { get; set; }
	    public string? AriaValueText { get; set; }
    }
}