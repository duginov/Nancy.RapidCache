﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nancy.RapidCache.Projection
{
    /// <summary>
    /// Persistable Nancy Response
    /// </summary>
    [Serializable]
    public class SerializableResponse
    {
        public string ContentType { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Contents { get; set; }
        public DateTime Expiration { get; set; }

        public SerializableResponse(Response response, DateTime expiration)
        {
            Expiration = expiration;
            ContentType = response.ContentType;
            Headers = response.Headers;
            StatusCode = response.StatusCode;

            using (var memoryStream = new MemoryStream())
            {
                response.Contents(memoryStream);
#if NETSTANDARD1_6
                memoryStream.TryGetBuffer(out ArraySegment<byte> buffer);
#else
                var buffer = new ArraySegment<byte>();

                if (memoryStream != null)
                {
                    buffer = new ArraySegment<byte>(memoryStream.GetBuffer());
                }
#endif
                Contents = Encoding.UTF8.GetString(buffer.Where(a => a != 0).ToArray());
            }
        }
    }
}