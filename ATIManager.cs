/*
	ATIManager.cs
	The guts of the class that communicates w/ NTs ATI server.
    wraps functionality of the NinjaTrader.Client.Client class

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

using NinjaTrader.Client;

namespace dmh.NinjaTraderRemote
{

    // this code sucks bad, I need to rewrite it when I know what I'm doing
    class ATIManager
    {
        Client ATIClient;
        bool isConnected;
        DateTime connectionStartTime, connectionEndTime;
        ATIAccount account;
        public string AccountName { get; set; }
        private string hostName;
        private int port;

        
        public string ConnectionString
        {
            set
            {
                string[] chunks = value.Split(':');
                if (chunks.Length == 2)
                {
                    hostName = chunks[0];
                    try
                    {
                        Int32.TryParse(chunks[1], out port);
                    }
                    catch (FormatException ex)
                    {
                        port = 36973;
                    }
                }
            }
            get
            {
                return String.Format("{0}:{1}", hostName, port);
            }
        }

        public ATIManager(string hostname, int port) : this()
        {
            hostName = hostname;
            this.port = port;
            // isConnected = ATIClient.SetUp(hostname, port) == 0 ? true : false;
            //if (isConnected)
            //    connectionStartTime = DateTime.Now;
        }



        public ATIManager()
        {
            ATIClient = new Client();
            account = new ATIAccount();
            AccountName = ""; // SIM101!

            // not needed
            hostName = "127.0.0.1";
            port = 36973;
        }

        public bool Connect()
        {
            // SetUp is only required if non defaults are to be used (very likely in our case)
            if (hostName != "127.0.0.1" && port != 36973)
            {
                isConnected = ATIClient.SetUp(hostName, port) == 0 ? true : false;

                if (isConnected)
                {
                    connectionStartTime = DateTime.Now;
                    return true;
                }
                else return false;
            }
            else return true;
        }

        public bool IsConnected
        {
            get
            {
                return ATIClient.Connected(0) == 0 ? true : false;
            }
        }

        public bool Disconnect()
        {
            connectionEndTime = DateTime.Now;
            return ATIClient.TearDown() == 0 ? true : false;
        }

        //TODO: 
        public bool SendCommandString(string command, string account, string instrument, string action, int quantity, 
                                      string orderType, double limitPrice, double stopPrice, string timeInForce, string ocoID, 
                                      string orderID, string strategyName, string strategyID)
        {
            return true;
        }

        //TODO: 
        public bool SendCommand(CommandType command, string account, string instrument, ActionType action, int quantity,
                                OrderType orderType, double limitPrice, double stopPrice, TimeInForce TIF, string ocoID,
                                string orderID, string strategyName, string strategyID)
        {
            return true;
        }


        public ATIAccount ReadAccountInformation(string accountName)
        {
            ATIAccount tempAcct = new ATIAccount();

            tempAcct.acctName = accountName;
            tempAcct.BuyingPower = ATIClient.BuyingPower(accountName);
            tempAcct.CashValue = ATIClient.CashValue(accountName);
            tempAcct.RealizedPnL = ATIClient.RealizedPnL(accountName);
            tempAcct.ParseStrategies(ATIClient.Strategies(accountName));
            tempAcct.ParseOrders(ATIClient.Orders(accountName));
            return tempAcct;
        }

        public void ReadAccountInformation(ATIAccount acct)
        {
            acct = ReadAccountInformation(AccountName);
        }
    }
}
