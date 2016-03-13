namespace DataMining.Learning.DataObjects.Inputs
{
    public class JunctionRecord<TFirst, TSecond>
    {
        public JunctionRecord(TFirst first, TSecond second, double value)
        {
            First = first;
            Second = second;
            Value = value;
        }

        public TFirst First { get; private set; }

        public TSecond Second { get; private set; }

        public double Value { get; private set; }
    }
}