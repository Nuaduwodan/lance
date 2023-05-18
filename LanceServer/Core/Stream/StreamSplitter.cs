using System.Diagnostics;

namespace LanceServer.Core.Stream;

/// <summary>
/// Attaches a slave stream to a stream that can be reused
/// </summary>
class StreamSplitter : System.IO.Stream
{
    [Flags]
    public enum StreamOwnership
    {
        OwnNone = 0x0,
        OwnPrimaryStream = 0x1,
        OwnSlaveStream = 0x2,
        OwnBoth = OwnPrimaryStream | OwnSlaveStream
    }

    public enum SlaveFailAction
    {
        Propagate,
        Ignore,
        Filter
    }

    public delegate SlaveFailAction SlaveFailHandler(
        object oSender, SlaveFailMethod method, Exception exc
    );

    public enum SlaveFailMethod
    {
        Read,
        Write,
        Seek
    }

    private readonly System.IO.Stream _primaryStream;
    private readonly System.IO.Stream _slaveStream;
    private StreamOwnership _streamsOwned;

    private SlaveFailAction _readFailAction = SlaveFailAction.Propagate;
    private SlaveFailAction _writeFailAction = SlaveFailAction.Propagate;
    private SlaveFailAction _seekFailAction = SlaveFailAction.Propagate;

    private SlaveFailHandler? _slaveReadFailFilter;
    private SlaveFailHandler? _slaveWriteFailFilter;
    private SlaveFailHandler? _slaveSeekFailFilter;

    private int _lastReadResult;

    public StreamSplitter(
        System.IO.Stream primaryStream, System.IO.Stream slaveStream,
        StreamOwnership streamsOwned)
    {
        _primaryStream = primaryStream;
        _slaveStream = slaveStream;
        _streamsOwned = streamsOwned;
    }
    
    public override void Close()
    {
        Flush();
        if ((_streamsOwned & StreamOwnership.OwnPrimaryStream) > 0)
        {
            _primaryStream.Close();
        }

        if ((_streamsOwned & StreamOwnership.OwnSlaveStream) > 0)
        {
            _slaveStream.Close();
        }
        
        base.Close();
    }
    
    public StreamOwnership StreamsOwned
    {
        get => _streamsOwned;
        set => _streamsOwned = value;
    }

    public System.IO.Stream PrimaryStream => _primaryStream;
    
    public System.IO.Stream SlaveStream => _slaveStream;
    
    public int LastReadResult => _lastReadResult;
    
    public SlaveFailAction SlaveFailActions
    {
        set
        {
            SlaveReadFailAction = value;
            SlaveWriteFailAction = value;
            SlaveSeekFailAction = value;
        }
    }

    public SlaveFailHandler? SlaveFailFilters
    {
        set
        {
            SlaveReadFailFilter = value;
            SlaveWriteFailFilter = value;
            SlaveSeekFailFilter = value;
        }
    }

    public SlaveFailAction SlaveReadFailAction
    {
        get => _readFailAction;

        set
        {
            if (value == SlaveFailAction.Filter)
            {
                throw new InvalidOperationException(
                    "You cannot set this property to "
                    + "SlaveFailAction.Filter manually.  Use the "
                    + "SlaveReadFailFilter property instead."
                );
            }
            else
            {
                _slaveReadFailFilter = null;
                _readFailAction = value;
            }
        }
    }

    public SlaveFailAction SlaveWriteFailAction
    {
        get => _writeFailAction;

        set
        {
            if (value == SlaveFailAction.Filter)
            {
                throw new InvalidOperationException(
                    "You cannot set this property to "
                    + "SlaveFailAction.Filter manually.  Use the "
                    + "SlaveWriteFailFilter property instead."
                );
            }
            else
            {
                _slaveWriteFailFilter = null;
                _writeFailAction = value;
            }
        }
    }

    public SlaveFailAction SlaveSeekFailAction
    {
        get => _seekFailAction;

        set
        {
            if (value == SlaveFailAction.Filter)
            {
                throw new InvalidOperationException(
                    "You cannot set this property to "
                    + "SlaveFailAction.Filter manually.  Use the "
                    + "SlaveSeekFailFilter property instead."
                );
            }
            else
            {
                _slaveSeekFailFilter = null;
                _seekFailAction = value;
            }
        }
    }

    public SlaveFailHandler? SlaveWriteFailFilter
    {
        get => _slaveWriteFailFilter;

        set
        {
            if (_slaveWriteFailFilter != null)
            {
                _writeFailAction = SlaveFailAction.Propagate;
            }

            _slaveWriteFailFilter = value;
            if (value != null)
            {
                _writeFailAction = SlaveFailAction.Filter;
            }
        }
    }

    public SlaveFailHandler? SlaveReadFailFilter
    {
        get => _slaveReadFailFilter;

        set
        {
            if (_slaveReadFailFilter != null)
            {
                _readFailAction = SlaveFailAction.Propagate;
            }

            _slaveReadFailFilter = value;

            if (value != null)
            {
                _readFailAction = SlaveFailAction.Filter;
            }
        }
    }

