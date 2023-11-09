using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Tcp.Base.Message;

public class TcpMessage
{
    private string _text;

    public string Text
    {
        get => _text;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(_text));

            _text = value;
        }
    }
    public TcpMessage(string text)
    {
        this.Text = text;
    }
}
