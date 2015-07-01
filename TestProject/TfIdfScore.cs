using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class TfIdfScore
    {
        private double _tfScore;
        private double _idfScore;
        private double _cosineScore;

        public Dictionary<String, double> tfIdfVector;

        public TfIdfScore()
        {
            this._tfScore = 0;
            this._idfScore = 0;
            this._cosineScore = 0;
            this.tfIdfVector = new Dictionary<String, double>();
        }


        public double tfScore
        {
            get { return _tfScore; }
            set { _tfScore = value; }
        }
        public double idfScore
        {
            get { return _idfScore; }
            set { _idfScore = value; }
        }

        public double cosineScore
        {
            get { return _cosineScore; }
            set { _cosineScore = value; }
        }

    }
}
