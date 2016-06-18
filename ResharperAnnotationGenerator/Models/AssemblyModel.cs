using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ResharperAnnotationGenerator.Models
{
    public class AssemblyModel : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            "Name", typeof (string), typeof (AssemblyModel), new PropertyMetadata(default(string)));

        public string Name
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty MembersProperty = DependencyProperty.Register(
            "Members", typeof (IList<MemberModel>), typeof (AssemblyModel), new PropertyMetadata(default(IList<MemberModel>)));

        public IList<MemberModel> Members
        {
            get { return (IList<MemberModel>) GetValue(MembersProperty); }
            set { SetValue(MembersProperty, value); }
        }
    }
}