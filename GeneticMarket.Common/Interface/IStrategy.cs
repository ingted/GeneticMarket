//using System;
//using System.Collections.Generic;
//using System.Text;
//using GeneticMarket.Common.Interface;
//using GeneticMarket.Common.Market;

//namespace GeneticMarket.Common.Interface
//{
//    public interface Strategy
//    {
//        object WeightInfo { get; set; }
//        object PerformanceInfo { get; set; }

//        int RuleCount { get; }
//        int DefaultPeriod { get; }
//        void AddRule(IRule rule);
//        IRule CopyRule(int ruleIndex, Strategy target);
//        string GetDescription();

//        void Initialize(List<Instrument> availableInstruments);

//        /// <summary>
//        /// called when strategy is being removed from main repository
//        /// </summary>
//        void Finalize();
//        void Process(Tick tick);
//        ICollection<Instrument> GetSignalInstruments();
//        int GetSignal(Instrument inst);

//        Strategy Duplicate();
//        void Randomize(double ruleRatio);
//        void CopyInfo(Strategy newStrategy);
//    }
//}
