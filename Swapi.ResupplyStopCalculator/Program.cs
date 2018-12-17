using System;
using System.IO;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.Json;
using Swapi.Core;
using Swapi.Core.Calculation;
using Swapi.Core.Repository;
using CommandLine;
using System.Collections.Generic;

namespace Swapi.ResupplyStopCalculator
{
    class Program
    {
        private class ArgumentOptions
        {
            [Value(0,MetaName= "TotalDistance", Required = true)]
            public int TotalDistance { get; set; }

            [ Option('u',Required = false)]
            public bool Unknowns { get; set; }
        }

        static void Main(string[] args)
            {
                Parser.Default.ParseArguments<ArgumentOptions>(args)
                    .WithParsed<ArgumentOptions>(opts => Run(opts))
                    .WithNotParsed<ArgumentOptions>((errs) => HandleParseError(errs));               
            }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Usage Swapi.ResupplyStopCalculator <TotalDistance> [-u]");
            Console.Read();
            
        }

        private static void Run(ArgumentOptions opts)
        {
            using (var container = GetContainer(opts))
            {
                var calculationProducer = container.Resolve<CalculationProducer>();

                using (calculationProducer.Subscribe(container.Resolve<IObserver<StopsCalculationResult>>()))
                {
                    calculationProducer.Run();
                }
            }
            Console.WriteLine($"Press any key to Exit ...");
            Console.Read();
        }

        private static IContainer GetContainer(ArgumentOptions opts)
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                var builder = new ContainerBuilder(); 
                builder.RegisterInstance(config).As<IConfiguration>().SingleInstance();
                builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
                builder.Register<ILogger>(c => c.Resolve<ILoggerFactory>().CreateLogger("Default"));
                builder.RegisterInstance(new RangeCalculationStrategyFactory()).As<IRangeCalculationStrategyFactory>().SingleInstance();
                builder.RegisterType<StopsCalculator>().As<IStopsCalculator>();
                builder.RegisterType<StarshipWebApiRepository>().As<IStarshipRepository>();
                builder.Register(c=> new CalculationProducer(
                    c.Resolve<IStarshipRepository>(), c.Resolve<IStopsCalculator>(), c.Resolve<ILogger>(), opts.TotalDistance));
                builder.Register(c => new CalculationConsoleConsumer(opts.Unknowns)).As<IObserver<StopsCalculationResult>>().SingleInstance();
            return builder.Build();
            }

    }
}
