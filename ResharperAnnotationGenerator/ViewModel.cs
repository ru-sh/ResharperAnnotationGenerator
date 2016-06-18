using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ResharperAnnotationGenerator.Models;

namespace ResharperAnnotationGenerator
{
    public class ViewModel : DependencyObject
    {
        public static readonly DependencyProperty AssemblyModelProperty = DependencyProperty.Register(
            "AssemblyModel", typeof(AssemblyModel), typeof(ViewModel), new PropertyMetadata(default(AssemblyModel)));

        public AssemblyModel AssemblyModel
        {
            get { return (AssemblyModel)GetValue(AssemblyModelProperty); }
            set { SetValue(AssemblyModelProperty, value); }
        }

        public ViewModel()
        {
            AssemblyModel = new AssemblyModel()
            {
                Name = "Test",
                Members = new List<MemberModel>() { { new MemberModel() { Name = "Foo" } } }
            };

            OpenFileCommand = new RelayCommand(x =>
            {
                var openFileDialog = new OpenFileDialog()
                {
                    CheckFileExists = true,
                    Filter = "Assembly Files (*.dll)|*.dll"
                };

                if (openFileDialog.ShowDialog() != true)
                {
                    return;
                }

                var assembly = Assembly.LoadFrom(openFileDialog.FileName);
                AssemblyModel = new AssemblyModel()
                {
                    Name = assembly.GetName().Name,
                    Members = assembly.GetTypes()
                        .Select(type => new {type, method = type.GetMethods()})
                        .SelectMany(
                            arg => arg.method.Select(m => new MemberModel() {Name = arg.type.FullName + m.Name}))
                        .ToList()
                };
            });
        }

        public static readonly DependencyProperty OpenFileCommandProperty = DependencyProperty.Register(
            nameof(OpenFileCommand), typeof (ICommand), typeof (ViewModel), new PropertyMetadata(default(ICommand)));

        public ICommand OpenFileCommand
        {
            get { return (ICommand) GetValue(OpenFileCommandProperty); }
            set { SetValue(OpenFileCommandProperty, value); }
        }
    }
}