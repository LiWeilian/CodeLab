using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFDemo.WinSvc
{
    // Implement the ICalculator service contract in a service class.
    public class CalculatorService : ICalculator
    {
        // Implement the ICalculator methods.
        public double Add(CalcParams p)
        {
            double result = p.N1 + p.N2;
            return result;
        }

        public double Subtract(CalcParams p)
        {
            double result = p.N1 - p.N2;
            return result;
        }

        public double Multiply(CalcParams p)
        {
            double result = p.N1 * p.N2;
            return result;
        }

        public double Divide(CalcParams p)
        {
            double result = p.N1 / p.N2;
            return result;
        }
        public string JSONData(string id)
        {
            return Data(id);
        }

        private string Data(string id)
        {
            // logic
            return "Data: " + id;
        }
    }
}
