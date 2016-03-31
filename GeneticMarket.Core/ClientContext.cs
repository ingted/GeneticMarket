using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Trade;
using GeneticMarket.BackEnd;
using GeneticMarket.BackEnd.DataProvider;
using GeneticMarket.Base.Config;
using GeneticMarket.Core;
using GeneticMarket.Common.Interface;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Logic;
using GeneticMarket.Core.Genetic;
using GeneticMarket.Core.Performance;
using GeneticMarket.Core.Logic;

namespace GeneticMarket.Core
{
    public delegate void RegisteredEventHandler(ClientContext sender);

    public class ClientContext : MarshalByRefObject
    {
        //config classes
        public EnvironmentParameters EnvParams = new EnvironmentParameters();
        public CommonConfig CommonConfig = new CommonConfig();

        //trade classes
        public Account DefaultAccount = null;
        public PortfolioManager PortfolioManager = null;

        //repository classes
        public StrategyRepository StrategyRepository = null;
        public IndicatorRepository IndicatorRepository = null;

        //market classes
        public GeneticEnvironment GeneticEnvironment = null;
        public IMarketWatch MarketWatch = null;
        public MarketMeter MarketMeter = null;

        //logic classes
        public PerformanceLogic PerformanceLogic = null;
        public PositionLogic PositionLogic = null;

        //persistence classes
        public PersistenceLogic PersistenceLogic = null;

        public ClientContext()
        {
            DefaultAccount = new Account();
            PortfolioManager = new PortfolioManager();
            GeneticEnvironment = new GeneticEnvironment();
            MarketWatch = new MarketWatch();
            MarketMeter = new MarketMeter();
            PerformanceLogic = new PerformanceLogic();
            PositionLogic = new PositionLogic();
            StrategyRepository = new StrategyRepository();
            IndicatorRepository = new IndicatorRepository();
            PersistenceLogic = new PersistenceLogic();

            PersistenceLogic.Register(this);

            //we need marketWatch.Instruments in strategyRep before connecting to server when loading report
            StrategyRepository.Register(MarketWatch, null);

            //we need this when loading performance
            PerformanceLogic.Register(this);
        }

        public void RegisterAll(List<string> instruments,IStrategyRepositoryHelper srep)
        {
            MarketWatch.Register(instruments, IndicatorRepository);
            StrategyRepository.Register(MarketWatch, srep);

            GeneticEnvironment.Register(this);
            PortfolioManager.Register(this);
            MarketMeter.Register(this);
            PositionLogic.Register(this);
            DefaultAccount.Register(MarketWatch);
        }       
    }
}
