#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012-2020 Josué Clément
//mod6991@gmail.com

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DotNetToolBox.MVVM
{
    public class DefaultCommands
    {
        public static readonly DependencyProperty LoadedEventCommandProperty                        = RoutedEventToCommandManager.CreateCommandForRoutedEvent(FrameworkElement.LoadedEvent, "LoadedEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty UnloadedEventCommandProperty                      = RoutedEventToCommandManager.CreateCommandForRoutedEvent(FrameworkElement.UnloadedEvent, "UnloadedEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty SelectionChangedEventCommandProperty              = RoutedEventToCommandManager.CreateCommandForRoutedEvent(Selector.SelectionChangedEvent, "SelectionChangedEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseDoubleClickCommandProperty                   = RoutedEventToCommandManager.CreateCommandForRoutedEvent(Control.MouseDoubleClickEvent, "MouseDoubleClickEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty GotFocusEventCommandProperty                      = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.GotFocusEvent, "GotFocusEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty ContextMenuClosingEventCommandProperty            = RoutedEventToCommandManager.CreateCommandForRoutedEvent(FrameworkElement.ContextMenuClosingEvent, "ContextMenuClosingEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty ContextMenuOpeningEventCommandProperty            = RoutedEventToCommandManager.CreateCommandForRoutedEvent(FrameworkElement.ContextMenuOpeningEvent, "ContextMenuOpeningEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty SizeChangedEventCommandProperty                   = RoutedEventToCommandManager.CreateCommandForRoutedEvent(FrameworkElement.SizeChangedEvent, "SizeChangedEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty ToolTipClosingEventCommandProperty                = RoutedEventToCommandManager.CreateCommandForRoutedEvent(FrameworkElement.ToolTipClosingEvent, "ToolTipClosingEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty ToolTipOpeningEventCommandProperty                = RoutedEventToCommandManager.CreateCommandForRoutedEvent(FrameworkElement.ToolTipOpeningEvent, "ToolTipOpeningEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty KeyDownEventCommandProperty                       = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.KeyDownEvent, "KeyDownEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty KeyUpEventCommandProperty                         = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.KeyUpEvent, "KeyUpEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty LostFocusEventCommandProperty                     = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.LostFocusEvent, "LostFocusEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseDownEventCommandProperty                     = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.MouseDownEvent, "MouseDownEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseLeftButtonDownEventCommandProperty           = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.MouseLeftButtonDownEvent, "MouseLeftButtonDownEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseLeftButtonUpEventCommandProperty             = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.MouseLeftButtonUpEvent, "MouseLeftButtonUpEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseMoveEventCommandProperty                     = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.MouseMoveEvent, "MouseMoveEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseRightButtonDownEventCommandProperty          = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.MouseRightButtonDownEvent, "MouseRightButtonDownEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseRightButtonUpEventCommandProperty            = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.MouseRightButtonUpEvent, "MouseRightButtonUpEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseUpEventCommandProperty                       = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.MouseUpEvent, "MouseUpEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty MouseWheelEventCommandProperty                    = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.MouseWheelEvent, "MouseWheelEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty DragEnterEventCommandProperty                     = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.DragEnterEvent, "DragEnterEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty DragLeaveEventCommandProperty                     = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.DragLeaveEvent, "DragLeaveEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty DragOverEventCommandProperty                      = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.DragOverEvent, "DragOverEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty DropEventCommandProperty                          = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.DropEvent, "DropEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewKeyDownEventCommandProperty                = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewKeyDownEvent, "PreviewKeyDownEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewKeyUpEventCommandProperty                  = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewKeyUpEvent, "PreviewKeyUpEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewMouseDownEventCommandProperty              = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewMouseDownEvent, "PreviewMouseDownEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewMouseLeftButtonDownEventCommandProperty    = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewMouseLeftButtonDownEvent, "PreviewMouseLeftButtonDownEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewMouseLeftButtonUpEventCommandProperty      = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewMouseLeftButtonUpEvent, "PreviewMouseLeftButtonUpEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewMouseMoveEventCommandProperty              = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewMouseMoveEvent, "PreviewMouseMoveEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewMouseRightButtonDownEventCommandProperty   = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewMouseRightButtonDownEvent, "PreviewMouseRightButtonDownEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewMouseRightButtonUpEventCommandProperty     = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewMouseRightButtonUpEvent, "PreviewMouseRightButtonUpEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewMouseUpEventCommandProperty                = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewMouseUpEvent, "PreviewMouseUpEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewMouseWheelEventCommandProperty             = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewMouseWheelEvent, "PreviewMouseWheelEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewDragEnterEventCommandProperty              = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewDragEnterEvent, "PreviewDragEnterEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewDragLeaveEventCommandProperty              = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewDragLeaveEvent, "PreviewDragLeaveEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewDragOverEventCommandProperty               = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewDragOverEvent, "PreviewDragOverEventCommand", typeof(DefaultCommands));
        public static readonly DependencyProperty PreviewDropEventCommandProperty                   = RoutedEventToCommandManager.CreateCommandForRoutedEvent(UIElement.PreviewDropEvent, "PreviewDropEventCommand", typeof(DefaultCommands));

        public static ICommand GetLoadedEventCommand(DependencyObject o)
        {
            return o.GetValue(LoadedEventCommandProperty) as ICommand;
        }

        public static void SetLoadedEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(LoadedEventCommandProperty, value);
        }

        public static ICommand GetUnloadedEventCommand(DependencyObject o)
        {
            return o.GetValue(UnloadedEventCommandProperty) as ICommand;
        }

        public static void SetUnloadedEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(UnloadedEventCommandProperty, value);
        }

        public static ICommand GetSelectionChangedEventCommand(DependencyObject o)
        {
            return o.GetValue(SelectionChangedEventCommandProperty) as ICommand;
        }

        public static void SetSelectionChangedEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(SelectionChangedEventCommandProperty, value);
        }

        public static ICommand GetMouseDoubleClickEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseDoubleClickCommandProperty) as ICommand;
        }

        public static void SetMouseDoubleClickEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseDoubleClickCommandProperty, value);
        }

        public static ICommand GetGotFocusEventCommand(DependencyObject o)
        {
            return o.GetValue(GotFocusEventCommandProperty) as ICommand;
        }

        public static void SetGotFocusEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(GotFocusEventCommandProperty, value);
        }

        public static ICommand GetContextMenuClosingEventCommand(DependencyObject o)
        {
            return o.GetValue(ContextMenuClosingEventCommandProperty) as ICommand;
        }

        public static void SetContextMenuClosingEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(ContextMenuClosingEventCommandProperty, value);
        }

        public static ICommand GetContextMenuOpeningEventCommand(DependencyObject o)
        {
            return o.GetValue(ContextMenuOpeningEventCommandProperty) as ICommand;
        }

        public static void SetContextMenuOpeningEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(ContextMenuOpeningEventCommandProperty, value);
        }

        public static ICommand GetSizeChangedEventCommand(DependencyObject o)
        {
            return o.GetValue(SizeChangedEventCommandProperty) as ICommand;
        }

        public static void SetSizeChangedEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(SizeChangedEventCommandProperty, value);
        }

        public static ICommand GetToolTipClosingEventCommand(DependencyObject o)
        {
            return o.GetValue(ToolTipClosingEventCommandProperty) as ICommand;
        }

        public static void SetToolTipClosingEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(ToolTipClosingEventCommandProperty, value);
        }

        public static ICommand GetToolTipOpeningEventCommand(DependencyObject o)
        {
            return o.GetValue(ToolTipOpeningEventCommandProperty) as ICommand;
        }

        public static void SetToolTipOpeningEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(ToolTipOpeningEventCommandProperty, value);
        }

        public static ICommand GetKeyDownEventCommand(DependencyObject o)
        {
            return o.GetValue(KeyDownEventCommandProperty) as ICommand;
        }

        public static void SetKeyDownEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(KeyDownEventCommandProperty, value);
        }

        public static ICommand GetKeyUpEventCommand(DependencyObject o)
        {
            return o.GetValue(KeyUpEventCommandProperty) as ICommand;
        }

        public static void SetKeyUpEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(KeyUpEventCommandProperty, value);
        }

        public static ICommand GetLostFocusEventCommand(DependencyObject o)
        {
            return o.GetValue(LostFocusEventCommandProperty) as ICommand;
        }

        public static void SetLostFocusEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(LostFocusEventCommandProperty, value);
        }

        public static ICommand GetMouseDownEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseDownEventCommandProperty) as ICommand;
        }

        public static void SetMouseDownEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseDownEventCommandProperty, value);
        }

        public static ICommand GetMouseLeftButtonDownEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseLeftButtonDownEventCommandProperty) as ICommand;
        }

        public static void SetMouseLeftButtonDownEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseLeftButtonDownEventCommandProperty, value);
        }

        public static ICommand GetMouseLeftButtonUpEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseLeftButtonUpEventCommandProperty) as ICommand;
        }

        public static void SetMouseLeftButtonUpEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseLeftButtonUpEventCommandProperty, value);
        }

        public static ICommand GetMouseMoveEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseMoveEventCommandProperty) as ICommand;
        }

        public static void SetMouseMoveEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseMoveEventCommandProperty, value);
        }

        public static ICommand GetMouseRightButtonDownEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseRightButtonDownEventCommandProperty) as ICommand;
        }

        public static void SetMouseRightButtonDownEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseRightButtonDownEventCommandProperty, value);
        }

        public static ICommand GetMouseRightButtonUpEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseRightButtonUpEventCommandProperty) as ICommand;
        }

        public static void SetMouseRightButtonUpEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseRightButtonUpEventCommandProperty, value);
        }

        public static ICommand GetMouseUpEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseUpEventCommandProperty) as ICommand;
        }

        public static void SetMouseUpEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseUpEventCommandProperty, value);
        }

        public static ICommand GetMouseWheelEventCommand(DependencyObject o)
        {
            return o.GetValue(MouseWheelEventCommandProperty) as ICommand;
        }

        public static void SetMouseWheelEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(MouseWheelEventCommandProperty, value);
        }

        public static ICommand GetDragEnterEventCommand(DependencyObject o)
        {
            return o.GetValue(DragEnterEventCommandProperty) as ICommand;
        }

        public static void SetDragEnterEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(DragEnterEventCommandProperty, value);
        }

        public static ICommand GetDragLeaveEventCommand(DependencyObject o)
        {
            return o.GetValue(DragLeaveEventCommandProperty) as ICommand;
        }

        public static void SetDragLeaveEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(DragLeaveEventCommandProperty, value);
        }

        public static ICommand GetDragOverEventCommand(DependencyObject o)
        {
            return o.GetValue(DragOverEventCommandProperty) as ICommand;
        }

        public static void SetDragOverEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(DragOverEventCommandProperty, value);
        }

        public static ICommand GetDropEventCommand(DependencyObject o)
        {
            return o.GetValue(DropEventCommandProperty) as ICommand;
        }

        public static void SetDropEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(DropEventCommandProperty, value);
        }

        public static ICommand GetPreviewKeyDownEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewKeyDownEventCommandProperty) as ICommand;
        }

        public static void SetPreviewKeyDownEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewKeyDownEventCommandProperty, value);
        }

        public static ICommand GetPreviewKeyUpEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewKeyUpEventCommandProperty) as ICommand;
        }

        public static void SetPreviewKeyUpEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewKeyUpEventCommandProperty, value);
        }

        public static ICommand GetPreviewMouseDownEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewMouseDownEventCommandProperty) as ICommand;
        }

        public static void SetPreviewMouseDownEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewMouseDownEventCommandProperty, value);
        }

        public static ICommand GetPreviewMouseLeftButtonDownEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewMouseLeftButtonDownEventCommandProperty) as ICommand;
        }

        public static void SetPreviewMouseLeftButtonDownEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewMouseLeftButtonDownEventCommandProperty, value);
        }

        public static ICommand GetPreviewMouseLeftButtonUpEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewMouseLeftButtonUpEventCommandProperty) as ICommand;
        }

        public static void SetPreviewMouseLeftButtonUpEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewMouseLeftButtonUpEventCommandProperty, value);
        }

        public static ICommand GetPreviewMouseMoveEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewMouseMoveEventCommandProperty) as ICommand;
        }

        public static void SetPreviewMouseMoveEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewMouseMoveEventCommandProperty, value);
        }

        public static ICommand GetPreviewMouseRightButtonDownEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewMouseRightButtonDownEventCommandProperty) as ICommand;
        }

        public static void SetPreviewMouseRightButtonDownEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewMouseRightButtonDownEventCommandProperty, value);
        }

        public static ICommand GetPreviewMouseRightButtonUpEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewMouseRightButtonUpEventCommandProperty) as ICommand;
        }

        public static void SetPreviewMouseRightButtonUpEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewMouseRightButtonUpEventCommandProperty, value);
        }

        public static ICommand GetPreviewMouseUpEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewMouseUpEventCommandProperty) as ICommand;
        }

        public static void SetPreviewMouseUpEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewMouseUpEventCommandProperty, value);
        }

        public static ICommand GetPreviewMouseWheelEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewMouseWheelEventCommandProperty) as ICommand;
        }

        public static void SetPreviewMouseWheelEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewMouseWheelEventCommandProperty, value);
        }

        public static ICommand GetPreviewDragEnterEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewDragEnterEventCommandProperty) as ICommand;
        }

        public static void SetPreviewDragEnterEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewDragEnterEventCommandProperty, value);
        }

        public static ICommand GetPreviewDragLeaveEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewDragLeaveEventCommandProperty) as ICommand;
        }

        public static void SetPreviewDragLeaveEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewDragLeaveEventCommandProperty, value);
        }

        public static ICommand GetPreviewDragOverEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewDragOverEventCommandProperty) as ICommand;
        }

        public static void SetPreviewDragOverEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewDragOverEventCommandProperty, value);
        }

        public static ICommand GetPreviewDropEventCommand(DependencyObject o)
        {
            return o.GetValue(PreviewDropEventCommandProperty) as ICommand;
        }

        public static void SetPreviewDropEventCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(PreviewDropEventCommandProperty, value);
        }
    }
}
