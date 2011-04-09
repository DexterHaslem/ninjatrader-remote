/*
	ATIParameterEnums.cs
    Enumerations for command, order types etc. 
    This is a waste of effort seeing how it's already available in a NinjaTrader namespace!

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
    public enum ActionType
    {
        Buy,
        Sell
    };

    public enum TimeInForce
    {
        Day,
        GTC
    };

    public enum OrderType
    {
        Market,
        Limit,
        Stop,
        StopLimit
    };

    public enum CommandType
    {
        Cancel,
        CancelAllOrders,
        Change,
        ClosePosition,
        CloseStrategy,
        FlattenEverything,
        Place,
        ReversePosition
    };

    public enum MarketPosition
    {
        Flat = 0,
        Long = 1,
        Short = - 1
    };
}
