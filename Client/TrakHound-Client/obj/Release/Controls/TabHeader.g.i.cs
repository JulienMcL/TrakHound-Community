﻿#pragma checksum "..\..\..\Controls\TabHeader.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8C5226474EBB8AB56103B175967FB702"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using TH_WPF;
using TrakHound_Client.Controls;


namespace TrakHound_Client.Controls {
    
    
    /// <summary>
    /// TabHeader
    /// </summary>
    public partial class TabHeader : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\Controls\TabHeader.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border root;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\Controls\TabHeader.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle content_image;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\Controls\TabHeader.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\Controls\TabHeader.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border close_BD;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TrakHound-Client;component/controls/tabheader.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\TabHeader.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 15 "..\..\..\Controls\TabHeader.xaml"
            ((TrakHound_Client.Controls.TabHeader)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.UserControl_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.root = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            
            #line 24 "..\..\..\Controls\TabHeader.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Control_MouseEnter);
            
            #line default
            #line hidden
            
            #line 24 "..\..\..\Controls\TabHeader.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Control_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 111 "..\..\..\Controls\TabHeader.xaml"
            ((System.Windows.Controls.Grid)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Control_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.content_image = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.txt = ((System.Windows.Controls.TextBlock)(target));
            
            #line 150 "..\..\..\Controls\TabHeader.xaml"
            this.txt.SizeChanged += new System.Windows.SizeChangedEventHandler(this.TextBlock_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.close_BD = ((System.Windows.Controls.Border)(target));
            
            #line 170 "..\..\..\Controls\TabHeader.xaml"
            this.close_BD.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TabItemClose_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
