using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class TfIdfScore
    {
        private double _tfScore;
        private double _idfScore;
        private double _finalScore;

        public TfIdfScore()
        {
            this._tfScore = 0;
            this._idfScore = 0;
            this._finalScore = 0;
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
        public double finalScore
        {
            get { return _finalScore; }
            set { _finalScore = value; }
        }
    }
}
