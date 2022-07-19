using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ParserApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            IXmlParser parser;
            IFormatReporter reporter;
            List<ModelItem> items;

            //might have been done in a much more flexible way using bindings
            string checkedParser = StackPanelParsing.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value).Content.ToString();
            switch (checkedParser)
            {
                case "Model":
                    parser = new XmlModelParser();
                    break;
                default:
                    parser = new XmlModelParser();
                    break;
            }

            string path = PathConstructor.BuildPath("Resources", "data.xml");

            try
            {
                items = parser.ParseXmlData(path);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("You have to place data.xml inside Resources folder along with other project source files",
                    "Source file Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (XmlException xmlException)
            {
                MessageBox.Show("Something's wrong with your xml file: " + xmlException.Message,
                    "XML Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string checkedFormat = StackPanelFormats.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value).Content.ToString();
            switch (checkedFormat)
            {
                case "docx":
                    reporter = new DocxReporter();
                    break;
                case "xlsx":
                    reporter = new XlsxReporter();
                    break;
                case "txt":
                    reporter = new TxtReporter();
                    break;
                default:
                    reporter = new DocxReporter();
                    break;
            }

            reporter.CreateReport(items);
        }
    }
}
