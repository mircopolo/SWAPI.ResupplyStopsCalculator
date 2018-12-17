using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using Swapi.Core.Calculation;
using Swapi.Core.Model;
using Swapi.Core.Repository;

namespace Swapi.Core.UnitTests
{
    public class CalculationProducerTests
    {
        public void Run_WhenValidResult_CallsOnNext()
        {
            var moqLogger= new Mock<ILogger>();
            var moqRepository = new Mock<IStarshipRepository>();
            var moqCalculator = new Mock<IStopsCalculator>();
            var moqObserver = new Mock<IObserver<StopsCalculationResult>>();
            
            var producer = new CalculationProducer(moqRepository.Object, moqCalculator.Object, moqLogger.Object,1);
        }

        public void Run_WhenFinished_CallsOnComplete()
        {
            var moqFactory = new Mock<IStarshipRepository>();
        }

        public void Run_WhenException_CallsOnError()
        {
            var moqFactory = new Mock<IStarshipRepository>();
        }
    }
}
