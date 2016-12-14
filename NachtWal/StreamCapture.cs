using System.IO;
using System.Text;

namespace NachtWal
{
    /// <summary>
    /// IO Stream Capture
    /// Capturing HTML generated from ASP.NET
    /// http://stackoverflow.com/questions/386487/capturing-html-generated-from-asp-net
    /// Stack OverflowのAnswer 22を参考にしました。
    /// </summary>
    public class StreamCapture : Stream
    {
        private readonly Stream _streamToCapture;
        private readonly Encoding _responseEncoding;
        private string _streamContent;

        public string StreamContent
        {
            get { return _streamContent; }
            private set { _streamContent = value; }
        }

        public StreamCapture(Stream streamToCapture, Encoding responseEncoding)
        {
            _responseEncoding = responseEncoding;
            _streamToCapture = streamToCapture;
        }

        public override bool CanRead { get { return _streamToCapture.CanRead; } }
        public override bool CanSeek { get { return _streamToCapture.CanSeek; } }
        public override bool CanWrite { get { return _streamToCapture.CanWrite; } }
        public override void Flush() { _streamToCapture.Flush(); }
        public override long Length { get { return _streamToCapture.Length; } }
        public override long Position
        {
            get { return _streamToCapture.Position; }
            set
            {
                _streamToCapture.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count) { return _streamToCapture.Read(buffer, offset, count); }
        public override long Seek(long offset, SeekOrigin origin) { return _streamToCapture.Seek(offset, origin); }
        public override void SetLength(long value) { _streamToCapture.SetLength(value); }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _streamContent += _responseEncoding.GetString(buffer);
            _streamToCapture.Write(buffer, offset, count);
        }

        public override void Close()
        {
            _streamToCapture.Close();
            base.Close();
        }
    }
}
