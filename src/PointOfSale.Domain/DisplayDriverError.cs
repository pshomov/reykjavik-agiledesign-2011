using System;

namespace PointOfSale.Domain
{
    public class DisplayDriverError : Exception
    {
        public DisplayDriverError(Exception exception) : base("Display is broken or something like that", exception)
        {         
        }
    }
}