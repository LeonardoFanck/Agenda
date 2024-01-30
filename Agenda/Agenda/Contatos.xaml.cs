using Agenda.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contatos : TabbedPage
    {
        public Contatos()
        {
            InitializeComponent();

            BindingContext = this;
            CarregaLista();
        }

        private async void CarregaLista()
        {
            var lista = new Servico();
            var listagem = lista.Lista();

            CvLista.ItemsSource = await listagem;
        }

        private async void TxtCep_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                var cliente = new HttpClient();
                var cep = TxtCep.Text;
                var json = await cliente.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
                var dados = JsonConvert.DeserializeObject<Endereco>(json);

                TxtLogradouro.Text = dados.logradouro;
                TxtBairro.Text = dados.bairro;
                TxtCidade.Text = dados.localidade;
                TxtUf.Text = dados.uf;

                TxtNumero.Focus();
            }
            catch
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao tentar localizar o CEP", "OK");
                return;
            }
        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var salvar = new Servico();
                var registrar = await salvar.RegistraContato(
                    TxtNome.Text, 
                    TxtFone1.Text, 
                    TxtFone2.Text, 
                    TxtEmail.Text, 
                    TxtCep.Text,
                    TxtLogradouro.Text,
                    TxtNumero.Text, 
                    TxtBairro.Text, 
                    TxtComplemento.Text, 
                    TxtUf.Text, 
                    TxtCidade.Text);

                CarregaLista();

                if (registrar)
                {
                    await DisplayAlert("Sucesso", "Contato Salvo!", "OK");

                    limpaCampos();

                    return;
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao tentar salvar!", "OK");
                }
            }
            catch
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao tentar salvar!", "OK");
                return;
            }
        }

        private void BtnLimpar_Clicked(object sender, EventArgs e)
        {
            limpaCampos();
        }

        private void limpaCampos()
        {
            TxtNome.Text = string.Empty;
            TxtFone1.Text = string.Empty;
            TxtFone2.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtCep.Text = string.Empty;
            TxtLogradouro.Text = string.Empty;
            TxtNumero.Text = string.Empty;
            TxtBairro.Text = string.Empty;
            TxtComplemento.Text = string.Empty;
            TxtUf.Text = string.Empty;
            TxtCidade.Text = string.Empty;
        }

        private async void SwvDeletar_Invoked(object sender, EventArgs e)
        {
            await DisplayAlert("Teste", "Apagado", "Ok");
        }
    }
}