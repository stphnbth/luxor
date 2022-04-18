using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using static Reference.DataTables;

namespace Luxor.DOM
{
    public interface Element : Node 
    {
        protected NamedNodeMap? Attributes { get; }
        protected List<string> ClassList { get; }
        protected string ClassName { get; set; }
        protected string ID { get; set; }
        protected string LocalName { get; }
        public string? NamespaceURI { get; }
        protected string? Prefix { get; }
        protected ShadowRoot? ShadowRoot { get; }
        protected string Slot { get; set; }
        protected string TagName { get; }

        ShadowRoot AttachShadow(ShadowRoot init)
        {
            throw new NotImplementedException();
        }
        
        Element? Closest(string selectors)
        {
            throw new NotImplementedException();
        }
        
        string? GetAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        string? GetAttributeNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        List<String> GetAttributeNames()
        {
            throw new NotImplementedException();
        }
        
        Attr? GetAttributeNode(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        Attr? GetAttributeNodeNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        List<Element> GetElementsByTagName(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        List<Element> GetElementsByTagNameNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        List<Element> GetElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }
        
        bool HasAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        bool HasAttributes()
        {
            throw new NotImplementedException();
        }
        
        bool HasAttributeNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        bool Matches(string selectors)
        {
            throw new NotImplementedException();
        }
        
        void RemoveAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        void RemoveAttributeNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        Attr RemoveAttributeNode(Attr attr)
        {
            throw new NotImplementedException();
        }
        
        void SetAttribute(string qualifiedName, string value)
        {
            throw new NotImplementedException();
        }
        
        void SetAttributeNS(string? nspace, string qualifiedName, string value)
        {
            throw new NotImplementedException();
        }
        
        bool ToggleAttribute(string qualifiedName, bool? force)
        {
            throw new NotImplementedException();
        }
        
        Attr? SetAttributeNode(Attr attr)
        {
            throw new NotImplementedException();
        }
        
        Attr? SetAttributeNodeNS(Attr attr)
        {
            throw new NotImplementedException();
        }
        
    }
}