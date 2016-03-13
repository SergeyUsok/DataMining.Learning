using System;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Recommendation
{
    public class Assessment : JunctionRecord<User, Item>
    {
        public Assessment(User user, Item item, double value)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (item == null) throw new ArgumentNullException("item");

            First = user;
            Second = item;
            Value = value;
        }

        public User First { get; private set; }

        public Item Second { get; private set; }

        public double Value { get; private set; }
    }
}