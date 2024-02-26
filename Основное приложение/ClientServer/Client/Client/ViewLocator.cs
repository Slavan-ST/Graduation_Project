using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Client.ViewModels;
using ReactiveUI;
using System;
using System.Diagnostics;

namespace Client
{
    public class ViewLocator : IViewLocator
    {
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            // Find view's by chopping of the 'Model' on the view model name
            // MyApp.ShellViewModel => MyApp.ShellView
            var viewModelName = viewModel!.GetType().FullName;
            var viewTypeName = viewModelName!.Replace("ViewModel", "View");

            try
            {
                var viewType = Type.GetType(viewTypeName);
                if (viewType == null)
                {
                    return null;
                }
                return Activator.CreateInstance(viewType) as IViewFor;
            }
            catch (Exception)
            {
                Debug.WriteLine("Error in locator: " + viewTypeName);
                throw;
            }
        }
    }
}