using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTP.DBAccess;
using GMSDK;

namespace QTP.Domain
{
    public abstract class Monitor
    {
        #region MD
        protected TInstrument target { get; set; }
        protected StrategyQTP strategy;

        protected bool initializeing;
        protected List<Bar> barsBuffer;
        protected List<Tick> ticksBuffer;

        public void SetTInstrument(StrategyQTP strategy, TInstrument target)
        {
            this.strategy = strategy;
            this.target = target;

            barsBuffer = new List<Bar>();
            ticksBuffer = new List<Tick>();
        }


        public abstract string PulseHintMessage();
        public abstract void OnPulse();

        public abstract void Initialize();
        public abstract void InitializeBufferData();
        public abstract void OnTick(Tick tick);
        public abstract void OnBar(Bar bar);

        #endregion

        #region Monitor Position and Order

        // Position and Order
        protected Order orderLast;
        protected Position posTrace;
        protected double stopLossPrice;


        public virtual void OnOrderFilled()
        {
            orderLast = null;
        }

        public abstract void OnPosition(Position pos);

        public virtual void SetStopLossPrice()
        {
            if (posTrace.side == 1)
                stopLossPrice = posTrace.price * 0.95;
            else
                stopLossPrice = posTrace.price * 1.05;
        }

        #endregion
    }
}