    public SlaveFailHandler? SlaveSeekFailFilter
    {
        get => _slaveSeekFailFilter;

        set
        {
            if (_slaveSeekFailFilter != null)
            {
                _seekFailAction = SlaveFailAction.Propagate;
            }

            _slaveSeekFailFilter = value;
            if (value != null)
            {
                _seekFailAction = SlaveFailAction.Filter;
            }
        }
    }

    public override bool CanRead => _primaryStream.CanRead;

    public override bool CanSeek => _primaryStream.CanSeek && _slaveStream.CanSeek;

    public override bool CanWrite => _primaryStream.CanWrite && _slaveStream.CanWrite;

    public override void Flush()
    {
        _primaryStream.Flush();

        if (_writeFailAction == SlaveFailAction.Propagate)
        {
            _slaveStream.Flush();
        }
        else
        {
            try
            {
                _slaveStream.Flush();
            }
            catch (Exception exc)
            {
                HandleSlaveException(
                    exc, SlaveFailMethod.Write,
                    _writeFailAction
                );
            }
        }
    }

    public override long Length => _primaryStream.Length;

    public override void SetLength(long len)
    {
        long diff = len - _primaryStream.Length;

        _primaryStream.SetLength(len);

        if (_seekFailAction == SlaveFailAction.Propagate)
        {
            _slaveStream.SetLength(_slaveStream.Length + diff);
        }
        else
        {
            try
            {
                _slaveStream.SetLength(_slaveStream.Length + diff);
            }
            catch (Exception exc)
            {
                HandleSlaveException(
                    exc, SlaveFailMethod.Seek, _seekFailAction
                );
            }
        }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        _lastReadResult = _primaryStream.Read(buffer, offset, count);
        if (_lastReadResult != 0)
        {
            if (_readFailAction == SlaveFailAction.Propagate)
            {
                _slaveStream.Write(buffer, offset, _lastReadResult);
            }
            else
            {
                try
                {
                    _slaveStream.Write(buffer, offset, _lastReadResult);
                }
                catch (Exception exc)
                {
                    HandleSlaveException(
                        exc, SlaveFailMethod.Read, _readFailAction
                    );
                }
            }
        }

        return _lastReadResult;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _primaryStream.Write(buffer, offset, count);

        if (_writeFailAction == SlaveFailAction.Propagate)
        {
            _slaveStream.Write(buffer, offset, count);
        }
        else
        {
            try
            {
                _slaveStream.Write(buffer, offset, count);
            }
            catch (Exception exc)
            {
                HandleSlaveException(
                    exc, SlaveFailMethod.Write,
                    _writeFailAction
                );
            }
        }
    }

    public override long Position
    {
        get => _primaryStream.Position;

        set
        {
            long diff = value - _primaryStream.Position;

            _primaryStream.Position = value;

            if (_seekFailAction == SlaveFailAction.Propagate)
            {
                _slaveStream.Position += diff;
            }
            else
            {
                try
                {
                    _slaveStream.Position += diff;
                }
                catch (Exception exc)
                {
                    HandleSlaveException(
                        exc, SlaveFailMethod.Seek, _seekFailAction
                    );
                }
            }
        }
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        if (origin == SeekOrigin.Begin)
        {
            Position = offset;
        }
        else if (origin == SeekOrigin.Current)
        {
            Position += offset;
        }
        else if (origin == SeekOrigin.End)
        {
            Position = Length + offset;
        }

        return Position;
    }

    private void FilterException(Exception exc, SlaveFailMethod method)
    {
        var action = SlaveFailAction.Filter;

        switch (method)
        {
            case SlaveFailMethod.Read:
                action = _slaveReadFailFilter!(this, method, exc);
                break;
            case SlaveFailMethod.Write:
                action = _slaveWriteFailFilter!(this, method, exc);
                break;
            case SlaveFailMethod.Seek:
                action = _slaveSeekFailFilter!(this, method, exc);
                break;
            default:
                Debug.Assert(false, "Unhandled SlaveFailMethod.");
                break;
        }

        if (action == SlaveFailAction.Filter)
        {
            throw new InvalidOperationException(
                "SlaveFailAction.Filter is not a valid return "
                + "value for the ReadFailFilter delegate.",
                exc
            );
        }
        
        HandleSlaveException(exc, method, action);
    }

    private void HandleSlaveException(
        Exception exc, SlaveFailMethod method, SlaveFailAction action)
    {
        if (action == SlaveFailAction.Propagate)
        {
            throw exc;
        }
        else if (action == SlaveFailAction.Ignore)
        {
            // Intentionally Empty
        }
        else if (action == SlaveFailAction.Filter)
        {
            FilterException(exc, method);
        }
        else
        {
            Debug.Assert(false, "Unhandled SlaveFailAction");
        }
    }
}