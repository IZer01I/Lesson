﻿#pragma checksum "..\..\..\View\QuestionaryEditwindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "78AE30E730EC451ADB1AAA69E23E58ECE7936AB08BA4BDE46FA2E359403DD86A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Job_vacancy_app.View;
using Job_vacancy_app.ViewModel;
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


namespace Job_vacancy_app.View {
    
    
    /// <summary>
    /// QuestionaryEditwindow
    /// </summary>
    public partial class QuestionaryEditwindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 66 "..\..\..\View\QuestionaryEditwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label VacancyValue;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\View\QuestionaryEditwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ExpTb;
        
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
            System.Uri resourceLocater = new System.Uri("/Job_vacancy_app;component/view/questionaryeditwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\QuestionaryEditwindow.xaml"
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
            this.VacancyValue = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            
            #line 85 "..\..\..\View\QuestionaryEditwindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ExpTb_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ExpTb = ((System.Windows.Controls.TextBox)(target));
            
            #line 124 "..\..\..\View\QuestionaryEditwindow.xaml"
            this.ExpTb.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ExpTb_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
