using System;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Recommendation
{
    public class Assessment : JunctionRecord<User, Item>
    {
        public Assessment(User user, Item item, double value) 
            : base(user, item, value)
        {
        }
    }
}