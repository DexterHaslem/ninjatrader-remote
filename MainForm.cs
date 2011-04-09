/*
	MainForm.cs
    Main windows form code is in this file

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dmh.NinjaTraderRemote
{
    public partial class MainForm : Form
    {
        ATIManager atiManager;

        public MainForm()
        {
            InitializeComponent();
            atiManager = new ATIManager();
            txtHost.Text = atiManager.ConnectionString;
            btnDisconnect.Enabled = false;
            btnReadAcct.Enabled = false;

            tabControl1.Visible = false;

            resetData();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            atiManager.ConnectionString = txtHost.Text;

            if (atiManager.Connect())
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnReadAcct.Enabled = true;
                tabControl1.Visible = true;
                //tabControl1.Enabled = true;
            }
            else
                MessageBox.Show("Couldn't connect to ATI server!");

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (atiManager.IsConnected)
            {
                if (!atiManager.Disconnect())
                    MessageBox.Show("Couldn't disconnect! (wtf)");
                else
                {
                    btnConnect.Enabled = true;
                    btnDisconnect.Enabled = false;
                    btnReadAcct.Enabled = false;
                    tabControl1.Visible = false;
                    //tabControl1.Enabled = false;
                    resetData();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (atiManager.IsConnected)
                atiManager.Disconnect();
        }

        private void btnReadAcct_Click(object sender, EventArgs e)
        {
            resetData();
            ATIAccount test = atiManager.ReadAccountInformation(txtAcctName.Text); // "" is sim101
            //label1.Text = String.Format("{0} {1} {2} : {3}", test.acctName, test.BuyingPower.ToString(), test.CashValue.ToString(),
            //                             test.RealizedPnL);
            if (test.acctName == "" || test.acctName == "Sim101")
                groupBox1.Text = "Account Sim101 stats";
            else
                groupBox1.Text = "Account " + test.acctName + " stats";

            foreach (string order in test.Orders)
            {
                AddOrderInfoToListView(order, listView1);
                //listBox1.Items.Add(String.Format("Order ID:\t{0}\t({1})", order, atiManager.ATIClient.OrderStatus(order)));
            }

            foreach (string strategy in test.Strategies)
            {
                AddStrategyToListView(strategy, listView2);
            }

            lblPnL.Text = test.RealizedPnL.ToString("0.##");
            lblBuyingPower.Text = test.BuyingPower.ToString("0.##");
            lblCashValue.Text = test.CashValue.ToString("0.##");
        }

        private void resetData()
        {
            //listBox1.Items.Clear();
            listView1.Items.Clear();
            listView2.Items.Clear();
            lblBuyingPower.Text = "";
            lblCashValue.Text = "";
            lblPnL.Text = "";
            groupBox1.Text = "Account stats";
        }

        private void AddOrderInfoToListView(string orderID, ListView list)
        {
            string orderStatus = atiManager.OrderStatus(orderID);
            string fillSize = "";
            
            if(orderStatus == "Filled")
            {
                fillSize = atiManager.Filled(orderID).ToString();
            }

            string[] header = new string[3];            
             
            header[0] = orderID;
            header[1] = orderStatus;
            header[2] = fillSize;
            
            ListViewItem lvi = new ListViewItem(header);
            list.Items.Add(lvi);
            //
        }

        private void AddStrategyToListView(string strategyID, ListView list)
        {
            string[] header = new string[2];

            string strategyPosition = atiManager.StrategyPosition(strategyID);

            header[0] = strategyID;
            header[1] = strategyPosition;

            ListViewItem lvi = new ListViewItem(header);
            list.Items.Add(lvi);
        }
    }
}
