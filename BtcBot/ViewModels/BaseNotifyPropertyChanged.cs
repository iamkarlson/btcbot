using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BtcBot.ViewModels {
    /// <summary>
    /// Implements standard WPF interface <see cref="INotifyPropertyChanged" />
    /// </summary>
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged {
        private readonly Dictionary<string, object> _propertyBackingDictionary = new Dictionary<string, object>();

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression) {
            OnPropertyChanged(((MemberExpression) propertyExpression.Body).Member.Name);
        }

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null) {
                var e = new PropertyChangedEventArgs(propertyName);
                propertyChanged(this, e);
            }
        }

        protected void RaisePropertyChanged(string propertyName) { OnPropertyChanged(propertyName); }

        protected T getProp<T>([CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName));
            }

            object value;
            if (_propertyBackingDictionary.TryGetValue(propertyName, out value)) {
                return (T) value;
            }

            return default(T);
        }

        protected bool setProp<T>(T newValue, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (EqualityComparer<T>.Default.Equals(newValue, getProp<T>(propertyName))) {
                return false;
            }

            _propertyBackingDictionary[propertyName] = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}