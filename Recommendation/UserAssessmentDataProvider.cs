using System;
using System.Collections.Generic;
using System.Linq;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;
using DataMining.Learning.DataSources;
using DataMining.Learning.Interfaces;

namespace DataMining.Learning.Recommendation
{
    public class UserAssessmentDataProvider : IInputDataProvider<User,Item>
    {
        private readonly IDataSource _usersSource;
        private readonly IDataSource _itemsSource;
        private readonly IDataSource _assessmentsSource;

        public UserAssessmentDataProvider(IDataSource usersSource, IDataSource itemsSource, IDataSource assessmentsSource)
        {
            if (usersSource == null) throw new ArgumentNullException("usersSource");
            if (itemsSource == null) throw new ArgumentNullException("itemsSource");
            if (assessmentsSource == null) throw new ArgumentNullException("assessmentsSource");

            _usersSource = usersSource;
            _itemsSource = itemsSource;
            _assessmentsSource = assessmentsSource;
        }

        public IRelationSchema<User,Item> GetInputData()
        {
            var users = GetUsers();

            var items = GetItems();

            var assessments = GetAssessments(users, items);

            return new UserAssessmentData(users, items, assessments);
        }

        private IReadOnlyCollection<User> GetUsers()
        {
            return _usersSource.GetSourceEntries()
                               .Select(entry => new User(entry.Property("Name")))
                               .ToList();
        }

        private IReadOnlyCollection<Item> GetItems()
        {
            return _itemsSource.GetSourceEntries()
                               .Select(entry => new Item(entry.Property("Name")))
                               .ToList();
        }

        private IEnumerable<Assessment> GetAssessments(IEnumerable<User> users, IEnumerable<Item> items)
        {
            var usersMap = users.ToDictionary(user => user.Name);
            var itemsMap = items.ToDictionary(item => item.Name);

            return _assessmentsSource.GetSourceEntries()
                                     .Select(entry =>new Assessment(usersMap[entry.Property("UserName")],
                                                                    itemsMap[entry.Property("ItemName")],
                                                                    entry.Property<double>("Rating")))
                                      .ToList();
        }
    }
}
