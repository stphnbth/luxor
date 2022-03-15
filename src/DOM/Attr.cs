using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using static Reference.DataTables;

namespace Luxor.DOM
{
    public class Attr : Node 
    {
        public string? Namespace { get; }
        public string? Prefix { get; }
        public Element? OwnerElement { get; }
        public string? Value { get; set; }

        public string QualifiedName => Prefix is null ? Name! : Prefix + ":" + Name!;

        public Attr(string name, string? nspace = null, string? prefix = null, Element? element = null, string value = "")
        {
            if (nspace is not null)
                Namespace = nspace;
            
            if (prefix is not null)
                Prefix = prefix;

            if (element is not null)
                OwnerElement = element;

            if (!value.Equals(""))
                Value = value;
        }

        public override bool Equals(object? obj)
        {         
            if (obj is not null && obj is Attr)
                return Equals((Attr) obj);
            
            return false;
        }

        public bool Equals(Attr otherAttr)
        {
            if (otherAttr is null)
                return false;
 
            return this.Name!.Equals(otherAttr.Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Value);
        }
    }

}