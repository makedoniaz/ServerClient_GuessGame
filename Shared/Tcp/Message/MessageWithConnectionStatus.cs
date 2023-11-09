using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Tcp.Base.Message;
using Shared.Tcp.ConnectionStatus;

namespace Shared.Tcp.Message;

public class MessageWithConnectionStatus : TcpMessage
{
    private TcpConnectionStatus _status;

    public TcpConnectionStatus Status
    {
        get => _status;
        set => _status = value;
    }

    public MessageWithConnectionStatus(string text, TcpConnectionStatus status) : base(text)
    {
        Status = status;
    }
}
