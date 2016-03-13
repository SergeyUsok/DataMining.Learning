using System;
using System.Collections.Generic;
using System.IO;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.Interfaces;

namespace DataMining.Learning.DataSources
{
    public class DelimitedFileDataSource : IDataSource
    {
        private readonly string _fileName;
        private readonly string[] _columnSeparator;

        public DelimitedFileDataSource(string fileName, string columnSeparator)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("fileName");
            if (string.IsNullOrWhiteSpace(columnSeparator)) throw new ArgumentNullException("columnSeparator");

            _fileName = fileName;
            _columnSeparator = new[] { columnSeparator };
        }

        public IEnumerable<SourceEntry> GetSourceEntries()
        {
            using (var reader = new StreamReader(OpenStream()))
            {
                var headers = GetHeader(reader);

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var columns = line.Split(_columnSeparator, StringSplitOptions.None);
                    yield return new SourceEntry(headers, columns);
                }
            }
        }

        protected virtual Stream OpenStream()
        {
            return File.OpenRead(_fileName);
        }

        private string[] GetHeader(TextReader reader)
        {
            var header = reader.ReadLine();

            if (header == null)
                throw new InvalidDataException(string.Format("File {0} is empty", _fileName));

            return header.Split(_columnSeparator, StringSplitOptions.None);
        }
    }
}
