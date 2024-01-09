using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Abonados_del_betis;

public partial class MessageBox : Window
{
    public MessageBox()
    {
        InitializeComponent();
        mensaje.Text = "texto";   
    }
}