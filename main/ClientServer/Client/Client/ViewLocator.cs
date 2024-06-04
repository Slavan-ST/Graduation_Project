using ReactiveUI;
using System;

namespace Client
{
    public class ViewLocator : IViewLocator
    {
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
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
                throw;
            }
        }
    }
}