﻿#pragma checksum "..\..\..\..\Pages\Agent\Page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7383AF880AC0B9E976BAD705424B1807"
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
using TH_DeviceManager.Controls;
using TH_WPF;
using TH_WPF.LoadingAnimation;


namespace TH_DeviceManager.Pages.Agent {
    
    
    /// <summary>
    /// Page
    /// </summary>
    public partial class Page : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 82 "..\..\..\..\Pages\Agent\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ipaddress_TXT;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\Pages\Agent\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox port_TXT;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\..\Pages\Agent\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox devicename_TXT;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\..\..\Pages\Agent\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox currentHeartbeat_TXT;
        
        #line default
        #line hidden
        
        
        #line 312 "..\..\..\..\Pages\Agent\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sampleHeartbeat_TXT;
        
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
            System.Uri resourceLocater = new System.Uri("/TH_DeviceManager;component/pages/agent/page.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Agent\Page.xaml"
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
            this.ipaddress_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 82 "..\..\..\..\Pages\Agent\Page.xaml"
            this.ipaddress_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ipaddress_TXT_TextChanged);
            
            #line default
            #line hidden
            
            #line 82 "..\..\..\..\Pages\Agent\Page.xaml"
            this.ipaddress_TXT.LostFocus += new System.Windows.RoutedEventHandler(this.ipaddress_TXT_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.port_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 134 "..\..\..\..\Pages\Agent\Page.xaml"
            this.port_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.port_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.devicename_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 186 "..\..\..\..\Pages\Agent\Page.xaml"
            this.devicename_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.devicename_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 216 "..\..\..\..\Pages\Agent\Page.xaml"
            ((System.Windows.Controls.Slider)(target)).ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.CurrentSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.currentHeartbeat_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 249 "..\..\..\..\Pages\Agent\Page.xaml"
            this.currentHeartbeat_TXT.LostFocus += new System.Windows.RoutedEventHandler(this.currentHeartbeat_TXT_LostFocus);
            
            #line default
            #line hidden
            
            #line 249 "..\..\..\..\Pages\Agent\Page.xaml"
            this.currentHeartbeat_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.currentHeartbeat_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 279 "..\..\..\..\Pages\Agent\Page.xaml"
            ((System.Windows.Controls.Slider)(target)).ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SampleSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.sampleHeartbeat_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 312 "..\..\..\..\Pages\Agent\Page.xaml"
            this.sampleHeartbeat_TXT.LostFocus += new System.Windows.RoutedEventHandler(this.sampleHeartbeat_TXT_LostFocus);
            
            #line default
            #line hidden
            
            #line 312 "..\..\..\..\Pages\Agent\Page.xaml"
            this.sampleHeartbeat_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.sampleHeartbeat_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 332 "..\..\..\..\Pages\Agent\Page.xaml"
            ((TH_WPF.Button_01)(target)).Clicked += new TH_WPF.Button_01.Clicked_Handler(this.TestConnection_Clicked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

