using System;
using System.Collections.Generic;
namespace FinancialForecasting{
    public interface IForecastStrategy{
        double PredictFutureValue(double initialValue, double growthRate, int years);
    }
    public class RecursiveForecast : IForecastStrategy{
        private Dictionary<int, double> memo = new Dictionary<int, double>();
        public double PredictFutureValue(double initialValue, double growthRate, int years){
            if (years == 0)
                return initialValue;
            if (memo.ContainsKey(years))
                return memo[years];
            double result = PredictFutureValue(initialValue, growthRate, years - 1) * (1 + growthRate);
            memo[years] = result;
            return result;
        }
    }
    public class ForecastContext{
        private static ForecastContext _instance = null;
        private static readonly object _lock = new object();
        private IForecastStrategy _strategy;

        private ForecastContext(IForecastStrategy strategy){
            _strategy = strategy;
        }
        public static ForecastContext GetInstance(IForecastStrategy strategy){
            if (_instance == null){
                lock (_lock){
                    if (_instance == null){
                        _instance = new ForecastContext(strategy);
                    }
                }
            }return _instance;
        }
        public void SetStrategy(IForecastStrategy strategy){
            _strategy = strategy;
        }

        public double GetFutureValue(double initialValue, double growthRate, int years){
            return _strategy.PredictFutureValue(initialValue, growthRate, years);
        }
    }
    class Program{
        static void Main(string[] args){
            double initialInvestment = 2000;
            double growthRate = 0.07;
            int years = 6;
            IForecastStrategy strategy = new RecursiveForecast();
            ForecastContext context = ForecastContext.GetInstance(strategy);
            double forecastedValue = context.GetFutureValue(initialInvestment, growthRate, years);
            Console.WriteLine($"Forecasted value after {years} years: ₹{forecastedValue:F2}");
        }
    }
}