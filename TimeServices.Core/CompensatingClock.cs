/* Copyright (c) 2011, hatchan
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 *    * Redistributions of source code must retain the above copyright notice, this list of 
 *      conditions and the following disclaimer.
 *    * Redistributions in binary form must reproduce the above copyright notice, this list of 
 *      conditions and the following disclaimer in the documentation and/or other materials 
 *      provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS 
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
 * COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
 * GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED 
 * AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED 
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Diagnostics;

namespace TimeServices.Core
{
    /// <summary>
    /// The <see cref="CompensatingClock"/> uses a simple (naive) algoritme to compensate for lag. 
    /// Only recommended for testing or development.
    /// Only recommended for a <see cref="IClock"/> which has lag, like a <see cref="IClock"/> which communicates with a webservice. 
    /// </summary>
    public class CompensatingClock : IClock
    {
        private readonly IClock _clock;

        /// <summary>
        /// Create a new instance of <see cref="CompensatingClock"/>.
        /// </summary>
        /// <param name="clock">The inner <see cref="IClock"/> on which to apply the compensating.</param>
        public CompensatingClock(IClock clock)
        {
            _clock = clock;
        }

        /// <summary>
        /// A simple method which will divide the timespan by two.
        /// </summary>
        /// <param name="input">The timespan which needs to be divided by two</param>
        /// <returns><paramref name="input"/> / 2</returns>
        protected static TimeSpan TimeSpanDividedByTwo(TimeSpan input)
        {
            return new TimeSpan(input.Ticks / 2);
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> set to the source's local time.
        /// </summary>
        public DateTime Now
        {
            get
            {
                var stopwatch = Stopwatch.StartNew();
                var result = _clock.Now;
                stopwatch.Stop();
                return result - TimeSpanDividedByTwo(stopwatch.Elapsed);
            }
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> set to the coordinated universal time.
        /// </summary>
        public DateTime UtcNow
        {
            get
            {
                var stopwatch = Stopwatch.StartNew();
                var result = _clock.UtcNow;
                stopwatch.Stop();
                return result - TimeSpanDividedByTwo(stopwatch.Elapsed);
            }
        }

        /// <summary>
        /// Gets a <see cref="DateTimeOffset"/> with it's offset set to the source's local time.
        /// </summary>
        public DateTimeOffset NowAsOffset
        {
            get
            {
                var stopwatch = Stopwatch.StartNew();
                var result = _clock.NowAsOffset;
                stopwatch.Stop();
                return result - TimeSpanDividedByTwo(stopwatch.Elapsed);
            }
        }
    }
}