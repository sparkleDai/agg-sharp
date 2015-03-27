﻿/*
Copyright (c) 2015, Lars Brubaker
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those
of the authors and should not be interpreted as representing official policies, 
either expressed or implied, of the FreeBSD Project.
*/

using System;
using System.Diagnostics;

namespace MatterHackers.Agg
{
	public class DumpCallStackIfSlow : IDisposable
	{
		int millisecondsToReportIfOver = 0;
		Stopwatch timeToCallOnDrawFunction;
		string startMessage;

		public DumpCallStackIfSlow(double maxSecondsBeforePrintCallStack, string startMessage)
		{
			this.startMessage = startMessage;
			millisecondsToReportIfOver = (int)(maxSecondsBeforePrintCallStack * 1000);
			timeToCallOnDrawFunction = Stopwatch.StartNew();
		}

		public void Dispose()
		{
			timeToCallOnDrawFunction.Stop();
			if (timeToCallOnDrawFunction.ElapsedMilliseconds > millisecondsToReportIfOver)
			{
				Debug.Write("{0} [{1}ms]\n{2}\n".FormatWith(startMessage, timeToCallOnDrawFunction.ElapsedMilliseconds, Environment.StackTrace));
			}
		}
	}
}
