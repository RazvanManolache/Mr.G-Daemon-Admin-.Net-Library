using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{
    public class StringBuffer : INotifyPropertyChanged
    {
        private readonly int _maxSizeInBytes;
        private readonly StringBuilder _builder;
        private int _currentSizeInBytes;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StringBuffer(): this(1024)
        {
        }

        public StringBuffer(int maxSizeInKB)
        {
            _maxSizeInBytes = maxSizeInKB * 1024;
            _builder = new StringBuilder();
            _currentSizeInBytes = 0;
        }

        public void Clear()
        {
            _builder.Clear();
            _currentSizeInBytes = 0;
            OnPropertyChanged(nameof(CurrentContent));
            OnPropertyChanged(nameof(LengthInBytes));
        }

        public void Append(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            var valueBytes = Encoding.UTF8.GetByteCount(value);
            _currentSizeInBytes += valueBytes;
            value += Environment.NewLine;
            _builder.Append(value);

            while (_currentSizeInBytes > _maxSizeInBytes)
            {
                int excessSize = _currentSizeInBytes - _maxSizeInBytes;
                string firstChar = _builder[0].ToString();
                int firstCharBytes = Encoding.UTF8.GetByteCount(firstChar);

                if (firstCharBytes <= excessSize)
                {
                    _builder.Remove(0, 1);
                    _currentSizeInBytes -= firstCharBytes;
                }
                else
                {
                    break;
                }
            }
            OnPropertyChanged(nameof(CurrentContent));
            OnPropertyChanged(nameof(LengthInBytes));
        }

        public override string ToString()
        {
            return _builder.ToString();
        }

        // Override + operator
        public static StringBuffer operator +(StringBuffer builder, string value)
        {
            builder.Append(value);
            return builder;
        }


        public string CurrentContent => _builder.ToString();

        public int LengthInBytes => _currentSizeInBytes;
    }
}
