using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Swapi.Core.Calculation;
using Swapi.Core.Model;
using Swapi.Core.Repository;

namespace Swapi.Core
{
    /// <summary>
    /// Responsible for running calculation on all Starships an notifying consumer.
    /// </summary>
    public class CalculationProducer : IObservable<StopsCalculationResult>
    {
        private ILogger _logger;
        public int TotalDistance { get; private set; }

        private BlockingCollection<Starship> _collection = new BlockingCollection<Starship>();

        private IList<IObserver<StopsCalculationResult>> _observers = new List<IObserver<StopsCalculationResult>>();

        public CalculationProducer(IStarshipRepository repository, IStopsCalculator calculator, ILogger logger, int totalDistance)
        {
            TotalDistance = totalDistance;
            Repository = repository;
            Calculator = calculator;
            _logger = logger;
        }
        public IStarshipRepository Repository { get; private set; }

        public IStopsCalculator Calculator { get; private set; }

        public IDisposable Subscribe(IObserver<StopsCalculationResult> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new DisposableSubscription<StopsCalculationResult>(_observers, observer);
        }

        /// <summary>
        /// Start the calculation execution.
        /// </summary>
        public void Run()
        {
            try
            {
                var notifyTask = Task.Run(() => Notify());
                foreach (var starship in Repository.GetAllStarships())
                {
                    _collection.Add(starship);
                }

                _collection.CompleteAdding();
                Task.WaitAll(notifyTask);

                foreach (var observer in _observers)
                {
                    observer.OnCompleted();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal error loading starships");
            }

        }
        // Background task to notify observers of results
        private Task Notify()
        {
            try
            {
                foreach (var starship in _collection.GetConsumingEnumerable())
                {
                    StopsCalculationResult result = null;
                    try
                    {
                        result = Calculator.CalculateTotalStops(starship, TotalDistance);
                    }
                    catch (CalculationException ex)
                    {
                        //Notify error and skip to next ship
                        foreach (var observer in _observers)
                        {
                            observer.OnError(ex);
                        }
                        continue;
                    }

                    foreach (var observer in _observers)
                    {
                        observer.OnNext(result);
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal error loading starships");
            }
            
            return Task.FromResult(true);
        }
    }
}
