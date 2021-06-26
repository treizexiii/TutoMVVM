﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TutoMVVM.WpfApplication.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        private bool _isExecuting;

        public bool IsExecuting
        {
            get { return _isExecuting; }
            set { _isExecuting = value; CanExecuteChanged?.Invoke(this, new EventArgs()); }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object parameter)
        {
            IsExecuting = true;
            await ExecuteAsync(parameter);
            IsExecuting = false;
        }

        protected abstract Task ExecuteAsync(object parameter);
    }
}
