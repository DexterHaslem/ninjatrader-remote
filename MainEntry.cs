﻿/*
	MainEntry.cs
	Program entry point, blah

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
using System.Windows.Forms;
using System.Diagnostics;

namespace dmh.NinjaTraderRemote
{
    static class MainEntry
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //Application.Exit();
            //TimeInForce test = TimeInForce.DAY;
            //Trace.WriteLine(Enum.GetName(typeof(TimeInForce), test));

        }
    }
}
