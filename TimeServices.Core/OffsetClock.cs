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

namespace TimeServices.Core
{
    /// <summary>
    /// The <see cref="OffsetClock"/> will subtract the timespan from the values of the wrapped clock.
    /// </summary>
    public class OffsetClock : IClock
    {
        private readonly IClock _clock;
        private readonly TimeSpan _timeSpan;

        /// <summary>
        /// Create a new instance of <see cref="OffsetClock"/>.
        /// </summary>
        /// <param name="clock">The inner <see cref="IClock"/> on which to apply the offset</param>
        /// <param name="timeSpan">The offset</param>
        public OffsetClock(IClock clock, TimeSpan timeSpan)
        {
            _clock = clock;
            _timeSpan = timeSpan;
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> set to the source's local time.
        /// </summary>
        public DateTime Now
        {
            get { return _clock.Now - _timeSpan; }
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> set to the coordinated universal time.
        /// </summary>
        public DateTime UtcNow
        {
            get { return _clock.UtcNow - _timeSpan; }
        }

        /// <summary>
        /// Gets a <see cref="DateTimeOffset"/> with it's offset set to the source's local time.
        /// </summary>
        public DateTimeOffset NowAsOffset
        {
            get { return _clock.NowAsOffset - _timeSpan; }
        }
    }
}