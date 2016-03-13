using System;

namespace DataMining.Learning.Exceptions
{
    public class PropertyMissingException : Exception
    {
        public PropertyMissingException(string propertyName) 
            : base(string.Format("Requseted property {0} is missing in SourceEntry", propertyName))
        {
        }
    }
}
