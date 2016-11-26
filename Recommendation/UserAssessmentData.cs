using System.Collections.Generic;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Recommendation
{
    public class UserAssessmentData : IUserItemRelation
    {
        public UserAssessmentData(IEnumerable<User> users, IEnumerable<Item> items, Junction<User, Item> assessments)
        {
            Primary = users;
            Secondary = items;
            Association = assessments;
        }

        #region IRelationSchema implementation

        public IEnumerable<User> Primary { get; private set; }

        public IEnumerable<Item> Secondary { get; private set; }

        public Junction<User, Item> Association { get; private set; }

        #endregion
    }
}
