using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPFControls.Layout.Controls
{
    [TemplateVisualState(Name = VisualStates.StateVisible, GroupName = VisualStates.GroupVisibility)]
    [TemplateVisualState(Name = VisualStates.StateHidden, GroupName = VisualStates.GroupVisibility)]
    [StyleTypedProperty(Property = "OverlayStyle", StyleTargetType = typeof(Rectangle))]
    public class DialogContainer : ContentControl
    {
        public DialogContainer()
        {
            DefaultStyleKey = typeof(DialogContainer);

            Loaded += new RoutedEventHandler(DialogContainer_Loaded);
        }

        void DialogContainer_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeVisualState(true);
        }

        protected bool IsContentVisible { get; set; }

        protected virtual void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsShowing ? VisualStates.StateVisible : VisualStates.StateHidden, useTransitions);
            VisualStateManager.GoToState(this, IsContentVisible ? VisualStates.StateVisible : VisualStates.StateHidden, useTransitions);
        }

        public bool IsShowing
        {
            get { return (bool)GetValue(IsShowingProperty); }
            set { SetValue(IsShowingProperty, value); }
        }

        public static readonly DependencyProperty IsShowingProperty = DependencyProperty.Register(
            "IsShowing",
            typeof(bool),
            typeof(DialogContainer),
            new PropertyMetadata(false, new PropertyChangedCallback(OnIsShowingChanged)));

        private static void OnIsShowingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialogContainer)d).OnIsShowingChanged(e);
        }

        protected virtual void OnIsShowingChanged(DependencyPropertyChangedEventArgs e)
        {
            if (IsShowing)
            {
                // Go visible now
                IsContentVisible = true;

            }
            else
            {
                // No longer visible
                IsContentVisible = false;
            }
            ChangeVisualState(true);
        }


        public object DialogContent
        {
            get { return (object)GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }

        public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register(
            "DialogContent",
            typeof(object),
            typeof(DialogContainer),
            new PropertyMetadata(null));

        public DataTemplate DialogContentTemplate
        {
            get { return (DataTemplate)GetValue(DialogContentTemplateProperty); }
            set { SetValue(DialogContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty DialogContentTemplateProperty = DependencyProperty.Register(
            "DialogContentTemplate",
            typeof(DataTemplate),
            typeof(DialogContainer),
            new PropertyMetadata(null));

        public Style OverlayStyle
        {
            get { return (Style)GetValue(OverlayStyleProperty); }
            set { SetValue(OverlayStyleProperty, value); }
        }

        public static readonly DependencyProperty OverlayStyleProperty = DependencyProperty.Register(
            "OverlayStyle",
            typeof(Style),
            typeof(DialogContainer),
            new PropertyMetadata(null));

    }
}
