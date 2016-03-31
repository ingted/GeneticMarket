using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;
using GeneticMarket.Common.Trade;

namespace GeneticMarket.Common.Interface
{
    /// <summary>
    /// parameters are added in rule constructor
    /// we suppose that parameters is set-up before calling Start
    /// because in Start, rule uses parameters to add its signals
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// returns true if rule Initialize method is called.
        /// After calling this method, state of the rule cannot change
        /// </summary>
        //bool IsInitialized { get; }

        /// <summary>
        /// Init signals and other parameters
        /// </summary>
        void Initialize(IRuleContainer container);

        void Finalize();

        //void SetPeriods(int smallTf, int defaultTf, int largeTf);

        /// <summary>
        /// we let rules randomize their parameters because there may be some logics for parameters
        /// for example in MACD ma1 period should be less than ma2
        /// </summary>
        void RandomizeState();

        //object IndicatorRepository
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// used when duplicating parent strategy
        /// </summary>
        /// <param name="target"></param>
        void CopyState(IRule target);

        /// <summary>
        /// processed the given tick and returns a signal (long, short or neutral)
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        int Execute(Tick tick);

        /// <summary>
        /// comparison and info methods
        /// </summary>
        /// <returns></returns>
        int GetIdentifier();
        string GetDescription();
    }
}
