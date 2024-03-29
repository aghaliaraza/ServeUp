﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeUpApiServer.Infrastructure.Middlewares
{
    /// <summary>
    /// Options for configuring the <see cref="HttpTrackingMiddleware"/> class.
    /// </summary>
    public sealed class TrackingOptions
    {
        /// <summary>
        /// Interface to an implementation of a durable store for tracking calls.
        /// </summary>
        public ITrackingStore TrackingStore { get; set; }

        /// <summary>
        /// Name of the HTTP response header property holding the tracking identifier.
        /// By default, the name of this property is "http-tracking-id"
        /// </summary>
        public string TrackingIdPropertyName { get; set; }

        /// <summary>
        /// The maximum number of bytes from the request to persist to durable storage.
        /// </summary>
        public long? MaximumRecordedRequestLength { get; set; }

        /// <summary>
        /// The maximum number of bytes from the response to persist to durable storage.
        /// </summary>
        public long? MaximumRecordedResponseLength { get; set; }
    }
}
