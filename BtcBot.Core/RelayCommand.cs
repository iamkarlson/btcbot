using System;
using System.Windows.Input;

namespace BtcBot.Core {
    /// <summary>
    /// Defaulr impl of the <see cref="ICommand" />
    /// </summary>
    public class RelayCommand: ICommand {
        private readonly Func<bool> canExecuteEvaluator;
        private readonly Action methodToExecute;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="methodToExecute">Called method on command</param>
        /// <param name="canExecuteEvaluator">Evaluator for checking possibility</param>
        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator) {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        /// <inheritdoc />
        public RelayCommand(Action methodToExecute): this(methodToExecute, null) {
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter) {
            if(canExecuteEvaluator == null) { return true; }
            else {
                bool result = canExecuteEvaluator.Invoke();
                return result;
            }
        }

        /// <inheritdoc />
        public void Execute(object parameter) {
            methodToExecute.Invoke();
        }
    }
}