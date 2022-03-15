using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using static Reference.DataTables;

namespace Luxor.DOM
{
    public class Element : Node 
    {
        private NamedNodeMap _attributes = new NamedNodeMap();
        private string _customState = "undefined";
        
        public string? Namespace { get; }
        public string? Prefix { get; }

        public string LocalName { get => Name!; }
        public string TagName { get => QualifiedName.ToUpper(); } // TODO: NAMESPACES...
        public string QualifiedName => Prefix is null ? Name! : Prefix + ":" + Name!;
 
        public string ID { get => _attributes["id"]; set => _attributes.Add("id", value); }
        public string ClassName { get => _attributes["class"]; set => _attributes.Add("class", value); }
        
        public string[] ClassList 
        {
            get => _attributes["class"].Split(' ');
            set => _attributes["class"] = _attributes["class"] + " " + value;
        } 

        public string Slot { get => _attributes["slot"]; set => _attributes["slot"] = value; }

        public ShadowRoot? ShadowRoot { get; } 

        public bool HasAttributes()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAttributeNames()
        {
            throw new NotImplementedException();
        }
        
        public string GetAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(string nspace, string name)
        {
            throw new NotImplementedException();
        }

        public void SetAttribute(string qualifiedName, string value)
        {
            throw new NotImplementedException();
        }

        public void SetAttribute(string nSpace, string qualifiedName, string value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public void RemoveAttribute(string nSpace, string name)
        {
            throw new NotImplementedException();
        }

        public bool ToggleAttribute(string qualifiedName, bool force = false)
        {
            throw new NotImplementedException();
        }

        public bool HasAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public bool HasAttribute(string nSpace, string name)
        {
            throw new NotImplementedException();
        }

        public Attr GetAttributeNode(string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public Attr GetAttributeNode(string nSpace, string name)
        {
            throw new NotImplementedException();
        }

        public Attr SetAttributeNode(Attr attribute)
        {
            throw new NotImplementedException();
        }

        public Attr RemoveAttributeNode(Attr attribute)
        {
            throw new NotImplementedException();
        }

        public ShadowRoot AttachShadow(ShadowRootMode mode, 
                SlotAssignmentMode slotAssignemnt = SlotAssignmentMode.Named)
        {
            throw new NotImplementedException();
        }

        public Element? Closest(string selectors)
        {
            throw new NotImplementedException();
        }

        public bool Matches(string selectors)
        {
            throw new NotImplementedException();
        }

        public List<Element> GetElementsByTagName(string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public List<Element> GetElementsByTagName(string nSpace, string name)
        {
            throw new NotImplementedException();
        }

        public List<Element> GetElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }

        Element() 
        {
            throw new NotImplementedException();
        }
    }
}