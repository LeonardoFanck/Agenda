using Agenda.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriarAcesso : ContentPage
    {
        private bool carregando;

        public CriarAcesso()
        {
            InitializeComponent();
        }

        private void telaCarregamento()
        {
            if (carregando)
            {
                bvTelaPreta.IsVisible = false;
                actRoda.IsVisible = false;
                actRoda.IsRunning = false;

                carregando = false;
            }
            else
            {
                bvTelaPreta.IsVisible = true;
                actRoda.IsVisible = true;
                actRoda.IsRunning = true;

                carregando = true;
            }
        }

        private async void btnCriarAcesso_Clicked(object sender, EventArgs e)
        {
            telaCarregamento();

            if (txtCriarSenha.Text != txtConfirmarSenha.Text)
            {
                await DisplayAlert("Falha", "As senhas não Conferem", "Ok");
                telaCarregamento();
                return;
            }

            try
            {
                var acesso = new ServicoUsers();

                var loginDuplicado = await acesso.verificarDuplicado(txtCriarEmail.Text);

                if (loginDuplicado)
                {
                    await DisplayAlert("Aviso", "Esse login já existe, use outro Email!", "Ok");

                    telaCarregamento();
                    return;
                }

                var criarAcesso = await acesso.registrarUsuario(txtCriarEmail.Text, txtCriarSenha.Text);

                if (criarAcesso)
                {
                    await DisplayAlert("Sucesso", "Usuário Criado com sucesso!", "Ok");
                    telaCarregamento();
                    return;
                }
                else
                {
                    await DisplayAlert("Erro", "Não foi possível criar o usuário!", "Ok");
                    telaCarregamento();
                    return;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Ocorreu um erro", "Ok");
                telaCarregamento();
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            txtCriarEmail.Text = string.Empty;
            txtCriarSenha.Text = string.Empty;
            txtConfirmarSenha.Text = string.Empty;
        }
    }
}