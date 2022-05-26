namespace Luxor.DOM
{
    public interface IChildNode
    {
        // PUBLIC METHODS

        public void Before(IList<Node> nodes)
        {
            throw new NotImplementedException();
        }

        public void After(IList<Node> nodes)
        {
            throw new NotImplementedException();
        }
        
        public void ReplaceWith(IList<Node> nodes)
        {
            throw new NotImplementedException();
        }
        
        public void Remove()
        {
            throw new NotImplementedException();
        }
        
    }
}