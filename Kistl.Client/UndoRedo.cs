using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Controls;

namespace Kistl.Client
{
    class UndoManager
    {

        Stack<UndoOperation> undoStack = new Stack<UndoOperation>();
        Stack<UndoOperation> redoStack = new Stack<UndoOperation>();

        #region SharedUndoScope

        public static UndoManager GetSharedUndoScope(DependencyObject obj)
        {
            return (UndoManager)obj.GetValue(SharedUndoScopeProperty);
        }

        public static void SetSharedUndoScope(DependencyObject obj, UndoManager value)
        {
            obj.SetValue(SharedUndoScopeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SharedUndoScope.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SharedUndoScopeProperty =
            DependencyProperty.RegisterAttached("SharedUndoScope", typeof(UndoManager), typeof(UndoManager), 
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnUseGlobalUndoScopeChanged)));

        private static void OnUseGlobalUndoScopeChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            if (depObj is TextBoxBase)
            {
                if (args.OldValue != null)
                {
                    RemoveEventHandlers(depObj as TextBoxBase, args.OldValue as UndoManager);
                }
                if (args.NewValue != null)
                {
                    AttachEventHandlers(depObj as TextBoxBase, args.NewValue as UndoManager);
                }
            }
        }

        private static void AttachEventHandlers(TextBoxBase textBox, UndoManager manager)
        {
            if (textBox != null && manager != null)
            {
                textBox.AddHandler(CommandManager.PreviewExecutedEvent, new ExecutedRoutedEventHandler(manager.ExecutedHandler), true); // we need to see all events to subvert the built-in undo/redo tracking in the text boxes
                textBox.TextChanged += new TextChangedEventHandler(manager.TextChangedHandler);
            }
        }

        private static void RemoveEventHandlers(TextBoxBase textBox, UndoManager manager)
        {
            if (textBox != null && manager != null)
            {
                textBox.RemoveHandler(CommandManager.PreviewExecutedEvent, new ExecutedRoutedEventHandler(manager.ExecutedHandler));
                textBox.TextChanged -= new TextChangedEventHandler(manager.TextChangedHandler);
            }
        }
        #endregion

        void TextChangedHandler(object sender, TextChangedEventArgs e)
        {
            this.AddUndoableAction(sender as TextBoxBase, e.UndoAction);
        }

        private void ExecutedHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Undo)
            {
                e.Handled = true;
                Undo();
            }

            if (e.Command == ApplicationCommands.Redo)
            {
                e.Handled = true;
                Redo();
            }
        }

        private void AddUndoableAction(TextBoxBase sender, UndoAction action)
        {
            if (action == UndoAction.Undo)
            {
                redoStack.Push(new UndoOperation(sender, action));
            }
            else
            {
                if (undoStack.Count > 0)
                {
                    UndoOperation op = undoStack.Peek();
                    if ((op.Sender == sender) && (action == UndoAction.Merge))
                    {
                        // no-op
                    }
                    else
                    {
                        PushUndoOperation(sender, action);
                    }
                }
                else
                {
                    PushUndoOperation(sender, action);
                }
            }
        }

        private void PushUndoOperation(TextBoxBase sender, UndoAction action)
        {
            undoStack.Push(new UndoOperation(sender, action));
            System.Diagnostics.Debug.WriteLine("PUSHED");
        }

        public void Undo()
        {

            if (undoStack.Count > 0)
            {
                UndoOperation op = undoStack.Pop();
                op.Sender.Undo();
                op.Sender.Focus();
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                UndoOperation op = redoStack.Pop();
                op.Sender.Redo();
                op.Sender.Focus();
            }
        }
    }

    public class UndoOperation
    {
        public TextBoxBase Sender;
        public UndoAction Action;

        public UndoOperation(TextBoxBase sender, UndoAction action)
        {
            this.Sender = sender;
            this.Action = action;
        }
    }
}
