using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapilarMask.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapilarMask.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPage : ContentPage
    {
        StackLayout STACKLAYOUT;
        StackLayout LAYOUT_DESCRICAO;
        ScrollView SCROLL_VIEW;
        Picker TratamentoCurrente;
        Label DESCRICAO_TRATAMENTO;
        Editor TITULO;
        string descricao_tratamento="Descrição por defeito";
        
        public NoteEntryPage()
        {
            InitializeComponent();
            STACKLAYOUT = new StackLayout();                                  // inicializacao layout estatico
            LAYOUT_DESCRICAO = new StackLayout();
            SCROLL_VIEW = new ScrollView();                                   // inicializacao da Scroll View
            SCROLL_VIEW.Content = STACKLAYOUT;
            Content = SCROLL_VIEW;
            DESCRICAO_TRATAMENTO = new Label { Text = descricao_tratamento, FontSize = 15};
            TITULO = new Editor {  FontSize = 20};
            TITULO.IsReadOnly = true;
            TITULO.FontAttributes = FontAttributes.Bold;
            TITULO.TextColor = Color.Purple;

            TratamentoCurrente = new Picker { Title = "Tratamento Atual"};
            TratamentoCurrente.Items.Add("Hidratação 1");
            TratamentoCurrente.Items.Add("Nutrição 1");
            TratamentoCurrente.Items.Add("Hidratação 2");

            TratamentoCurrente.Items.Add("Hidratação 3");
            TratamentoCurrente.Items.Add("Hidratação 4");
            TratamentoCurrente.Items.Add("Nutrição 2");

            TratamentoCurrente.Items.Add("Hidratação 5");
            TratamentoCurrente.Items.Add("Nutrição 3");
            TratamentoCurrente.Items.Add("Hidratação 6");

            TratamentoCurrente.Items.Add("Hidratação 7");
            TratamentoCurrente.Items.Add("Nutrição 4");
            TratamentoCurrente.Items.Add("Reconstrução");

            TratamentoCurrente.SelectedIndexChanged += handle_tratamento_diferente;
            TratamentoCurrente.SetBinding(Picker.SelectedIndexProperty, "Tratamento_selecionado");
            TITULO.SetBinding(Editor.TextProperty, "Fase");

            descricao_tratamento = obterIngredientes(TratamentoCurrente.SelectedIndex.ToString());
           
            STACKLAYOUT.Margin = new Thickness(20);
            showOptions();

        }

        private void handle_tratamento_diferente(object sender, EventArgs e)
        {

            TITULO.Text = (string) TratamentoCurrente.SelectedItem;
          
            DESCRICAO_TRATAMENTO.Text = obterIngredientes(TratamentoCurrente.SelectedItem.ToString());

           
            
        }

        private void showOptions()
        {
            
            STACKLAYOUT.Children.Add(TITULO);
            STACKLAYOUT.Children.Add(TratamentoCurrente);
            STACKLAYOUT.Children.Add(new Label { Text = "\n"});
            STACKLAYOUT.Children.Add(LAYOUT_DESCRICAO);
            STACKLAYOUT.Children.Add(new Label { Text = "\n" });
            STACKLAYOUT.Children.Add(new Label { Text = "Notas:", FontSize =20, FontAttributes = FontAttributes.Bold, TextColor = Color.Purple });
            Editor editor = new Editor
            {
                Placeholder = "Notas",
                HeightRequest = 160,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Editor)),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            editor.SetBinding(Editor.TextProperty, "Text");
            STACKLAYOUT.Children.Add(editor);
            Grid grid1 = new Grid { Margin = new Thickness(0) };
            Button save = new Button { Text = "Guardar", BackgroundColor = Color.White,BorderColor = Color.Purple, TextColor = Color.Purple };
            Button delete = new Button { Text = "Apagar", BackgroundColor = Color.White, BorderColor = Color.Purple, TextColor = Color.Purple };

            save.Clicked += OnSaveButtonClicked;
            delete.Clicked += OnDeleteButtonClicked;

            grid1.Children.Add(save, 0, 0);
            grid1.Children.Add(delete, 1, 0);
            STACKLAYOUT.Children.Add(grid1);

            LAYOUT_DESCRICAO.Children.Add(new Label { Text = "Ingredientes:", FontSize = 20,FontAttributes = FontAttributes.Bold, TextColor = Color.Purple });
            LAYOUT_DESCRICAO.Children.Add(DESCRICAO_TRATAMENTO);

        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            await App.Database.SaveNoteAsync(note);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(note);
            await Navigation.PopAsync();
        }
        public string obterIngredientes(string tratamento)
        {
            if (tratamento.Contains("Hidratação"))
            {
                return "Aloe Vera; Pantenol; Sacarose e derivados de Açúcar;" +
                    "Vitaminas a, b, C e D; Extratos de plantas e frutas; Cetyl e Alcohol;" +
                    "Cetearyl Alcohol; Glicerina; Proteína da Aveia; Proteína da Seda;" +
                    "GeleiaReal; Chocolate. ";
            }
            else if (tratamento.Contains("Nutrição"))
            {
                return "Manteiga de karité; Manteiga de Cacau; Óleo de Abacate; Azeite;" +
                    "Óleo de Côco; Óleo de Argão; Óleo de Amendoas doces; Óleo de Tutano;" +
                    "Ceramidas.";
            }
            return "Prolina; Queratina; Creatina; Arginina; Cisteina; Colagénio;" +
                "Proteínas e aminoácidos do Trigo.";
        }
    }
}