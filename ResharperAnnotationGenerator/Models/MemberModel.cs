using System.Windows;

namespace ResharperAnnotationGenerator.Models
{
    public class MemberModel : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            nameof(Name), typeof (string), typeof (MemberModel), new PropertyMetadata(default(string)));

        public string Name
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
    }
}