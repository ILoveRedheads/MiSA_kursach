using MathNet.Numerics.LinearAlgebra.Solvers;
using System.Collections;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.GoFIterator
{
    public class LocalityAggregate : Aggregate
    {
        private List<Locality> _items = new List<Locality>();

        public override Iterator CreateIterator()
        {
            return new LocalityIterator(this);
        }

        // Gets item count
        public int Count
        {
            get { return _items.Count; }
        }

        // Indexer
        public object this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value as Locality; }
        }

        public void FillItems(List<Locality> list)
        {
            _items = new List<Locality>(list);
        }
    }

    public class BudgetsAggregate : Aggregate
    {
        private List<double> _items = new List<double>();

        public override Iterator CreateIterator()
        {
            return new BudgetsIterator(this);
        }

        // Gets item count
        public int Count
        {
            get { return _items.Count; }
        }

        // Indexer
        public double this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public void FillItems(List<double> list)
        {
            _items = new List<double>(list);
        }
    }
    public class NumberResidantsAggregate : Aggregate
    {
        private List<double> _items = new List<double>();

        public override Iterator CreateIterator()
        {
            return new NumberResidantsIterator(this);
        }

        // Gets item count
        public int Count
        {
            get { return _items.Count; }
        }

        // Indexer
        public double this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public void FillItems(List<double> list)
        {
            _items = new List<double>(list);
        }
    }
}
