﻿#pragma checksum "..\..\..\..\DialogWindow\CreateNameWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "098647DDF8002DD7F719D427A4371B9646BEACE2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ClassLibrary1.DialogWindow {
    
    
    /// <summary>
    /// CreateNameWindow
    /// </summary>
    public partial class CreateNameWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxNewName;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LabelErrorWrite;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonApply;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClassLibrary1;component/dialogwindow/createnamewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
            ((ClassLibrary1.DialogWindow.CreateNameWindow)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TextBoxNewName = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
            this.TextBoxNewName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBoxNewName_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LabelErrorWrite = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.ButtonApply = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
            this.ButtonApply.Click += new System.Windows.RoutedEventHandler(this.ButtonApply_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonCancel = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\..\..\DialogWindow\CreateNameWindow.xaml"
            this.ButtonCancel.Click += new System.Windows.RoutedEventHandler(this.ButtonCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

