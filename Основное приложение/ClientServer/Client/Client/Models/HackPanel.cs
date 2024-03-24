using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class HackPanel : Panel// or Decorator
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            if (double.IsInfinity(availableSize.Height))
            {
                var c = this.GetVisualAncestors().OfType<Control>().FirstOrDefault(v => v.IsArrangeValid);
                if (c != null)
                {
                    availableSize = availableSize.WithHeight(c.Bounds.Height);
                }
            }
            return base.MeasureOverride(availableSize);
        }
    }
}
