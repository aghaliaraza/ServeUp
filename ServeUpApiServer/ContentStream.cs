using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServeUpApiServer
{
    public class ContentStream : Stream
    {
        protected readonly Stream buffer_;
        protected readonly Stream stream_;

        private long contentLength_ = 0L;

        public ContentStream(Stream buffer, Stream stream)
        {
            buffer_ = buffer;
            stream_ = stream;
        }

        /// <summary>
        /// Returns the recorded length of the underlying stream.
        /// </summary>
        public virtual long ContentLength
        {
            get { return contentLength_; }
        }

        public override bool CanRead
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanSeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanWrite
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public async Task<String> ReadContentAsync(string contentType, long maxCount)
        {
            if (!IsTextContentType(contentType))
            {
                contentType = String.IsNullOrEmpty(contentType) ? "N/A" : contentType;
                return String.Format("{0} [{1} bytes]", contentType, ContentLength);
            }

            buffer_.Seek(0, SeekOrigin.Begin);

            var length = Math.Min(ContentLength, maxCount);

            var buffer = new byte[length];
            var count = await buffer_.ReadAsync(buffer, 0, buffer.Length);

            return
                GetEncoding(contentType)
                .GetString(buffer, 0, count)
                ;
        }

        protected void WriteContent(byte[] buffer, int offset, int count)
        {
            buffer_.Write(buffer, offset, count);
        }

        #region Implementation

        private static bool IsTextContentType(string contentType)
        {
            if (contentType == null)
                return false;

            var isTextContentType =
                contentType.StartsWith("application/json") ||
                contentType.StartsWith("application/xml") ||
                contentType.StartsWith("text/")
                ;
            return isTextContentType;
        }

        private static Encoding GetEncoding(string contentType)
        {
            var charset = "utf-8";
            var regex = new Regex(@";\s*charset=(?<charset>[^\s;]+)");
            var match = regex.Match(contentType);
            if (match.Success)
                charset = match.Groups["charset"].Value;

            try
            {
                return Encoding.GetEncoding(charset);
            }
            catch (ArgumentException e)
            {
                return Encoding.UTF8;
            }
        }
 
    #endregion
 
    #region System.IO.Stream Overrides
 
    
 
    public override int Read(byte[] buffer, int offset, int count)
        {
            // read content from the request stream

            count = stream_.Read(buffer, offset, count);
            contentLength_ += count;

            // record the read content into our temporary buffer

            if (count != 0)
                WriteContent(buffer, offset, count);

            return count;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // store the bytes into our local stream

            WriteContent(buffer, 0, count);

            // write the bytes to the response stream
            // and record the actual number of bytes written

            stream_.Write(buffer, offset, count);
            contentLength_ += count;
        }

        #endregion

        #region IDisposable Implementation

        protected override void Dispose(bool disposing)
        {
            buffer_.Dispose();

            // not disposing the stream_ member
            // which is owned by the Owin infrastructure
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
