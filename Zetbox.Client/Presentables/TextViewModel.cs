namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.API.Dtos;

    [ViewModelDescriptor]
    public class TextViewModel : ViewModel
    {
        public new delegate TextViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string text);

        public TextViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string text)
            : base(appCtx, dataCtx, parent)
        {
            _text = text;
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        private Formatting _formatting;
        public Formatting Formatting
        {
            get
            {
                return _formatting;
            }
            set
            {
                if (_formatting != value)
                {
                    _formatting = value;
                    OnPropertyChanged("Formatting");
                }
            }
        }


        public override string Name
        {
            get { return _text; }
        }
    }
}
