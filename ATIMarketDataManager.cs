/*
	ATIMarketDataManager.cs
	This class keeps track of market data requested by ATI 
    on a per instrument level

	Copyright (C) 2011 Dexter Haslem

	This file is part of ninjatrader-remote.

    ninjatrader-remote is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    ninjatrader-remote is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with ninjatrader-remote.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dmh.NinjaTraderRemote
{
    class ATIMarketDataManager
    {
        protected class MarketData
        {
            double last, bid, ask;
            DateTime lastUpdate;
            public MarketData(double last, double bid, double ask)
            {
                Set(last, bid, ask);   
            }

            public void Set(double last, double bid, double ask)
            {
                this.last = last;
                this.bid = bid;
                this.ask = ask;
                lastUpdate = DateTime.Now;
            }
        }

        ATIManager atiMgr;

        // instrument name, on/off
        Dictionary<string, bool> subscribedInstruments;

        public ATIMarketDataManager(ATIManager atim)
        {
            atiMgr = atim;
        }

        public ATIMarketDataManager()
        {
            //
        }

        public bool SubscribeMarketData(string instrument)
        {
            if (!subscribedInstruments[instrument])
            {
                return true;
            }
            else return false;
        }

        public bool UnsubscribeMarketData(string instrument)
        {
            if (subscribedInstruments[instrument])
            {
                return true;
            }
            else return false;
        }
    }
}
