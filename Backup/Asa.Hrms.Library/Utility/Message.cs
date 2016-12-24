using System;

namespace Asa.Hrms.Utility
{
    /// <summary>
    /// Summary description for Message
    /// </summary>
    public class Message
    {
        private MessageType _Type = MessageType.Information;
        private string _Msg;

        public MessageType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string Msg
        {
            get { return _Msg; }
            set { _Msg = value; }
        }

        public Message()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Clear()
        {
            _Msg = "";
            _Type = MessageType.Information;
        }
    }

    public enum MessageType
    {
        Information,
        Warning,
        Error
    }
}
