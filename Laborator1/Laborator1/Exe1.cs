using System;

namespace Laborator1
{
    public class Message
    {
        public delegate void ChangeMessageHandler(object sender, AskMessageChangedEventArgs e);

        public event ChangeMessageHandler AskMessageChanged;
        private object _message;

        public object AskMessage
        {
            set
            {
                _message = value ?? throw new ArgumentNullException(nameof(value));
                OnAskMessageChanged();
            }
        }

        protected void OnAskMessageChanged()
        {
            AskMessageChanged?.Invoke(this, new AskMessageChangedEventArgs(_message));
        }
    }

    public class AskMessageChangedEventArgs : EventArgs
    {
        private object _askMessage;

        public AskMessageChangedEventArgs(object askMessage)
        {
            _askMessage = askMessage;
        }

        public object AskMessage => _askMessage;
    }
}