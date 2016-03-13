using System;

namespace DataMining.Learning.DataObjects.Inputs
{
    public class User
    {
        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentNullException("name");
            
            Name = name;
        }

        public string Name { get; private set; }
    }
}
