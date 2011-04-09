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
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            atiManager.ConnectionString = txtHost.Text;

            if (atiManager.Connect())
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                ATIAccount test = atiManager.ReadAccountInformation("");
                label1.Text = test.BuyingPower.ToString();
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
                }
            }
        }
    }
}
