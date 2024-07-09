using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// This will eventually be a CodeBit

namespace Bredd.Json;

internal class JsonWriterResult : BufferedStreamResult {
    public JsonWriterResult() {
        ContentType = "application/json";
    }

    public Utf8JsonWriter OpenWriter() {
        var options = new JsonWriterOptions() { Indented = true };
        return new Utf8JsonWriter(ResultStream, options);
    }
} // class JsonWriterResult

internal abstract class BufferedStreamResult : ActionResult {
    private static readonly UTF8Encoding s_UTF8 = new UTF8Encoding(false);

    BufferedWriteStream m_buffer = new BufferedWriteStream();

    protected Stream ResultStream { get => m_buffer; }
    protected string ContentType { get; set; }
    protected int StatusCode { get; set; }

    protected BufferedStreamResult(int statusCode = StatusCodes.Status200OK) {
        ContentType = "text/plain";
        StatusCode = statusCode;
    }

    public override void ExecuteResult(ActionContext context) {
        context.HttpContext.Response.ContentType = ContentType;
        context.HttpContext.Response.StatusCode = StatusCode;
        m_buffer.DrainAsync(context.HttpContext.Response.Body).GetAwaiter().GetResult();
    }

    public override Task ExecuteResultAsync(ActionContext context) {
        context.HttpContext.Response.ContentType = ContentType;
        context.HttpContext.Response.StatusCode = StatusCode;
        return m_buffer.DrainAsync(context.HttpContext.Response.Body);
    }

    private class BufferedWriteStream : Stream {
        private const int c_pageSize = 4 * 1024;

        private LinkedList<byte[]> m_buffer = new LinkedList<byte[]>();
        private byte[]? m_currentPage = null;
        private int m_pagePos = 0;
        private bool m_disposed = false;

        #region Stream Overrides

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => m_buffer.Count * c_pageSize + m_pagePos;

        public override long Position {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (m_disposed)
                throw new ObjectDisposedException(nameof(BufferedWriteStream));

            // Write into buffers
            while (count > 0) {
                if (m_currentPage == null || m_pagePos >= c_pageSize) {
                    m_currentPage = new byte[c_pageSize];
                    m_pagePos = 0;
                    m_buffer.AddLast(m_currentPage);
                }
                int n = c_pageSize - m_pagePos;
                if (n > count)
                    n = count;
                Buffer.BlockCopy(buffer, offset, m_currentPage, m_pagePos, n);
                m_pagePos += n;
                offset += n;
                count -= n;
            }
        }

        public override void Flush() {
            // Do nothing.
        }

        public override Task FlushAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public override void SetLength(long value) => throw new NotSupportedException();

        protected override void Dispose(bool disposing) {
            m_disposed = true;
        }

        #endregion Stream overrides

        public async Task DrainAsync(Stream destination) {
            int fullPages = m_buffer.Count - 1;
            foreach (var page in m_buffer) {
                if (fullPages > 0)
                    await destination.WriteAsync(page, 0, c_pageSize);
                else
                    await destination.WriteAsync(page, 0, m_pagePos);
                --fullPages;
            }
        }
    } // class BufferedWriteStream
} // class BufferedStreamResult


// namespace

