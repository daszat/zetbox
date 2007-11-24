using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Collections.Specialized;

namespace Microsoft.Samples.KMoore.WPFSamples.Zap
{
    //TODO: Need to add a property to define the child command item DataTemplate.
    //right now I hard-wire in generic.xaml assuming colors, which is not goodness
    [ContentProperty("ZapPanel")]
    public class ZapScroller : Control, INotifyPropertyChanged
    {
        static ZapScroller()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZapScroller), new FrameworkPropertyMetadata(typeof(ZapScroller)));
        }

        public ZapScroller()
        {
            SetValue(CommandsPropertyKey, new ReadOnlyObservableCollection<ZapCommandItem>(_commands));

            _firstCommand = new FirstPreviousNextLastCommand(this, ZapCommandType.First);
            _previousCommand = new FirstPreviousNextLastCommand(this, ZapCommandType.Previous);
            _nextCommand = new FirstPreviousNextLastCommand(this, ZapCommandType.Next);
            _lastCommand = new FirstPreviousNextLastCommand(this, ZapCommandType.Last);


        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            Binding binding = new Binding("Items");
            binding.Source = this.TemplatedParent;
            this.SetBinding(ParentItemsInternalProperty, binding);
        }

        private static readonly DependencyProperty ParentItemsInternalProperty = DependencyProperty.Register("ParentItemsInternal",
            typeof(ItemCollection), typeof(ZapScroller), new PropertyMetadata(new PropertyChangedCallback(parentItemsChanged)));

        private static void parentItemsChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ((ZapScroller)element).parentItemsChanged();
        }


        private static readonly DependencyPropertyKey ParentItemsPropertyKey = DependencyProperty.RegisterReadOnly("ParentItems",
            typeof(ItemCollection), typeof(ZapScroller), new PropertyMetadata());

        public static readonly DependencyProperty ParentItemsProperty = ParentItemsPropertyKey.DependencyProperty;

        public ItemCollection ParentItems
        {
            get
            {
                return (ItemCollection)GetValue(ParentItemsProperty);
            }
            private set
            {
                SetValue(ParentItemsPropertyKey, value);
            }
        }


        private static readonly DependencyPropertyKey CommandsPropertyKey = DependencyProperty.RegisterReadOnly("Commands",
            typeof(ReadOnlyObservableCollection<ZapCommandItem>), typeof(ZapScroller), new PropertyMetadata(null));

        public static readonly DependencyProperty CommandsProperty = CommandsPropertyKey.DependencyProperty;

        public ReadOnlyObservableCollection<ZapCommandItem> Commands
        {
            get
            {
                return (ReadOnlyObservableCollection<ZapCommandItem>)GetValue(CommandsProperty);
            }
        }



        private static readonly DependencyPropertyKey ItemCountPropertyKey = DependencyProperty.RegisterReadOnly("ItemCount",
            typeof(int), typeof(ZapScroller), new PropertyMetadata(0));

        public static readonly DependencyProperty ItemCountProperty = ItemCountPropertyKey.DependencyProperty;

        public int ItemCount
        {
            get { return (int)GetValue(ItemCountProperty); }
        }

        public int CurrentItemIndex
        {
            get { return _currentItemIndex; }
            set
            {
                if (_currentItemIndex != value)
                {
                    _currentItemIndex = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentItemIndex"));
                }
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            findZapDecorator();

            return base.MeasureOverride(constraint);
        }


        public ZapPanel ZapPanel
        {
            get { return _zapPanel; }
            set
            {
                _zapPanel = value;
            }
        }

        private ICommand _firstCommand;
        public ICommand FirstCommand
        {
            get { return _firstCommand; }
        }

        private ICommand _previousCommand;
        public ICommand PreviousCommand
        {
            get { return _previousCommand; }
        }

        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get { return _nextCommand; }
        }

        private ICommand _lastCommand;
        public ICommand LastCommand
        {
            get { return _lastCommand; }
        }

        public const string PART_ZapDecorator = "PART_ZapDecorator";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        #region implementation
        private void resetCommands()
        {
            int parentItemsCount = this.ItemCount;

            if (parentItemsCount != _commands.Count)
            {
                if (parentItemsCount > _commands.Count)
                {
                    for (int i = _commands.Count; i < parentItemsCount; i++)
                    {
                        _commands.Add(new ZapCommandItem(this, i));
                    }
                }
                else
                {
                    Debug.Assert(parentItemsCount < _commands.Count);
                    int delta = _commands.Count - parentItemsCount;
                    for (int i = 0; i < delta; i++)
                    {
                        _commands.RemoveAt(_commands.Count - 1);
                    }
                }
            }

            Debug.Assert(ParentItems.Count == _commands.Count);

            //KMoore - 2007-01-08
            //BUGBUG: no clue why I can't use binding here. For somereason item changes are not sent through
            //from the collectionView. Need to talk to smart people about this
            for (int i = 0; i < parentItemsCount; i++)
            {
                _commands[i].Content = ParentItems[i];
            }
            
#if DEBUG
            for (int i = 0; i < _commands.Count; i++)
            {
                Debug.Assert(((ZapCommandItem)_commands[i]).Index == i);
            }
#endif
        }

        private void findZapDecorator()
        {
            if (this.Template != null)
            {
                ZapDecorator temp = this.Template.FindName(PART_ZapDecorator, this) as ZapDecorator;
                if (_zapDecorator != temp)
                {
                    _zapDecorator = temp;
                    if (_zapDecorator != null)
                    {
                        _zapDecorator.Child = _zapPanel;

                        Binding binding = new Binding("CurrentItemIndex");
                        binding.Source = this;
                        _zapDecorator.SetBinding(ZapDecorator.TargetIndexProperty, binding);
                    }
                }
                else
                {
                    Debug.WriteLine("No element with name '" + PART_ZapDecorator + "' in the template.");
                }
            }
            else
            {
                Debug.WriteLine("No template defined for ZapScroller.");
            }
        }

        private void parentItemsChanged()
        {
            ItemCollection newParentItems = (ItemCollection)GetValue(ParentItemsInternalProperty);

            if (newParentItems != ParentItems)
            {
                if (ParentItems != null)
                {
                    INotifyCollectionChanged incc = ParentItems as INotifyCollectionChanged;
                    incc.CollectionChanged -= parentItemsContentChanged;
                }
                if (newParentItems != null)
                {
                    INotifyCollectionChanged incc = newParentItems as INotifyCollectionChanged;
                    incc.CollectionChanged += parentItemsContentChanged;
                }

                ParentItems = newParentItems;

                resetParentItems();
            }
        }

        private void parentItemsContentChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            resetParentItems();
        }

        private void resetParentItems()
        {
            if (ParentItems.Count != this.ItemCount)
            {
                SetValue(ItemCountPropertyKey, ParentItems.Count);
            }
            resetCommands();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == "ItemCount")
            {
                OnPropertyChanged(new PropertyChangedEventArgs("ItemCount"));
            }
        }


        private int _currentItemIndex;
        private ZapPanel _zapPanel;
        private ZapDecorator _zapDecorator;
        private ObservableCollection<ZapCommandItem> _commands = new ObservableCollection<ZapCommandItem>();

        #endregion
    }

    internal class FirstPreviousNextLastCommand : ICommand, INotifyPropertyChanged
    {
        public FirstPreviousNextLastCommand(ZapScroller owner, ZapCommandType type)
        {
            Debug.Assert(owner != null);
            _owner = owner;
            _type = type;

            _owner.PropertyChanged += new PropertyChangedEventHandler(_owner_PropertyChanged);

            resetExecute();
        }

        private void _owner_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentItemIndex" || e.PropertyName == "ItemCount")
            {
                resetExecute();
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public bool CanExecuteProperty
        {
            get
            {
                return _canExecute;
            }
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void Execute(object parameter)
        {
            switch (_type)
            {
                case ZapCommandType.First:
                    _owner.CurrentItemIndex = 0;
                    break;
                case ZapCommandType.Previous:
                    _owner.CurrentItemIndex--;
                    break;
                case ZapCommandType.Next:
                    _owner.CurrentItemIndex++;
                    break;
                case ZapCommandType.Last:
                    _owner.CurrentItemIndex = (_owner.ItemCount - 1);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void resetExecute()
        {
            if (_owner.ItemCount != _previousCount || _owner.CurrentItemIndex != _previousIndex)
            {
                _previousIndex = _owner.CurrentItemIndex;
                _previousCount = _owner.ItemCount;

                bool newExecute;
                switch (_type)
                {
                    case ZapCommandType.First:
                        newExecute = (_previousIndex > 0);
                        break;
                    case ZapCommandType.Previous:
                        newExecute = (_previousIndex > 0);
                        break;
                    case ZapCommandType.Next:
                        newExecute = (_previousIndex < (_previousCount - 1));
                        break;
                    case ZapCommandType.Last:
                        newExecute = (_previousIndex < (_previousCount - 1));
                        break;
                    default:
                        throw new NotSupportedException();
                }

                if (newExecute != _canExecute)
                {
                    _canExecute = newExecute;
                    OnCanExecuteChanged(EventArgs.Empty);
                    OnPropertyChanged(new PropertyChangedEventArgs("CanExecuteProperty"));
                }
            }
        }

        int _previousCount = -1;
        int _previousIndex = -1;

        bool _canExecute;

        ZapScroller _owner;
        ZapCommandType _type;
    }

    internal enum ZapCommandType { First, Previous, Next, Last }

    public class ZapCommandItem : DependencyObject, ICommand, INotifyPropertyChanged
    {
        public ZapCommandItem(ZapScroller zapScroller, int index)
        {
            Debug.Assert(zapScroller != null);
            Debug.Assert(index >= 0);

            _zapScroller = zapScroller;
            _zapScroller.PropertyChanged += new PropertyChangedEventHandler(_zapScroller_PropertyChanged);
            _index = index;

            //KMoore - 2007-01-08
            //BUGBUG: no clue why I can't use binding here. For some reason item changes are not sent through
            //from the collectionView. Need to talk to smart people about this
            Content = _zapScroller.ParentItems[_index];       
        }

        private static readonly DependencyPropertyKey ContentPropertyKey = DependencyProperty.RegisterReadOnly("Content", typeof(object),
            typeof(ZapCommandItem), new PropertyMetadata());

        public static readonly DependencyProperty ContentProperty = ContentPropertyKey.DependencyProperty;

        public object Content
        {
            get { return GetValue(ContentProperty); }
            internal set { SetValue(ContentPropertyKey, value); }
        }


        public int Index { get { return _index; } }

        /// <summary>
        /// For public use. Most people don't like zero-base indices.
        /// </summary>
        public int Number { get { return _index + 1; } }


        public bool CanExecuteProperty
        {
            get
            {
                _lastCanExecute = canExecute();
                return _lastCanExecute;
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteProperty;
        }

        private bool canExecute()
        {
            return (_index != _zapScroller.CurrentItemIndex);
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("CanExecuteProperty"));

            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _zapScroller.CurrentItemIndex = _index;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public override string ToString()
        {
            return string.Format("ZapCommandItem - Index: {0}, Content: {1}", _index, Content.ToString());
        }

        private void _zapScroller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentItemIndex")
            {
                if (_lastCanExecute != canExecute())
                {
                    OnCanExecuteChanged(EventArgs.Empty);
                }
            }
        }

        private int _index;
        private ZapScroller _zapScroller;
        private bool _lastCanExecute;
    }
}
