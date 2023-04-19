using System.Text;

namespace LanceServer.Core.Stream;

class StreamLog : System.IO.Stream
{
    string _name;

    public StreamLog(string name) { _name = name; }

    public override bool CanRead { get { return false; } }

    public override bool CanSeek { get { return false; } }

    public override bool CanWrite { get { return true; } }

    public override long Length => throw new NotImplementedException();

    public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void Flush()
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
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

    public override void Write(byte[] buffer, int offset, int count)
    {
        DateTime now = DateTime.Now;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Raw message from " + _name + " " + now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
        var truncatedArray = new byte[count];
        for (int i = offset; i < offset + count; ++i)
            truncatedArray[i - offset] = buffer[i];
        string str = System.Text.Encoding.Default.GetString(truncatedArray);
        sb.AppendLine("data (length " + str.Length + ")= '" + str + "'");
        System.Console.Error.WriteLine(sb.ToString());
    }
}