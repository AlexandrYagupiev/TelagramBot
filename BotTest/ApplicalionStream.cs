using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BotTest
{
    public class ApplicalionStream : Stream
    {
        private readonly int bufferSize;
        private readonly FileStream stream;
        private readonly Queue<byte> bytes;

        public ApplicalionStream (int bufferSize, FileStream stream)
        {
            this.bufferSize = bufferSize;
            this.stream = stream;
            this.bytes = new Queue<byte>();
        }
        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var readCount=stream.Read(buffer,offset,count);
            if(readCount>0)
            {
                for(var i=0;i!=readCount;i++)
                {
                    bytes.Enqueue(buffer[i]);
                }              
            }
            return readCount;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public Application GetNextApplication()
        {
            var buffer = new byte[bufferSize];
            Read(buffer,0,bufferSize);
            while(bytes.Count!=0)
            {
                var applicationBytes = GetNextApplicationInBytes();
                var applicationStr = UTF8Encoding.UTF8.GetString(applicationBytes);

            }
        }

        private byte[] GetNextApplicationInBytes()
        {
            var applicationBytes = new List<byte>();
            var _byte = bytes.Dequeue();
            while(_byte!='$')
            {
                applicationBytes.Add(_byte);
            }
            return applicationBytes.ToArray();
        }
    }
}
