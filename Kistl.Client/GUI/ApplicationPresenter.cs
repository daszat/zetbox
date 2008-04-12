using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.GUI
{
    public class ApplicationPresenter : Presenter
    {
        protected override void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }

    public interface IMainUI: IBasicControl
    {
        void Show();
        void Close();
    }
}
